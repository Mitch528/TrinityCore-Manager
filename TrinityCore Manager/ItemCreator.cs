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
    public partial class ItemCreator : DevComponents.DotNetBar.Office2007Form
    {
        public ItemCreator()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        private bool MySQLTestResult = false;


        public bool editor = false;
        public int editItemID = 0;

        private void wizard_Load(object sender, EventArgs e)
        {
            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);

            if (!editor)
            {
                qualityComboBoxEX.SelectedIndex = 0;
                itemTypeComboBoxEX.SelectedIndex = 0;
                itemBindsComboBoxEX.SelectedIndex = 0;

                skillProfressionComboBoxEX.SelectedIndex = 0;

                //Test MySQL Connection

                wizardPage1.NextButtonEnabled = eWizardButtonState.False;

                MySQLConnProgressBarX.Enabled = true;
                MySQLConnProgressBarX.Visible = true;

                MySQLConnLabelX.Visible = true;

                MySQLConnBackgroundWorker.RunWorkerAsync();

            }
            else
            {
                //Get Item Information

                wizardPage1.NextButtonEnabled = eWizardButtonState.False;

                
                MySQLConnProgressBarX.Enabled = true;
                MySQLConnProgressBarX.Visible = true;

                MySQLConnLabelX.Visible = true;
                MySQLConnLabelX.Text = "Getting Item Information...";

                itemInfoBackgroundWorker.RunWorkerAsync();

            }
        }

        private void wizard_WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
        {
            if (e.OldPage == detailsWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (itemNameTextBoxX.Text == String.Empty || displayIDIntegerInput.Value == 0 || entryIDIntegerInput.Value == 0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void wizard_FinishButtonClick(object sender, CancelEventArgs e)
        {
            int type = itemTypeComboBoxEX.SelectedIndex;
            int bind = 0;
            int buymoney = 0;
            int sellmoney = 0;

            switch (itemBindsComboBoxEX.SelectedIndex)
            {
                case 1:
                    bind = 1;
                    break;
                case 2:
                    bind = 3;
                    break;
                default:
                    bind = 0;
                    break;
            }

            buymoney = (Convert.ToInt32(buyCopperIntegerInput.Value) % 100) + (Convert.ToInt32(buySilverIntegerInput.Value) * 100) + (Convert.ToInt32(buyGoldIntegerInput.Value) * 10000);
            sellmoney = (Convert.ToInt32(sellCopperIntegerInput.Value) % 100) + (Convert.ToInt32(sellSilverIntegerInput.Value) * 100) + (Convert.ToInt32(sellGoldIntegerInput.Value) * 10000);


            List<int> AllowableClasses = new List<int>();
            int allowableclass = 0;

            //Allowable Classes

            if (warriorCheckBoxX.Checked)
                AllowableClasses.Add(1);

            if (paladinCheckBoxX.Checked)
                AllowableClasses.Add(2);

            if (hunterCheckBoxX.Checked)
                AllowableClasses.Add(4);

            if (rogueCheckBoxX.Checked)
                AllowableClasses.Add(8);

            if (priestCheckBoxX.Checked)
                AllowableClasses.Add(16);

            if (druidCheckBoxX.Checked)
                AllowableClasses.Add(1024);

            if (shamanCheckBoxX.Checked)
                AllowableClasses.Add(64);

            if (mageCheckBoxX.Checked)
                AllowableClasses.Add(128);

            if (warlockCheckBoxX.Checked)
                AllowableClasses.Add(256);

            if (deathKnightCheckBoxX.Checked)
                AllowableClasses.Add(32);

            foreach (int p in AllowableClasses)
                allowableclass += p;

            if (allowableclass == 0)
                allowableclass = -1;

            List<int> AllowableRaces = new List<int>();
            int allowablerace = 0;

            if (humanCheckBoxX.Checked)
                AllowableRaces.Add(1);

            if (orcCheckBoxX.Checked)
                AllowableRaces.Add(2);

            if (dwarfCheckBoxX.Checked)
                AllowableRaces.Add(4);

            if (nightElfCheckBoxX.Checked)
                AllowableRaces.Add(8);

            if (undeadCheckBoxX.Checked)
                AllowableRaces.Add(16);

            if (taurenCheckBoxX.Checked)
                AllowableRaces.Add(32);

            if (gnomeCheckBoxX.Checked)
                AllowableRaces.Add(64);

            if (trollCheckBoxX.Checked)
                AllowableRaces.Add(128);

            if (bloodelfCheckBoxX.Checked)
                AllowableRaces.Add(512);

            if (draeneiCheckBoxX.Checked)
                AllowableRaces.Add(1024);

            foreach (int p in AllowableRaces)
                allowablerace += p;

            if (allowablerace == 0)
                allowablerace = -1;

            int requiredskill = 0;

            switch (skillProfressionComboBoxEX.SelectedIndex)
            {
                case 1:
                    requiredskill = 129;
                    break;
                case 2:
                    requiredskill = 164;
                    break;
                case 3:
                    requiredskill = 165;
                    break;
                case 4:
                    requiredskill = 171;
                    break;
                case 5:
                    requiredskill = 182;
                    break;
                case 6:
                    requiredskill = 185;
                    break;
                case 7:
                    requiredskill = 186;
                    break;
                case 8:
                    requiredskill = 197;
                    break;
                case 9:
                    requiredskill = 202;
                    break;
                case 10:
                    requiredskill = 333;
                    break;
                case 11:
                    requiredskill = 356;
                    break;
                case 12:
                    requiredskill = 393;
                    break;
                case 13:
                    requiredskill = 755;
                    break;
                case 14:
                    requiredskill = 0;
                    break;
                case 15:
                    requiredskill = 43;
                    break;
                case 16:
                    requiredskill = 44;
                    break;
                case 17:
                    requiredskill = 46;
                    break;
                case 18:
                    requiredskill = 54;
                    break;
                case 19:
                    requiredskill = 55;
                    break;
                case 20:
                    requiredskill = 95;
                    break;
                case 21:
                    requiredskill = 136;
                    break;
                case 22:
                    requiredskill = 160;
                    break;
                case 23:
                    requiredskill = 162;
                    break;
                case 24:
                    requiredskill = 172;
                    break;
                case 25:
                    requiredskill = 173;
                    break;
                case 26:
                    requiredskill = 176;
                    break;
                case 27:
                    requiredskill = 226;
                    break;
                case 28:
                    requiredskill = 228;
                    break;
                case 29:
                    requiredskill = 229;
                    break;
                case 30:
                    requiredskill = 473;
                    break;
                case 31:
                    requiredskill = 762;
                    break;
                default:
                    requiredskill = 0;
                    break;
            }

            if (mysql != null)
            {
                try
                {

                    string itemName = itemNameTextBoxX.Text.Replace("'", "\\'");
                    string itemQuote = itemQuoteTextBoxX.Text.Replace("'", "\\'");

                    string[] columns = new string[] { "entry", "class", "subclass", "name", "displayid", "Quality", "BuyPrice", "SellPrice", "AllowableClass", "AllowableRace", "ItemLevel", "RequiredLevel", "RequiredSkill", "RequiredSkillRank", "maxcount", "bonding", "description" };
                    string[] values = new string[] { entryIDIntegerInput.Value.ToString(), "0", itemTypeComboBoxEX.SelectedIndex.ToString(), itemName, displayIDIntegerInput.Value.ToString(), qualityComboBoxEX.SelectedIndex.ToString(), buymoney.ToString(), sellmoney.ToString(), allowableclass.ToString(), allowablerace.ToString(), itemLevelIntegerInput.Value.ToString(), requiredLevelIntegerInput.Value.ToString(), requiredskill.ToString(), skillLevelIntegerInput.Value.ToString(), maxNumAllowedIntegerInput.Value.ToString(), bind.ToString(), itemQuote };

                    finished = true;

                    if (saveToDBRadioButton.Checked)
                    {
                        mysql.ReplaceIntoDatabase(Settings.Default.WorldDB, "item_template", values, columns);

                        TaskDialog.Show(new TaskDialogInfo("Done", eTaskDialogIcon.Information2, "Item imported into database", String.Empty, eTaskDialogButton.Ok));

                        this.Close();
                    }
                    else
                    {
                        if (saveSQLFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            string qry = mysql.CreateMySQLQuery(Settings.Default.WorldDB, "item_template", values, columns);

                            try
                            {
                                StreamWriter writer = new StreamWriter(saveSQLFileDialog.FileName);

                                writer.WriteLine(qry);

                                writer.Flush();

                                writer.Close();

                                TaskDialog.Show(new TaskDialogInfo("Done", eTaskDialogIcon.Information2, "SQL File Saved", String.Empty, eTaskDialogButton.Ok));


                                this.Close();

                            }
                            catch (Exception ex)
                            {
                                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", ex.Message, eTaskDialogButton.Ok));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", ex.Message, eTaskDialogButton.Ok));
                }
            }

        }

        private void testMySQLConnBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mysql != null)
                MySQLTestResult = mysql.TestMySQLConnection();
        }

        private void testMySQLConnBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            wizardPage1.NextButtonEnabled = eWizardButtonState.True;

            MySQLConnProgressBarX.Enabled = false;
            MySQLConnProgressBarX.Visible = false;

            MySQLConnLabelX.Visible = false;
        }

        private void searchDisplayIDButtonX_Click(object sender, EventArgs e)
        {
            SearchItemID searchDisplay = new SearchItemID();
            searchDisplay.FormClosed += new FormClosedEventHandler(searchDisplay_FormClosed);

            searchDisplay.searchType = SearchItemID.SearchType.Item;

            searchDisplay.ShowDialog();
        }

        private void searchDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            SearchItemID searchDisplay = (SearchItemID)sender;

            displayIDIntegerInput.Value = searchDisplay.GetDisplayID();
        }

        private void classAllCheckBoxX_CheckedChanged(object sender, EventArgs e)
        {
            if (classAllCheckBoxX.Checked)
            {
                warriorCheckBoxX.Checked = true;
                paladinCheckBoxX.Checked = true;
                hunterCheckBoxX.Checked = true;
                rogueCheckBoxX.Checked = true;
                priestCheckBoxX.Checked = true;
                druidCheckBoxX.Checked = true;
                shamanCheckBoxX.Checked = true;
                mageCheckBoxX.Checked = true;
                warlockCheckBoxX.Checked = true;
                deathKnightCheckBoxX.Checked = true;
            }
            else
            {
                warriorCheckBoxX.Checked = false;
                paladinCheckBoxX.Checked = false;
                hunterCheckBoxX.Checked = false;
                rogueCheckBoxX.Checked = false;
                priestCheckBoxX.Checked = false;
                druidCheckBoxX.Checked = false;
                shamanCheckBoxX.Checked = false;
                mageCheckBoxX.Checked = false;
                warlockCheckBoxX.Checked = false;
                deathKnightCheckBoxX.Checked = false;
            }
        }

        private void raceAllCheckBoxX_CheckedChanged(object sender, EventArgs e)
        {
            if (raceAllCheckBoxX.Checked)
            {
                humanCheckBoxX.Checked = true;
                orcCheckBoxX.Checked = true;
                dwarfCheckBoxX.Checked = true;
                nightElfCheckBoxX.Checked = true;
                undeadCheckBoxX.Checked = true;
                taurenCheckBoxX.Checked = true;
                gnomeCheckBoxX.Checked = true;
                trollCheckBoxX.Checked = true;
                bloodelfCheckBoxX.Checked = true;
                draeneiCheckBoxX.Checked = true;
            }
            else
            {
                humanCheckBoxX.Checked = false;
                orcCheckBoxX.Checked = false;
                dwarfCheckBoxX.Checked = false;
                nightElfCheckBoxX.Checked = false;
                undeadCheckBoxX.Checked = false;
                taurenCheckBoxX.Checked = false;
                gnomeCheckBoxX.Checked = false;
                trollCheckBoxX.Checked = false;
                bloodelfCheckBoxX.Checked = false;
                draeneiCheckBoxX.Checked = false;
            }
        }

        private void wizard_CancelButtonClick(object sender, CancelEventArgs e)
        {

        }

        private bool finished = false;

        private void ItemCreator_FormClosing(object sender, FormClosingEventArgs e)
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

        Dictionary<string, List<string>> itemInfo = null;

        private void itemInfoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (mysql != null)
                    itemInfo = mysql.ReadAll(Settings.Default.WorldDB, "item_template", new string[] { "entry" }, new string[] { editItemID.ToString() });
            }
            catch
            {
            }
        }

        private void itemInfoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            MySQLConnProgressBarX.Enabled = false;
            MySQLConnProgressBarX.Visible = false;

            MySQLConnLabelX.Visible = false;

            if (itemInfo != null && itemInfo.ContainsKey("entry") && itemInfo["entry"].Count == 1)
            {

                if (int.Parse(itemInfo["class"][0]) == 2)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "This entry id corresponds to a weapon!", "Please use the weapon editor", eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();
                }
                else if (int.Parse(itemInfo["class"][0]) == 4)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "This entry id corresponds to a piece of armor", "Please use the armor editor", eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();
                }

                wizardPage1.NextButtonEnabled = eWizardButtonState.True;

                //Details

                entryIDIntegerInput.Value = editItemID;
                itemNameTextBoxX.Text = itemInfo["name"][0];
                itemQuoteTextBoxX.Text = itemInfo["description"][0];
                qualityComboBoxEX.SelectedIndex = int.Parse(itemInfo["Quality"][0]);
                displayIDIntegerInput.Value = int.Parse(itemInfo["displayid"][0]);
                itemTypeComboBoxEX.SelectedIndex = int.Parse(itemInfo["subclass"][0]);

                int bind = 0;

                switch (int.Parse(itemInfo["bonding"][0]))
                {
                    case 1:
                        bind = 1;
                        break;
                    case 2:
                        bind = 3;
                        break;
                    default:
                        bind = 0;
                        break;
                }

                itemBindsComboBoxEX.SelectedIndex = bind;
                itemLevelIntegerInput.Value = int.Parse(itemInfo["ItemLevel"][0]);
                requiredLevelIntegerInput.Value = int.Parse(itemInfo["RequiredLevel"][0]);

                int requiredskill = int.Parse(itemInfo["RequiredSkill"][0]);

                switch (requiredskill)
                {
                    case 192:
                        skillProfressionComboBoxEX.SelectedIndex = 1;
                        break;
                    case 164:
                        skillProfressionComboBoxEX.SelectedIndex = 2;
                        break;
                    case 165:
                        skillProfressionComboBoxEX.SelectedIndex = 3;
                        break;
                    case 171:
                        skillProfressionComboBoxEX.SelectedIndex = 4;
                        break;
                    case 182:
                        skillProfressionComboBoxEX.SelectedIndex = 5;
                        break;
                    case 185:
                        skillProfressionComboBoxEX.SelectedIndex = 6;
                        break;
                    case 186:
                        skillProfressionComboBoxEX.SelectedIndex = 7;
                        break;
                    case 197:
                        skillProfressionComboBoxEX.SelectedIndex = 8;
                        break;
                    case 202:
                        skillProfressionComboBoxEX.SelectedIndex = 9;
                        break;
                    case 333:
                        skillProfressionComboBoxEX.SelectedIndex = 10;
                        break;
                    case 356:
                        skillProfressionComboBoxEX.SelectedIndex = 11;
                        break;
                    case 393:
                        skillProfressionComboBoxEX.SelectedIndex = 12;
                        break;
                    case 755:
                        skillProfressionComboBoxEX.SelectedIndex = 13;
                        break;
                    case 0:
                        skillProfressionComboBoxEX.SelectedIndex = 14;
                        break;
                    case 43:
                        skillProfressionComboBoxEX.SelectedIndex = 15;
                        break;
                    case 44:
                        skillProfressionComboBoxEX.SelectedIndex = 16;
                        break;
                    case 46:
                        skillProfressionComboBoxEX.SelectedIndex = 17;
                        break;
                    case 54:
                        skillProfressionComboBoxEX.SelectedIndex = 18;
                        break;
                    case 55:
                        skillProfressionComboBoxEX.SelectedIndex = 19;
                        break;
                    case 95:
                        skillProfressionComboBoxEX.SelectedIndex = 20;
                        break;
                    case 136:
                        skillProfressionComboBoxEX.SelectedIndex = 21;
                        break;
                    case 160:
                        skillProfressionComboBoxEX.SelectedIndex = 22;
                        break;
                    case 162:
                        skillProfressionComboBoxEX.SelectedIndex = 23;
                        break;
                    case 172:
                        skillProfressionComboBoxEX.SelectedIndex = 24;
                        break;
                    case 173:
                        skillProfressionComboBoxEX.SelectedIndex = 25;
                        break;
                    case 176:
                        skillProfressionComboBoxEX.SelectedIndex = 26;
                        break;
                    case 226:
                        skillProfressionComboBoxEX.SelectedIndex = 27;
                        break;
                    case 228:
                        skillProfressionComboBoxEX.SelectedIndex = 28;
                        break;
                    case 229:
                        skillProfressionComboBoxEX.SelectedIndex = 29;
                        break;
                    case 473:
                        skillProfressionComboBoxEX.SelectedIndex = 30;
                        break;
                    case 762:
                        skillProfressionComboBoxEX.SelectedIndex = 31;
                        break;
                    default:
                        skillProfressionComboBoxEX.SelectedIndex = 0;
                        break;
                }

                skillLevelIntegerInput.Value = int.Parse(itemInfo["RequiredSkillRank"][0]);
                maxNumAllowedIntegerInput.Value = int.Parse(itemInfo["maxcount"][0]);

                if ((int.Parse(itemInfo["AllowableClass"][0]) & 1) != 0)
                    warriorCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 2) != 0)
                    paladinCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 4) != 0)
                    hunterCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 8) != 0)
                    rogueCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 16) != 0)
                    priestCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 32) != 0)
                    deathKnightCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 64) != 0)
                    shamanCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 128) != 0)
                    mageCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 256) != 0)
                    warlockCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableClass"][0]) & 1024) != 0)
                    druidCheckBoxX.Checked = true;


                if ((int.Parse(itemInfo["AllowableRace"][0]) & 1) != 0)
                    humanCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 2) != 0)
                    orcCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 4) != 0)
                    dwarfCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 8) != 0)
                    nightElfCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 16) != 0)
                    undeadCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 32) != 0)
                    taurenCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 64) != 0)
                    gnomeCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 128) != 0)
                    trollCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 512) != 0)
                    bloodelfCheckBoxX.Checked = true;
                if ((int.Parse(itemInfo["AllowableRace"][0]) & 1024) != 0)
                    draeneiCheckBoxX.Checked = true;

                int buyTotal = int.Parse(itemInfo["BuyPrice"][0]);

                int gold = buyTotal / 10000;

                buyTotal = buyTotal % 10000;

                int silver = buyTotal / 100;

                int copper = buyTotal % 100;

                buyGoldIntegerInput.Value = gold;
                buySilverIntegerInput.Value = silver;
                buyCopperIntegerInput.Value = copper;


                buyTotal = int.Parse(itemInfo["SellPrice"][0]);

                gold = buyTotal / 10000;

                buyTotal = buyTotal % 10000;

                silver = buyTotal / 100;

                copper = buyTotal % 100;


                sellGoldIntegerInput.Value = gold;
                sellSilverIntegerInput.Value = silver;
                sellCopperIntegerInput.Value = copper;
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Item not found!", eTaskDialogButton.Ok));
            }
        }

        private void ItemCreator_Load(object sender, EventArgs e)
        {

        }

    }
}
