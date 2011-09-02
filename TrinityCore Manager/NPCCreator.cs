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
    public partial class NPCCreator : DevComponents.DotNetBar.Office2007Form
    {
        public NPCCreator()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        private bool MySQLTestResult = false;


        public bool editor = false;
        public int editNPCID = 0;

        private bool finished = false;

        private void NPCCreator_Load(object sender, EventArgs e)
        {
            //Set Default Values

            rankComboBoxEx.SelectedIndex = 0;
            factionComboBoxEx.SelectedIndex = 0;
            typeComboBoxEx.SelectedIndex = 0;
            familyComboItemEx.SelectedIndex = 0;
            classComboBoxEx.SelectedIndex = 0;
            aiNameComboBoxEx.SelectedIndex = 0;
            movementTypeComboBoxEx.SelectedIndex = 0;
            inhabitComboBoxEx.SelectedIndex = 0;


            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);

            if (!editor)
            {
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
                MySQLConnLabelX.Text = "Getting NPC Information...";

                npcInfoBackgroundWorker.RunWorkerAsync();
            }
        }

        private void wizard_WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
        {
            if (e.OldPage == npcDetailsWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (NameTextBoxX.Text == String.Empty || entryIDIntegerInput.Value == 0 || displayIDIntegerInput.Value == 0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MySQLConnBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mysql != null)
                MySQLTestResult = mysql.TestMySQLConnection();
        }

        private void MySQLConnBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            wizardPage1.NextButtonEnabled = eWizardButtonState.True;

            MySQLConnProgressBarX.Enabled = false;
            MySQLConnProgressBarX.Visible = false;

            MySQLConnLabelX.Visible = false;
        }


        Dictionary<string, List<string>> npcInfo = null;

        private void npcInfoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (mysql != null)
                    npcInfo = mysql.ReadAll(Settings.Default.WorldDB, "creature_template", new string[] { "entry" }, new string[] { editNPCID.ToString() });
            }
            catch
            {
            }
        }

        private void npcInfoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MySQLConnProgressBarX.Enabled = false;
            MySQLConnProgressBarX.Visible = false;

            MySQLConnLabelX.Visible = false;

            if (npcInfo != null && npcInfo.ContainsKey("entry") && npcInfo["entry"].Count == 1)
            {
                wizardPage1.NextButtonEnabled = eWizardButtonState.True;

                entryIDIntegerInput.Value = int.Parse(npcInfo["entry"][0]);

                NameTextBoxX.Text = npcInfo["name"][0];
                subnameTextBoxX.Text = npcInfo["subname"][0];
                rankComboBoxEx.SelectedIndex = int.Parse(npcInfo["rank"][0]);
                displayIDIntegerInput.Value = int.Parse(npcInfo["modelid1"][0]);

                int faction = 0;

                switch (int.Parse(npcInfo["faction_A"][0]))
                {
                    case 7:
                        faction = 0;
                        break;
                    case 14:
                        faction = 1;
                        break;
                    case 35:
                        faction = 2;
                        break;
                }

                factionComboBoxEx.SelectedIndex = faction;
                typeComboBoxEx.SelectedIndex = int.Parse(npcInfo["type"][0]);
                familyComboItemEx.SelectedIndex = int.Parse(npcInfo["family"][0]);


                int unit_class = 0;

                switch (int.Parse(npcInfo["unit_class"][0]))
                {
                    case 1:
                        unit_class = 0;
                        break;
                    case 2:
                        unit_class = 1;
                        break;
                    case 4:
                        unit_class = 2;
                        break;
                    case 8:
                        unit_class = 3;
                        break;
                }

                classComboBoxEx.SelectedIndex = unit_class;
                modelSizeNumericUpDown.Value = int.Parse(npcInfo["scale"][0]);


                //Retrieve Class Level Stats in order to calculate health and armor


                Dictionary<string, List<string>> classLevelStats = mysql.ReadAll(Settings.Default.WorldDB, "creature_classlevelstats", new string[] { "level", "class" }, new string[] { npcInfo["maxlevel"][0], npcInfo["unit_class"][0] });

                if (!classLevelStats.ContainsKey("basehp2") || !classLevelStats.ContainsKey("basearmor"))
                {
                    TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", String.Empty, eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();

                    return;
                }


                int health_mod = int.Parse(classLevelStats["basehp2"][0]);
                int armor_mod = int.Parse(classLevelStats["basearmor"][0]);

                float health = 0;
                float armor = 0;

                health = float.Parse(npcInfo["Health_mod"][0]) * health_mod;
                armor = float.Parse(npcInfo["Armor_mod"][0]) * armor_mod;

                healthIntegerInput.Value = Convert.ToInt32(health);
                armorIntegerInput.Value = Convert.ToInt32(armor);
                manaIntegerInput.Value = int.Parse(npcInfo["Mana_mod"][0]);
                maxLevelIntegerInput.Value = int.Parse(npcInfo["maxlevel"][0]);

                attackPowerIntegerInput.Value = int.Parse(npcInfo["attackpower"][0]);
                minDamageIntegerInput.Value = int.Parse(npcInfo["mindmg"][0]);
                maxDamageIntegerInput.Value = int.Parse(npcInfo["maxdmg"][0]);

                int attackTime = Convert.ToInt32(TimeSpan.FromMilliseconds(Convert.ToDouble(npcInfo["baseattacktime"][0])).TotalSeconds);

                speedNumericUpDown.Value = attackTime;


                holyIntegerInput.Value = int.Parse(npcInfo["resistance1"][0]);
                fireIntegerInput.Value = int.Parse(npcInfo["resistance2"][0]);
                natureIntegerInput.Value = int.Parse(npcInfo["resistance3"][0]);
                frostIntegerInput.Value = int.Parse(npcInfo["resistance4"][0]);
                shadowIntegerInput.Value = int.Parse(npcInfo["resistance5"][0]);
                arcaneIntegerInput.Value = int.Parse(npcInfo["resistance6"][0]);


                if ((int.Parse(npcInfo["npcflag"][0]) & 1) != 0)
                    gossipYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 128) != 0)
                    vendorYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 4096) != 0)
                    armorerYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 65536) != 0)
                    innkeeperYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 131072) != 0)
                    bankerYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 2097152) != 0)
                    auctioneerYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 4194304) != 0)
                    stableYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 8388608) != 0)
                    bankerYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 2) != 0)
                    questGiverYesRadioButton.Checked = true;
                if ((int.Parse(npcInfo["npcflag"][0]) & 268435456) != 0)
                    guardYesRadioButton.Checked = true;


                int total = int.Parse(npcInfo["maxgold"][0]);

                int gold = total / 10000;

                total = total % 10000;

                int silver = total / 100;

                int copper = total % 100;

                lootGoldIntegerInput.Value = gold;
                lootSilverIntegerInput.Value = silver;
                lootCopperIntegerInput.Value = copper;


                int AI = 0;

                switch (npcInfo["AIName"][0])
                {
                    case "NullAI":
                        AI = 1;
                        break;
                    case "AggressorAI":
                        AI = 2;
                        break;
                    case "ReactorAI":
                        AI = 3;
                        break;
                }


                aiNameComboBoxEx.SelectedIndex = AI;
                movementTypeComboBoxEx.SelectedIndex = int.Parse(npcInfo["MovementType"][0]);
                inhabitComboBoxEx.SelectedIndex = int.Parse(npcInfo["InhabitType"][0]) - 1;

                if (int.Parse(npcInfo["RegenHealth"][0]) == 1)
                    regenHealthYesRadioButton.Checked = true;

                vehicleIDIntegerInput.Value = int.Parse(npcInfo["VehicleId"][0]);
                scriptNameTextBoxX.Text = npcInfo["ScriptName"][0];


            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "NPC not found!", eTaskDialogButton.Ok));

                finished = true;

                this.Close();
            }
        }

        private void wizard_FinishButtonClick(object sender, CancelEventArgs e)
        {
            try
            {

                int unit_class = 0;

                switch (classComboBoxEx.SelectedIndex)
                {
                    case 0:
                        unit_class = 1;
                        break;
                    case 1:
                        unit_class = 2;
                        break;
                    case 2:
                        unit_class = 4;
                        break;
                    case 3:
                        unit_class = 8;
                        break;
                }

                Dictionary<string, List<string>> classLevelStats = mysql.ReadAll(Settings.Default.WorldDB, "creature_classlevelstats", new string[] { "level", "class" }, new string[] { minLevelIntegerInput.Value.ToString(), unit_class.ToString() });

                if (!classLevelStats.ContainsKey("basehp2") || !classLevelStats.ContainsKey("basearmor"))
                {
                    TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", String.Empty, eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();

                    return;
                }

                int health = int.Parse(classLevelStats["basehp2"][0]);
                int armor = int.Parse(classLevelStats["basearmor"][0]);

                float health_mod = 0;

                if (healthIntegerInput.Value != 0 && health != 0)
                    health_mod = float.Parse(healthIntegerInput.Value.ToString()) / health;
                else if (health == 0)
                    health_mod = float.Parse(healthIntegerInput.Value.ToString());

                float armor_mod = 0;

                if (armorIntegerInput.Value != 0 && armor != 0)
                    armor_mod = float.Parse(armorIntegerInput.Value.ToString()) / armor;
                else if (armor == 0)
                    armor_mod = float.Parse(armorIntegerInput.Value.ToString());



                int faction = 0;

                switch (factionComboBoxEx.SelectedIndex)
                {
                    case 0:
                        faction = 7;
                        break;
                    case 1:
                        faction = 14;
                        break;
                    case 2:
                        faction = 35;
                        break;
                }


                int flags = 0;

                if (gossipYesRadioButton.Checked)
                    flags += 1;

                if (vendorYesRadioButton.Checked)
                    flags += 128;

                if (armorerYesRadioButton.Checked)
                    flags += 4096;

                if (innkeeperYesRadioButton.Checked)
                    flags += 65536;

                if (bankerYesRadioButton.Checked)
                    flags += 131072;

                if (auctioneerYesRadioButton.Checked)
                    flags += 2097152;

                if (stableYesRadioButton.Checked)
                    flags += 4194304;

                if (guildBankYesRadioButton.Checked)
                    flags += 8388608;

                if (questGiverYesRadioButton.Checked)
                    flags += 2;

                if (guardYesRadioButton.Checked)
                    flags += 268435456;


                int money = (lootCopperIntegerInput.Value % 100) + (lootSilverIntegerInput.Value * 100) + (lootGoldIntegerInput.Value * 10000);

                int regen = 0;

                if (regenHealthYesRadioButton.Checked)
                    regen = 1;

                string AIName = String.Empty;

                switch (aiNameComboBoxEx.SelectedIndex)
                {
                    case 1:
                        AIName = "NullAI";
                        break;
                    case 2:
                        AIName = "AggressorAI";
                        break;
                    case 3:
                        AIName = "ReactorAI";
                        break;
                }

                double attackTime = TimeSpan.FromSeconds(Convert.ToDouble(speedNumericUpDown.Value)).TotalMilliseconds;


                string npcName = NameTextBoxX.Text.Replace("'", "\\'");
                string npcSubname = subnameTextBoxX.Text.Replace("'", "\\'");

                string[] columns = new string[] { "entry", "modelid1", "name", "subname", "minlevel", "maxlevel", "exp", "faction_A", "faction_H", "npcflag", "scale", "rank", "mindmg", "maxdmg", "attackpower", "baseattacktime", "rangeattacktime", "unit_class", "family", "minrangedmg", "maxrangedmg", "rangedattackpower", "type", "resistance1", "resistance2", "resistance3", "resistance4", "resistance5", "resistance6", "VehicleId", "mingold", "maxgold", "AIName", "MovementType", "InhabitType", "Health_mod", "Mana_mod", "Armor_mod", "RegenHealth", "ScriptName" };
                string[] values = new string[] { entryIDIntegerInput.Value.ToString(), displayIDIntegerInput.Value.ToString(), npcName, npcSubname, minLevelIntegerInput.Value.ToString(), maxLevelIntegerInput.Value.ToString(),
                    "2", faction.ToString(), faction.ToString(), flags.ToString(), modelSizeNumericUpDown.Value.ToString(), rankComboBoxEx.SelectedIndex.ToString(), minDamageIntegerInput.Value.ToString(), maxDamageIntegerInput.Value.ToString(), attackPowerIntegerInput.Value.ToString(), attackTime.ToString(), 
                    attackTime.ToString(), unit_class.ToString(), familyComboItemEx.SelectedIndex.ToString(), minDamageIntegerInput.Value.ToString(), maxDamageIntegerInput.Value.ToString(), attackPowerIntegerInput.Value.ToString(), typeComboBoxEx.SelectedIndex.ToString(), holyIntegerInput.Value.ToString(), fireIntegerInput.Value.ToString(), natureIntegerInput.Value.ToString(), frostIntegerInput.Value.ToString(), shadowIntegerInput.Value.ToString(), arcaneIntegerInput.Value.ToString(), vehicleIDIntegerInput.Value.ToString(), money.ToString(), money.ToString(), AIName, movementTypeComboBoxEx.SelectedIndex.ToString(), (inhabitComboBoxEx.SelectedIndex + 1).ToString(), health_mod.ToString(), manaIntegerInput.Value.ToString(), armor_mod.ToString(), regen.ToString(), scriptNameTextBoxX.Text };


                finished = true;

                if (saveToDBRadioButton.Checked)
                {
                    mysql.ReplaceIntoDatabase(Settings.Default.WorldDB, "creature_template", values, columns);

                    TaskDialog.Show(new TaskDialogInfo("Done", eTaskDialogIcon.Information2, "NPC imported into database", String.Empty, eTaskDialogButton.Ok));

                    this.Close();
                }
                else
                {
                    if (saveSQLFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        string qry = mysql.CreateMySQLQuery(Settings.Default.WorldDB, "creature_template", values, columns);

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

        private void NPCCreator_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
