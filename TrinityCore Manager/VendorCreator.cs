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
using System.IO;

namespace TrinityCore_Manager
{
    public partial class VendorCreator : DevComponents.DotNetBar.Office2007Form
    {
        public VendorCreator()
        {
            InitializeComponent();
        }

        public bool editor = false;
        public int editVendorID = 0;

        private bool finished = false;

        private Dictionary<string, List<string>> vendorInfo = null;

        private void VendorCreator_Load(object sender, EventArgs e)
        {
            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);


            bool testMySQL = mysql.TestMySQLConnection();

            if (!testMySQL)
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Could Not Connect To MySQL", String.Empty, eTaskDialogButton.Ok));

                finished = true;

                this.Close();
            }

            if (editor)
            {
                vendorInfoBackgroundWorker.RunWorkerAsync();
            }

        }

        private SQLMethods mysql = null;

        private void addButtonX_Click(object sender, EventArgs e)
        {
            if (vendorIntegerInput.Value != 0 && itemEntryIntegerInput.Value != 0)
            {

                bool duplicate = false;

                for (int i = 0; i < vendorListViewEx.Items.Count; i++)
                {
                    if (vendorListViewEx.Items[i].SubItems[1].Text == vendorIntegerInput.Value.ToString())
                        duplicate = true;
                }

                if (duplicate != true)
                {
                    vendorListViewEx.Items.Add(new ListViewItem(new string[] { vendorIntegerInput.Value.ToString(), itemEntryIntegerInput.Value.ToString() }));
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, String.Format("Cannot add a duplicate item entry ({0})", vendorIntegerInput.Value), String.Empty, eTaskDialogButton.Ok));
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "You must fill out all fields!", String.Empty, eTaskDialogButton.Ok));
            }
        }

        private void removeButtonX_Click(object sender, EventArgs e)
        {
            if (vendorListViewEx.Items.Count != 0)
            {
                if (vendorListViewEx.SelectedItems.Count != 0)
                {
                    if (vendorListViewEx.SelectedItems.Count == vendorListViewEx.Items.Count)
                    {
                        vendorListViewEx.Items.Clear();
                    }
                    foreach (ListViewItem lvi in vendorListViewEx.SelectedItems)
                    {
                        vendorListViewEx.Items.Remove(lvi);
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "You have not selected a row to delete!", String.Empty, eTaskDialogButton.Ok));
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "There are no rows to delete!", String.Empty, eTaskDialogButton.Ok));
            }
        }

        private void resetButtonX_Click(object sender, EventArgs e)
        {
            vendorListViewEx.Items.Clear();
        }

        private void saveToDBButtonX_Click(object sender, EventArgs e)
        {

            if (mysql == null)
                return;

            for (int i = 0; i < vendorListViewEx.Items.Count; i++)
            {
                string[] columns = new string[] { "entry", "item" };
                string[] values = new string[] { vendorListViewEx.Items[i].SubItems[0].Text, vendorListViewEx.Items[i].SubItems[1].Text };

                mysql.ReplaceIntoDatabase(Settings.Default.WorldDB, "npc_vendor", values, columns);
            }

            TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2, "Done", String.Empty, eTaskDialogButton.Ok));

        }

        private void VendorCreator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finished)
            {
                eTaskDialogButton button = eTaskDialogButton.Yes;

                button |= eTaskDialogButton.No;

                eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Are you sure?", eTaskDialogIcon.Exclamation, "Confirm", "Are you sure you want to exit?", button));

                if (result == eTaskDialogResult.Yes)
                {
                    finished = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void vendorInfoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mysql != null)
                vendorInfo = mysql.ReadAll(Settings.Default.WorldDB, "npc_vendor", new string[] { "entry" }, new string[] { editVendorID.ToString() });
        }

        private void vendorInfoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (vendorInfo != null)
            {

                if (vendorInfo.ContainsKey("entry") && vendorInfo["entry"].Count == 1)
                {

                    vendorIntegerInput.Value = int.Parse(vendorInfo["entry"][0]);


                    foreach (string item in vendorInfo["item"])
                    {
                        vendorListViewEx.Items.Add(new ListViewItem(new string[] { vendorInfo["entry"][0], item }));
                    }

                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Vendor not found!", eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Vendor not found!", eTaskDialogButton.Ok));

                finished = true;

                this.Close();
            }
        }

        private void exportSQLButtonX_Click(object sender, EventArgs e)
        {
            if (exportSQLFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(exportSQLFileDialog.FileName);

                for (int i = 0; i < vendorListViewEx.Items.Count; i++)
                {
                    string[] columns = new string[] { "entry", "item" };
                    string[] values = new string[] { vendorIntegerInput.Value.ToString(), vendorListViewEx.Items[i].SubItems[1].Text };

                    writer.WriteLine(mysql.CreateMySQLQuery(Settings.Default.WorldDB, "npc_vendor", values, columns));
                }

                writer.Close();

                TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2, "Saved", String.Empty, eTaskDialogButton.Ok));
            }
        }
    }
}
