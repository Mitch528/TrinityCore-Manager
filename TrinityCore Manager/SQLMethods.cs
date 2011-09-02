//    This file is part of TrinityCore Manager.

//    TrinityCore Manager is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    TrinityCore Manager is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with TrinityCore Manager.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrinityCore_Manager
{
    [Serializable]
    class SQLMethods : ICloneable
    {

        private string Host = String.Empty;
        private string Username = String.Empty;
        private string Password = String.Empty;
        private int Port = 3306;

        public SQLMethods(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
        }

        
        /*  http://www.thereforesystems.com/fundamentals-deep-cloning-in-c/  */
        public object Clone()
        {
            object clone;

            using (MemoryStream stream = new MemoryStream())
            {

                BinaryFormatter formatter = new BinaryFormatter();

                // Serialize this object

                formatter.Serialize(stream, this);

                stream.Position = 0;

                // Deserialize to another object

                clone = formatter.Deserialize(stream);

            }

            return clone;
        }

        public bool TestMySQLConnection()
        {

            try
            {
                string connection = String.Format("Data Source={0};User ID={1};Password={2};Port={3};Convert Zero Datetime=true;", Host, Username, Password, Port);

                MySqlConnection mConnect = new MySqlConnection(connection);

                mConnect.Open();
                mConnect.Close();
                mConnect.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogType.Error);
            }

            return false;

        }

        public bool CheckDBExists(string db)
        {
            try
            {
                string connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};Convert Zero Datetime=true;", Host, db, Username, Password, Port);

                MySqlConnection mConnect = new MySqlConnection(connection);

                mConnect.Open();
                mConnect.Close();
                mConnect.Dispose();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public event EventHandler MySQLStatementExecuted;

        public void ExecuteMySQLScript(string file, string db = "")
        {
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {

                    string connection = String.Empty;

                    if (db != String.Empty)
                        connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};", Host, db, Username, Password, Port);
                    else
                        connection = String.Format("Data Source={0};User ID={1};Password={2};Port={3};", Host, Username, Password, Port);

                    MySqlConnection conn = new MySqlConnection(connection);

                    MySqlScript script = new MySqlScript(conn, reader.ReadToEnd());

                    script.StatementExecuted += new MySqlStatementExecutedEventHandler(script_StatementExecuted);

                    script.Execute();

                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public event EventHandler MySQLScriptsFinished;

        public void ExecuteMySQLScripts(List<FileInfo> files, string db)
        {
            try
            {

                StringBuilder scriptBuilder = new StringBuilder();

                foreach (FileInfo fInfo in files)
                {
                    using (StreamReader reader = new StreamReader(fInfo.FullName))
                    {
                        scriptBuilder.Append(reader.ReadToEnd());
                    }
                }

                string connection = String.Empty;

                if (db != String.Empty)
                    connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};", Host, db, Username, Password, Port);
                else
                    connection = String.Format("Data Source={0};User ID={1};Password={2};Port={3};", Host, Username, Password, Port);

                MySqlConnection conn = new MySqlConnection(connection);

                MySqlScript script = new MySqlScript(conn, scriptBuilder.ToString());

                script.ScriptCompleted += new EventHandler(script_ScriptCompleted);

                script.Execute();

                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogType.Error);
            }
        }

        private void script_ScriptCompleted(object sender, EventArgs e)
        {
            if (MySQLScriptsFinished != null)
                MySQLScriptsFinished(this, new EventArgs());
        }

        public MySqlConnection NewMySQLConnection(string database = "")
        {

            string connection = String.Empty;

            if (database != String.Empty)
                connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};", Host, database, Username, Password, Port);
            else
                connection = String.Format("Data Source={0};User ID={1};Password={2};Port={3};", Host, Username, Password, Port);

            MySqlConnection mConnection = new MySqlConnection(connection);

            return mConnection;
        }

        private void script_StatementExecuted(object sender, EventArgs e)
        {
            if (MySQLStatementExecuted != null)
                MySQLStatementExecuted(this, new EventArgs());
        }

        public void ExecuteMySQLFiles(List<FileInfo> files, string db = "")
        {
            foreach (FileInfo fInfo in files)
            {
                ExecuteMySQLFile(fInfo.FullName, db);
            }
        }

        public void ExecuteMySQLFile(string loc, string db = "")
        {
            try
            {
                using (StreamReader reader = new StreamReader(loc))
                {

                    StringBuilder sb = new StringBuilder();

                    int totalLines = File.ReadAllLines(loc).Length;
                    int numLines = 0;

                    while (reader.Peek() != -1)
                    {

                        numLines++;

                        int value = (int)(((double)numLines / (double)totalLines) * 100);

                        string line = reader.ReadLine().TrimEnd();

                        if (line == String.Empty)
                            continue;

                        if (line.StartsWith("--"))
                            continue;

                        if (line.StartsWith("/*!"))
                        {
                            if (line.EndsWith("*/;"))
                            {
                                sb.Append(line);
                            }
                            else
                                continue;
                        }
                        else
                        {
                            sb.Append(line);
                        }

                        if (!line.EndsWith(";"))
                            continue;

                        string connection = String.Empty;

                        if (db != String.Empty)
                            connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};allow user variables=true;", Host, db, Username, Password, Port);
                        else
                            connection = String.Format("Data Source={0};User ID={1};Password={2};Port={3};allow user variables=true;", Host, Username, Password, Port);

                        MySqlConnection mConnection = new MySqlConnection(connection);

                        mConnection.Open();

                        MySqlCommand cmd = new MySqlCommand(sb.ToString(), mConnection);

                        cmd.ExecuteNonQuery();

                        mConnection.Close();
                        mConnection.Dispose();

                        if (line.EndsWith(";"))
                            sb.Clear();

                    }
                }
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message, Logger.LogType.Error);
            }
        }

        public void UpdateDatabase(string db, string table, string where, string equals, string[] valuesInOrder, string[] columnsInOrder)
        {
            try
            {
                string qry = CreateUpdateQuery(db, table, where, equals, valuesInOrder, columnsInOrder);

                Console.WriteLine(qry);


                string connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};Convert Zero Datetime=true;", Host, db, Username, Password, Port);

                MySqlConnection mConnect = new MySqlConnection(connection);

                mConnect.Open();


                MySqlCommand cmd = new MySqlCommand(qry, mConnect);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                mConnect.Close();
                mConnect.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateUpdateQuery(string db, string table, string where, string equals, string[] valuesInOrder, string[] columnsInOrder)
        {
            StringBuilder sb = new StringBuilder(String.Format("UPDATE {0} SET ", table));

            for (int i = 0; i < valuesInOrder.Length; i++)
            {
                int value = int.MaxValue;

                bool isInt = int.TryParse(valuesInOrder[i], out value);

                if (valuesInOrder.Length - 1 != i)
                {
                    if (!isInt)
                        sb.Append(String.Format("{0} = \"{1}\", ", columnsInOrder[i], valuesInOrder[i]));
                    else
                        sb.Append(String.Format("{0} = ", columnsInOrder[i]) + value + ", ");
                }
                else
                {
                    if (!isInt)
                        sb.Append(String.Format("{0} = \"{1}\"", columnsInOrder[i], valuesInOrder[i]));
                    else
                        sb.Append(String.Format("{0} = ", columnsInOrder[i]) + value);
                }
            }

            int val = 0;

            if (int.TryParse(equals, out val))
                sb.Append(String.Format(" WHERE {0} = {1}", where, val));
            else
                sb.Append(String.Format(" WHERE {0} = '{1}'", where, equals));

            return sb.ToString();
        }

        public string CreateMySQLQuery(string db, string table, string[] valuesInOrder, string[] columnsInOrder = null)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < valuesInOrder.Length; i++)
            {
                int value = int.MaxValue;

                bool isInt = int.TryParse(valuesInOrder[i], out value);

                if (valuesInOrder.Length - 1 != i)
                {
                    if (!isInt)
                        sb.Append(String.Format("'{0}', ", valuesInOrder[i]));
                    else
                        sb.Append(value + ", ");
                }
                else
                {
                    if (!isInt)
                        sb.Append(String.Format("'{0}'", valuesInOrder[i]));
                    else
                        sb.Append(value);
                }
            }

            string command = String.Empty;


            if (columnsInOrder == null)
                command = String.Format("REPLACE INTO {0} ({1}) VALUES ({2})", table, GetColumnStringFromTable(db, table), sb.ToString());
            else
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < columnsInOrder.Length; i++)
                {

                    if (columnsInOrder.Length - 1 != i)
                    {
                        builder.Append(String.Format("{0}, ", columnsInOrder[i]));
                    }
                    else
                    {
                        builder.Append(String.Format("{0}", columnsInOrder[i]));
                    }
                }

                command = String.Format("REPLACE INTO {0} ({1}) VALUES ({2})", table, builder.ToString(), sb.ToString());

            }

            return command;

        }

        public void ReplaceIntoDatabase(string db, string table, string[] valuesInOrder, string[] columnsInOrder = null)
        {
            try
            {
                string connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};Convert Zero Datetime=true;", Host, db, Username, Password, Port);

                MySqlConnection mConnect = new MySqlConnection(connection);

                mConnect.Open();


                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < valuesInOrder.Length; i++)
                {

                    Console.WriteLine(valuesInOrder[i]);
                    
                    int value = int.MaxValue;

                    bool isInt = int.TryParse(valuesInOrder[i], out value);

                    if (valuesInOrder.Length - 1 != i)
                    {
                        if (!isInt)
                            sb.Append(String.Format("'{0}', ", valuesInOrder[i]));
                        else
                            sb.Append(value + ", ");
                    }
                    else
                    {
                        if (!isInt)
                            sb.Append(String.Format("'{0}'", valuesInOrder[i]));
                        else
                            sb.Append(value);
                    }
                }

                string command = String.Empty;


                if (columnsInOrder == null)
                    command = String.Format("REPLACE INTO {0} ({1}) VALUES ({2})", table, GetColumnStringFromTable(db, table), sb.ToString());
                else
                {
                    StringBuilder builder = new StringBuilder();

                    for (int i = 0; i < columnsInOrder.Length; i++)
                    {

                        if (columnsInOrder.Length - 1 != i)
                        {
                            builder.Append(String.Format("{0}, ", columnsInOrder[i]));
                        }
                        else
                        {
                            builder.Append(String.Format("{0}", columnsInOrder[i]));
                        }
                    }

                    command = String.Format("REPLACE INTO {0} ({1}) VALUES ({2})", table, builder.ToString(), sb.ToString());

                }

                MySqlCommand cmd = new MySqlCommand(command, mConnect);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                mConnect.Close();
                mConnect.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetColumnStringFromTable(string db, string table)
        {

            try
            {

                string connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};Convert Zero Datetime=true;", Host, db, Username, Password, Port);

                MySqlConnection mConnect = new MySqlConnection(connection);

                mConnect.Open();


                MySqlDataAdapter da = new MySqlDataAdapter(String.Format("SHOW columns FROM {0}", table), mConnect);

                DataSet data = new DataSet();

                da.Fill(data, table);

                int i = 0;

                List<string> type = new List<string>();

                StringBuilder builder = new StringBuilder();

                foreach (DataRow dr in data.Tables[table].Rows)
                {

                    if (data.Tables[table].Rows.Count - 1 != i)
                        builder.Append(String.Format("{0}, ", dr["Field"].ToString()));
                    else
                        builder.Append(String.Format("{0}", dr["Field"].ToString()));

                    type.Add(dr["Type"].ToString());

                    i++;
                }

                data.Clear();

                data = new DataSet();

                mConnect.Close();
                mConnect.Dispose();

                return builder.ToString();

            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public Dictionary<string, List<string>> ReadAll(string db, string table, string[] whereInOrder = null, string[] equalsInOrder = null)
        {

            try
            {

                string connection = String.Format("Data Source={0};Database={1};User ID={2};Password={3};Port={4};Convert Zero Datetime=true;", Host, db, Username, Password, Port);

                MySqlConnection mConnect = new MySqlConnection(connection);

                mConnect.Open();


                MySqlDataAdapter da = new MySqlDataAdapter(String.Format("SHOW columns FROM {0}", table), mConnect);

                DataSet data = new DataSet();

                da.Fill(data, table);

                int i = 0;

                List<string> type = new List<string>();

                StringBuilder builder = new StringBuilder();

                foreach (DataRow dr in data.Tables[table].Rows)
                {

                    if (data.Tables[table].Rows.Count - 1 != i)
                        builder.Append(String.Format("{0}, ", dr["Field"].ToString()));
                    else
                        builder.Append(String.Format("{0}", dr["Field"].ToString()));

                    type.Add(dr["Type"].ToString());

                    i++;
                }

                data.Clear();

                data = new DataSet();

                if (whereInOrder == null || equalsInOrder == null)
                {
                    da = new MySqlDataAdapter(String.Format("SELECT {0} FROM {1}", builder.ToString(), table), mConnect);
                }
                else
                {

                    Console.WriteLine(whereInOrder.Length);

                    StringBuilder sb = new StringBuilder(String.Format("SELECT {0} FROM {1} WHERE ", builder.ToString(), table));

                    if (whereInOrder.Length != equalsInOrder.Length)
                    {
                        return new Dictionary<string, List<string>>();
                    }

                    for (int x = 0; x < whereInOrder.Length; x++)
                    {

                        if (x == whereInOrder.Length - 1)
                            sb.Append(String.Format("{0} = '{1}'", whereInOrder[x], equalsInOrder[x]));
                        else
                            sb.Append(String.Format("{0} = '{1}' AND ", whereInOrder[x], equalsInOrder[x]));
                    }

                    da = new MySqlDataAdapter(sb.ToString(), mConnect);
                    //da = new MySqlDataAdapter(String.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", builder.ToString(), table, where, equals), mConnect);
                }

                da.Fill(data, table);


                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

                i = 0;

                foreach (DataColumn dc in data.Tables[table].Columns)
                {
                    List<string> tempList = new List<string>();

                    foreach (DataRow dr in dc.Table.Rows)
                    {
                        tempList.Add(dr[dc].ToString());
                    }

                    dict.Add(dc.ToString(), tempList);

                    i++;

                }

                mConnect.Close();
                mConnect.Dispose();

                return dict;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogType.Error);
            }

            return new Dictionary<string, List<string>>();
        }
    }
}
