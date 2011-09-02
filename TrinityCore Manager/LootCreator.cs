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
    public partial class LootCreator : DevComponents.DotNetBar.Office2007Form
    {
        public LootCreator()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        public bool editor = false;
        public int editLootID = 0;

        private bool finished = false;

        private Dictionary<string, List<string>> lootInfo = null;

        private void LootCreator_Load(object sender, EventArgs e)
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
                lootInfoBackgroundWorker.RunWorkerAsync();
            }
        }

        private void addButtonX_Click(object sender, EventArgs e)
        {
            if (npcEntryIDIntegerInput.Value != 0 && itemEntryIDIntegerInput.Value != 0 && minAmntIntegerInput.Value != 0 && chanceIntegerInput.Value != 0 &&
                maxAmntIntegerInput.Value != 0)
            {

                bool duplicate = false;

                for (int i = 0; i < lootListViewEx.Items.Count; i++)
                {
                    if (lootListViewEx.Items[i].SubItems[1].Text == itemEntryIDIntegerInput.Value.ToString())
                    {
                        duplicate = true;
                    }
                }

                if (duplicate != true)
                {
                    lootListViewEx.Items.Add(new ListViewItem(new string[] { npcEntryIDIntegerInput.Value.ToString(), itemEntryIDIntegerInput.Value.ToString(), chanceIntegerInput.Value.ToString(), minAmntIntegerInput.Value.ToString(), maxAmntIntegerInput.Value.ToString() }));
                }
                else
                {

                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Cannot Add Duplicate", String.Format("Cannot add a duplicate item entry ({0})", itemEntryIDIntegerInput.Value), eTaskDialogButton.Ok));
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error", "You must fill out all fields!", eTaskDialogButton.Ok));
            }
        }

        private void removeButtonX_Click(object sender, EventArgs e)
        {
            if (lootListViewEx.Items.Count != 0)
            {
                if (lootListViewEx.SelectedItems.Count != 0)
                {
                    if (lootListViewEx.SelectedItems.Count == lootListViewEx.Items.Count)
                    {
                        lootListViewEx.Items.Clear();
                    }
                    foreach (ListViewItem lvi in lootListViewEx.SelectedItems)
                    {
                        lootListViewEx.Items.Remove(lvi);
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
            lootListViewEx.Items.Clear();
        }

        private void saveToDBButtonX_Click(object sender, EventArgs e)
        {
            if (mysql == null)
                return;

            for (int i = 0; i < lootListViewEx.Items.Count; i++)
            {

                    string[] columns = new string[] { "entry", "item", "ChanceOrQuestChance", "mincountOrRef", "maxcount" };
                    string[] values = new string[] { lootListViewEx.Items[i].SubItems[0].Text, lootListViewEx.Items[i].SubItems[1].Text, lootListViewEx.Items[i].SubItems[2].Text, lootListViewEx.Items[i].SubItems[3].Text, lootListViewEx.Items[i].SubItems[4].Text };

                    mysql.ReplaceIntoDatabase(Settings.Default.WorldDB, "creature_loot_template", values, columns);


                    string[] updateColumns = new string[] { "lootid", "entry" };
                    string[] updateValues = new string[] { lootListViewEx.Items[i].SubItems[0].Text, lootListViewEx.Items[i].SubItems[0].Text };

                    mysql.UpdateDatabase(Settings.Default.WorldDB, "creature_template", "entry", lootListViewEx.Items[i].SubItems[0].Text, updateValues, updateColumns);


            }

            TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2, "Done", String.Empty, eTaskDialogButton.Ok));

        }

        private void exportSQLButtonX_Click(object sender, EventArgs e)
        {

            if (lootListViewEx.Items.Count == 0)
                return;

            if (exportSQLFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(exportSQLFileDialog.FileName);

                for (int i = 0; i < lootListViewEx.Items.Count; i++)
                {
                    string[] columns = new string[] { "entry", "item", "ChanceOrQuestChance", "mincountOrRef", "maxcount" };
                    string[] values = new string[] { lootListViewEx.Items[i].SubItems[0].Text, lootListViewEx.Items[i].SubItems[1].Text, lootListViewEx.Items[i].SubItems[2].Text, lootListViewEx.Items[i].SubItems[3].Text, lootListViewEx.Items[i].SubItems[4].Text };

                    writer.WriteLine(mysql.CreateMySQLQuery(Settings.Default.WorldDB, "creature_loot_template", values, columns));


                    string[] updateColumns = new string[] { "lootid", "entry" };
                    string[] updateValues = new string[] { lootListViewEx.Items[i].SubItems[0].Text, lootListViewEx.Items[i].SubItems[0].Text };

                    writer.WriteLine(mysql.CreateUpdateQuery(Settings.Default.WorldDB, "creature_template", "entry", lootListViewEx.Items[i].SubItems[0].Text, updateValues, updateColumns));

                    writer.WriteLine(String.Empty);

                }

                writer.Close();

                TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2, "Saved", String.Empty, eTaskDialogButton.Ok));

            }
        }

        private void lootInfoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mysql != null)
                lootInfo = mysql.ReadAll(Settings.Default.WorldDB, "creature_loot_template", new string[] { "entry" }, new string[] { editLootID.ToString() });
        }

        private void lootInfoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (lootInfo != null)
            {

                if (lootInfo.ContainsKey("entry") && lootInfo["entry"].Count >= 1)
                {
                    npcEntryIDIntegerInput.Value = int.Parse(lootInfo["entry"][0]);

                    for (int i = 0; i < lootInfo["entry"].Count; i++)
                    {
                        lootListViewEx.Items.Add(new ListViewItem(new string[] { lootInfo["entry"][0], lootInfo["item"][i], lootInfo["ChanceOrQuestChance"][i], lootInfo["mincountOrRef"][i], lootInfo["maxcount"][i] }));
                    }

                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Loot entry not found!", eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Loot entry not found!", eTaskDialogButton.Ok));

                finished = true;

                this.Close();
            }
        }

        private void LootCreator_FormClosing(object sender, FormClosingEventArgs e)
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

        private void searchNPCEntryIDButtonX_Click(object sender, EventArgs e)
        {
            SearchNPCID searchNPCID = new SearchNPCID();
            searchNPCID.SubmitButtonPressed += new EventHandler(searchNPCID_SubmitButtonPressed);

            searchNPCID.ShowDialog();
        }

        private void searchNPCID_SubmitButtonPressed(object sender, EventArgs e)
        {
            SearchNPCID searchNPCID = (SearchNPCID)sender;

            npcEntryIDIntegerInput.Value = searchNPCID.GetEntryID();

        }

        private void searchItemIDButtonX_Click(object sender, EventArgs e)
        {
            SearchItemID searchEntryID = new SearchItemID();
            searchEntryID.FormClosed += new FormClosedEventHandler(searchEntryID_FormClosed);

            searchEntryID.searchType = SearchItemID.SearchType.Item;

            searchEntryID.ShowDialog();
        }

        private void searchEntryID_FormClosed(object sender, FormClosedEventArgs e)
        {
            SearchItemID searchEntryID = (SearchItemID)sender;

            itemEntryIDIntegerInput.Value = searchEntryID.GetEntryID();
        }

    }
}
