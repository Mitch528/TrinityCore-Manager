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
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;

namespace TrinityCore_Manager
{
    public partial class SearchItemID : DevComponents.DotNetBar.Office2007Form
    {
        public SearchItemID()
        {
            InitializeComponent();
        }

        public enum SearchType
        {
            Item,
            Weapon,
            Armor
        }

        private void SearchDisplayID_Load(object sender, EventArgs e)
        {
            itemsListBox.SelectedIndexChanged += new EventHandler(itemsListBox_SelectedIndexChanged);

            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);
        }

        public SearchType searchType = SearchType.Item;

        private List<string> displayids = new List<string>();
        private List<string> entryids = new List<string>();

        private XmlReader reader;
        private WebRequest request;

        private SQLMethods mysql = null;

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemEntryLabelX.Text = String.Format("Entry ID: {0}", entryids[itemsListBox.SelectedIndex]);
            itemDisplayIDLabelX.Text = String.Format("Display ID: {0}", displayids[itemsListBox.SelectedIndex]);
        }

        
        private void searchButtonX_Click(object sender, EventArgs e)
        {
            if (itemsListBox.Items.Count != 0)
                itemsListBox.Items.Clear();

            if (entryids.Count != 0)
                entryids.Clear();

            if (displayids.Count != 0)
                displayids.Clear();

            if (itemNameTextBoxX.Text != String.Empty)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    circularProgress.Visible = true;
                    circularProgress.IsRunning = true;

                    MySqlConnection conn = mysql.NewMySQLConnection(Settings.Default.WorldDB);

                    conn.Open();

                    string qry = String.Empty;

                    if (searchType == SearchType.Item)
                        qry = String.Format("SELECT * FROM item_template WHERE name LIKE '%{0}%' LIMIT 200", itemNameTextBoxX.Text);
                    else if (searchType == SearchType.Weapon)
                        qry = String.Format("SELECT * FROM item_template WHERE name LIKE '%{0}%' AND class='2' LIMIT 200", itemNameTextBoxX.Text);
                    else if (searchType == SearchType.Armor)
                        qry = String.Format("SELECT * FROM item_template WHERE name LIKE '%{0}%' AND class='4' LIMIT 200", itemNameTextBoxX.Text);

                    MySqlCommand cmd = new MySqlCommand(qry, conn);

                    MySqlDataReader Reader = cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        itemsListBox.Items.Add(Reader.GetString("name"));
                        displayids.Add(Reader.GetString("displayid"));
                        entryids.Add(Reader.GetString("entry"));
                    }

                    conn.Close();

                    this.Cursor = Cursors.Default;

                    circularProgress.IsRunning = false;
                    circularProgress.Visible = false;

                    if (itemsListBox.Items.Count != 0)
                    {
                        itemsListBox.SelectedIndex = 0;
                    }

                    itemsListBox.Focus();

                }
                catch (Exception ex)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", ex.Message, eTaskDialogButton.Ok));
                }
            }
        }

        public int GetDisplayID()
        {

            if (itemsListBox.SelectedIndex != -1 && itemsListBox.Items.Count != 0)
            {

                int id = 0;

                int.TryParse(displayids[itemsListBox.SelectedIndex], out id);

                return id;
            }
            else
            {
                return 0;
            }
        }

        public int GetEntryID()
        {
            if (itemsListBox.SelectedIndex != -1 && itemsListBox.Items.Count != 0)
            {
                int id = 0;

                int.TryParse(entryids[itemsListBox.SelectedIndex], out id);

                return id;
            }
            else
            {
                return 0;
            }
        }

        private void submitButtonX_Click(object sender, EventArgs e)
        {
            if (itemsListBox.Items.Count != 0 && itemsListBox.SelectedIndex != -1)
            {
                this.Close();
            }
        }
    }
}
