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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using TrinityCore_Manager.Properties;
using MySql.Data.MySqlClient;

namespace TrinityCore_Manager
{
    public partial class SearchNPCID : DevComponents.DotNetBar.Office2007Form
    {
        public SearchNPCID()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        public bool buttonPressed = false;

        private List<int> displayids = new List<int>();
        private List<int> entryids = new List<int>();

        private void SearchNPCID_Load(object sender, EventArgs e)
        {
            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);
        }

        private void npcSearchButtonX_Click(object sender, EventArgs e)
        {
            if (npcSearchListBox.Items.Count != 0)
                npcSearchListBox.Items.Clear();

            if (entryids.Count != 0)
                entryids.Clear();

            if (displayids.Count != 0)
                displayids.Clear();


            if (npcSearchTextBoxX.Text != String.Empty)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    BackgroundWorker mysqlBackgroundWorker = new BackgroundWorker();
                    mysqlBackgroundWorker.DoWork += new DoWorkEventHandler(mysqlBackgroundWorker_DoWork);
                    mysqlBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mysqlBackgroundWorker_RunWorkerCompleted);

                    mysqlBackgroundWorker.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error", ex.Message, eTaskDialogButton.Ok));
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error", "You must supply an NPC name to proceed!", eTaskDialogButton.Ok));
            }

        }

        private void mysqlBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                MySqlConnection mConnection = mysql.NewMySQLConnection(Settings.Default.WorldDB);

                string qry = String.Format("SELECT * FROM creature_template WHERE name LIKE '%{0}%' LIMIT 200", npcSearchTextBoxX.Text);

                mConnection.Open();

                MySqlCommand cmd = new MySqlCommand(qry, mConnection);

                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    npcSearchListBox.Items.Add(Reader.GetString("name"));
                    displayids.Add(Reader.GetInt32("modelid1"));
                    entryids.Add(Reader.GetInt32("entry"));
                }

                mConnection.Close();
                mConnection.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mysqlBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;

            if (npcSearchListBox.Items.Count != 0)
            {
                npcSearchListBox.SelectedIndex = 0;
            }

            npcSearchListBox.Focus();
        }

        public int GetDisplayID()
        {
            if (npcSearchListBox.SelectedIndex != -1 && npcSearchListBox.Items.Count != 0)
            {
                return displayids[npcSearchListBox.SelectedIndex];
            }
            else
            {
                return 0;
            }
        }

        public int GetEntryID()
        {
            if (npcSearchListBox.SelectedIndex != -1 && npcSearchListBox.Items.Count != 0)
            {
                return entryids[npcSearchListBox.SelectedIndex];
            }
            else
            {
                return 0;
            }
        }

        public event EventHandler SubmitButtonPressed;

        private void submitButtonX_Click(object sender, EventArgs e)
        {
            if (SubmitButtonPressed != null)
                SubmitButtonPressed(this, new EventArgs());

            this.Close();
        }

    }
}
