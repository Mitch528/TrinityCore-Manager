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
    public partial class WeaponCreator : DevComponents.DotNetBar.Office2007Form
    {
        public WeaponCreator()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        private bool MySQLTestResult = false;


        public bool editor = false;
        public int editItemID = 0;

        private bool finished = false;

        private void WeaponCreator_Load(object sender, EventArgs e)
        {


            //Set Default Values

            qualityComboBoxEx.SelectedIndex = 0;
            equipComboBoxEx.SelectedIndex = 0;
            weaponTypeComboBoxEx.SelectedIndex = 0;
            bindsComboBoxEx.SelectedIndex = 0;
            sheathComboBoxEx.SelectedIndex = 0;
            ammoTypeComboBoxEx.SelectedIndex = 0;

            damageTypeComboBox.SelectedIndex = 0;
            damageTypeComboBoxEx2.SelectedIndex = 0;


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
                MySQLConnLabelX.Text = "Getting Weapon Information...";

                weaponInfoBackgroundWorker.RunWorkerAsync();
            }
        }

        private void wizard_WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
        {
            if (e.OldPage == weaponDetailsWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (weaponNameTextBoxX.Text == String.Empty || entryIDIntegerInput.Value == 0 || displayIDIntegerInput.Value == 0)
                    e.Cancel = true;
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


        Dictionary<string, List<string>> weaponInfo = null;

        private void weaponInfoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (mysql != null)
                    weaponInfo = mysql.ReadAll(Settings.Default.WorldDB, "item_template", new string[] { "entry" }, new string[] { editItemID.ToString() });
            }
            catch
            {
            }
        }

        private void weaponInfoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MySQLConnProgressBarX.Enabled = false;
            MySQLConnProgressBarX.Visible = false;

            MySQLConnLabelX.Visible = false;

            if (weaponInfo != null && weaponInfo.ContainsKey("entry") && weaponInfo["entry"].Count == 1)
            {
                if (int.Parse(weaponInfo["class"][0]) == 2)
                {

                    wizardPage1.NextButtonEnabled = eWizardButtonState.True;

                    entryIDIntegerInput.Value = int.Parse(weaponInfo["entry"][0]);
                    weaponNameTextBoxX.Text = weaponInfo["name"][0];
                    quoteTextBoxX.Text = weaponInfo["description"][0];
                    qualityComboBoxEx.SelectedIndex = int.Parse(weaponInfo["Quality"][0]);
                    displayIDIntegerInput.Value = int.Parse(weaponInfo["displayid"][0]);
                    
                    switch (int.Parse(weaponInfo["subclass"][0]))
                    {
                        case 10:
                            weaponTypeComboBoxEx.SelectedIndex = 9;
                            break;
                        case 13:
                            weaponTypeComboBoxEx.SelectedIndex = 10;
                            break;
                        case 15:
                            weaponTypeComboBoxEx.SelectedIndex = 11;
                            break;
                        case 16:
                            weaponTypeComboBoxEx.SelectedIndex = 12;
                            break;
                        case 17:
                            weaponTypeComboBoxEx.SelectedIndex = 13;
                            break;
                        case 18:
                            weaponTypeComboBoxEx.SelectedIndex = 14;
                            break;
                        case 19:
                            weaponTypeComboBoxEx.SelectedIndex = 15;
                            break;
                        default:
                            weaponTypeComboBoxEx.SelectedIndex = int.Parse(weaponInfo["subclass"][0]);
                            break;
                    }
                    
                    switch (int.Parse(weaponInfo["InventoryType"][0]))
                    {
                        case 13:
                            equipComboBoxEx.SelectedIndex = 0;
                            break;
                        case 21:
                            equipComboBoxEx.SelectedIndex = 1;
                            break;
                        case 22:
                            equipComboBoxEx.SelectedIndex = 2;
                            break;
                        case 17:
                            equipComboBoxEx.SelectedIndex = 3;
                            break;
                        case 15:
                            equipComboBoxEx.SelectedIndex = 4;
                            break;
                        case 25:
                            equipComboBoxEx.SelectedIndex = 5;
                            break;
                        case 26:
                            equipComboBoxEx.SelectedIndex = 6;
                            break;
                        default:
                            equipComboBoxEx.SelectedIndex = 7;
                            break;
                    }


                    durabilityIntegerInput.Value = int.Parse(weaponInfo["MaxDurability"][0]);
                    bindsComboBoxEx.SelectedIndex = int.Parse(weaponInfo["bonding"][0]);
                    
                    switch (int.Parse(weaponInfo["sheath"][0]))
                    {
                        case 3:
                            sheathComboBoxEx.SelectedIndex = 0;
                            break;
                        case 1:
                            sheathComboBoxEx.SelectedIndex = 1;
                            break;
                        case 2:
                            sheathComboBoxEx.SelectedIndex = 2;
                            break;
                    }

                    ammoTypeComboBoxEx.SelectedIndex = int.Parse(weaponInfo["ammo_type"][0]);
                    weaponSpeedNumericUpDown.Value = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(weaponInfo["delay"][0])).TotalSeconds);


                    blockIntegerInput.Value = int.Parse(weaponInfo["block"][0]);
                    armorIntegerInput.Value = int.Parse(weaponInfo["armor"][0]);

                    int statValueCtr = 0;

                    if (int.Parse(weaponInfo["stat_type1"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type2"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type3"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type4"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type5"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type6"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type7"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type8"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type9"][0]) != 0)
                        statValueCtr += 1;
                    if (int.Parse(weaponInfo["stat_type10"][0]) != 0)
                        statValueCtr += 1;


                    for (int i = 1; i < statValueCtr; i++)
                        AddStatInfo();


                    List<Control> ctrlList = new List<Control>();

                    foreach (Control ctrl in itemStatsGroupPanel.Controls)
                        ctrlList.Add(ctrl);

                    ctrlList.Reverse();


                    int z = 1;
                    int w = 1;

                    foreach (Control c in itemStatsGroupPanel.Controls)
                    {
                        if (c is DevComponents.DotNetBar.Controls.ComboBoxEx)
                        {
                            if (c.Name.Contains("statTypeComboBoxEx"))
                            {
                                for (int x = 0; x < 9; x++)
                                {
                                    if (c.Name == String.Format("statTypeComboBoxEx{0}", x + 1))
                                    {
                                        int statType = 0;

                                        switch (int.Parse(weaponInfo[String.Format("stat_type{0}", z)][0]))
                                        {
                                            case 1:
                                                statType = 0;
                                                break;
                                            case 3:
                                                statType = 1;
                                                break;
                                            case 4:
                                                statType = 2;
                                                break;
                                            case 5:
                                                statType = 3;
                                                break;
                                            case 6:
                                                statType = 4;
                                                break;
                                            case 7:
                                                statType = 5;
                                                break;
                                            case 12:
                                                statType = 6;
                                                break;
                                            case 13:
                                                statType = 7;
                                                break;
                                            case 14:
                                                statType = 8;
                                                break;
                                            case 15:
                                                statType = 9;
                                                break;
                                            case 37:
                                                statType = 10;
                                                break;
                                            case 35:
                                                statType = 11;
                                                break;
                                            case 41:
                                                statType = 12;
                                                break;
                                            case 42:
                                                statType = 13;
                                                break;
                                            case 0:
                                                statType = 14;
                                                break;
                                            case 48:
                                                statType = 15;
                                                break;
                                            case 46:
                                                statType = 16;
                                                break;
                                            case 47:
                                                statType = 17;
                                                break;
                                            case 44:
                                                statType = 18;
                                                break;
                                            case 38:
                                                statType = 19;
                                                break;
                                            case 31:
                                                statType = 20;
                                                break;
                                            case 33:
                                                statType = 21;
                                                break;
                                            case 32:
                                                statType = 22;
                                                break;
                                            case 34:
                                                statType = 23;
                                                break;
                                            case 36:
                                                statType = 24;
                                                break;
                                            case 16:
                                                statType = 25;
                                                break;
                                            case 19:
                                                statType = 26;
                                                break;
                                            case 22:
                                                statType = 27;
                                                break;
                                            case 25:
                                                statType = 28;
                                                break;
                                            case 28:
                                                statType = 29;
                                                break;
                                            case 39:
                                                statType = 30;
                                                break;
                                            case 17:
                                                statType = 31;
                                                break;
                                            case 20:
                                                statType = 32;
                                                break;
                                            case 23:
                                                statType = 33;
                                                break;
                                            case 26:
                                                statType = 34;
                                                break;
                                            case 29:
                                                statType = 35;
                                                break;
                                            case 18:
                                                statType = 36;
                                                break;
                                            case 21:
                                                statType = 37;
                                                break;
                                            case 24:
                                                statType = 38;
                                                break;
                                            case 27:
                                                statType = 39;
                                                break;
                                            case 30:
                                                statType = 40;
                                                break;
                                            case 45:
                                                statType = 41;
                                                break;
                                            case 40:
                                                statType = 42;
                                                break;
                                        }

                                        DevComponents.DotNetBar.Controls.ComboBoxEx combobox = (DevComponents.DotNetBar.Controls.ComboBoxEx)c;

                                        combobox.SelectedIndex = statType;

                                        z++;

                                    }
                                }
                            }
                        }
                        else if (c is DevComponents.Editors.IntegerInput)
                        {
                            for (int x = 0; x < 9; x++)
                            {
                                if (c.Name == String.Format("statTypeValueIntegerInput{0}", x + 1))
                                {
                                    DevComponents.Editors.IntegerInput integerInput = (DevComponents.Editors.IntegerInput)c;

                                    integerInput.Value = int.Parse(weaponInfo[String.Format("stat_value{0}", w)][0]);
                                }
                            }

                            w++;
                        }
                    }


                    requiredLevelIntegerInput.Value = int.Parse(weaponInfo["RequiredLevel"][0]);
                    itemLevelIntegerInput.Value = int.Parse(weaponInfo["ItemLevel"][0]);
                    maxAllowedIntegerInput.Value = int.Parse(weaponInfo["maxcount"][0]);
                    stackableIntegerInput.Value = int.Parse(weaponInfo["stackable"][0]);


                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 1) != 0)
                        warriorCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 2) != 0)
                        paladinCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 4) != 0)
                        hunterCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 8) != 0)
                        rogueCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 16) != 0)
                        priestCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 32) != 0)
                        deathKnightCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 64) != 0)
                        shamanCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 128) != 0)
                        mageCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 256) != 0)
                        warlockCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableClass"][0]) & 1024) != 0)
                        druidCheckBoxX.Checked = true;

                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 1) != 0)
                        humanCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 2) != 0)
                        orcCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 4) != 0)
                        dwarfCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 8) != 0)
                        nightElfCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 16) != 0)
                        undeadCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 32) != 0)
                        taurenCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 64) != 0)
                        gnomeCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 128) != 0)
                        trollCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 512) != 0)
                        bloodElfCheckBoxX.Checked = true;
                    if ((int.Parse(weaponInfo["AllowableRace"][0]) & 1024) != 0)
                        draeneiCheckBoxX.Checked = true;


                    damageMinIntegerInput.Value = int.Parse(weaponInfo["dmg_min1"][0]);
                    damageMaxIntegerInput.Value = int.Parse(weaponInfo["dmg_max1"][0]);
                    damageTypeComboBox.SelectedIndex = int.Parse(weaponInfo["dmg_type1"][0]);

                    damageMinIntegerInput2.Value = int.Parse(weaponInfo["dmg_min2"][0]);
                    damageMaxIntegerInput2.Value = int.Parse(weaponInfo["dmg_max2"][0]);
                    damageTypeComboBoxEx2.SelectedIndex = int.Parse(weaponInfo["dmg_type1"][0]);

                    holyIntegerInput.Value = int.Parse(weaponInfo["holy_res"][0]);
                    natureIntegerInput.Value = int.Parse(weaponInfo["nature_res"][0]);
                    shadowIntegerInput.Value = int.Parse(weaponInfo["shadow_res"][0]);
                    fireIntegerInput.Value = int.Parse(weaponInfo["fire_res"][0]);
                    frostIntegerInput.Value = int.Parse(weaponInfo["frost_res"][0]);
                    arcaneIntegerInput.Value = int.Parse(weaponInfo["arcane_res"][0]);

                    switch (int.Parse(weaponInfo["socketColor_1"][0]))
                    {
                        case 1:
                            socket1MetaButtonX.Enabled = false;
                            break;
                        case 2:
                            socket1RedButtonX.Enabled = false;
                            break;
                        case 4:
                            socket1YellowButtonX.Enabled = false;
                            break;
                        case 8:
                            socket1BlueButtonX.Enabled = false;
                            break;
                        default:
                            socket1NoneButtonX.Enabled = false;
                            break;
                    }

                    switch (int.Parse(weaponInfo["socketColor_2"][0]))
                    {
                        case 1:
                            socket2MetaButtonX.Enabled = false;
                            break;
                        case 2:
                            socket2RedButtonX.Enabled = false;
                            break;
                        case 4:
                            socket2YellowButtonX.Enabled = false;
                            break;
                        case 8:
                            socket2BlueButtonX.Enabled = false;
                            break;
                        default:
                            socket2NoneButtonX.Enabled = false;
                            break;
                    }

                    switch (int.Parse(weaponInfo["socketColor_3"][0]))
                    {
                        case 1:
                            socket3MetaButtonX.Enabled = false;
                            break;
                        case 2:
                            socket3RedButtonX.Enabled = false;
                            break;
                        case 4:
                            socket3YellowButtonX.Enabled = false;
                            break;
                        case 8:
                            socket3BlueButtonX.Enabled = false;
                            break;
                        default:
                            socket3NoneButtonX.Enabled = false;
                            break;
                    }

                    switch (int.Parse(weaponInfo["socketBonus"][0]))
                    {
                        case 3312:
                            bonusComboBoxEx.SelectedIndex = 1;
                            break;
                        case 3313:
                            bonusComboBoxEx.SelectedIndex = 2;
                            break;
                        case 3305:
                            bonusComboBoxEx.SelectedIndex = 3;
                            break;
                        case 3353:
                            bonusComboBoxEx.SelectedIndex = 4;
                            break;
                        case 2872:
                            bonusComboBoxEx.SelectedIndex = 5;
                            break;
                        case 3853:
                            bonusComboBoxEx.SelectedIndex = 6;
                            break;
                        case 3356:
                            bonusComboBoxEx.SelectedIndex = 7;
                            break;
                        default:
                            bonusComboBoxEx.SelectedIndex = 0;
                            break;
                    }

                    int buyTotal = int.Parse(weaponInfo["BuyPrice"][0]);

                    int gold = buyTotal / 10000;

                    buyTotal = buyTotal % 10000;

                    int silver = buyTotal / 100;

                    int copper = buyTotal % 100;

                    buyGoldIntegerInput.Value = gold;
                    buySilverIntegerInput.Value = silver;
                    buyCopperIntegerInput.Value = copper;


                    buyTotal = int.Parse(weaponInfo["SellPrice"][0]);

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
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "This entry id does not belong to a weapon!", eTaskDialogButton.Ok));

                    finished = true;

                    this.Close();

                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Weapon not found!", eTaskDialogButton.Ok));

                finished = true;

                this.Close();
            }
        }

        private void searchDisplayIDsButtonX_Click(object sender, EventArgs e)
        {
            SearchItemID searchWeaponID = new SearchItemID();
            searchWeaponID.FormClosed += new FormClosedEventHandler(searchWeaponID_FormClosed);

            searchWeaponID.searchType = SearchItemID.SearchType.Weapon;

            searchWeaponID.ShowDialog();
        }

        private void searchWeaponID_FormClosed(object sender, FormClosedEventArgs e)
        {
            SearchItemID searchWeaponID = (SearchItemID)sender;

            displayIDIntegerInput.Value = searchWeaponID.GetDisplayID();
        }

        private int lastLocY = 20;
        private int addCtr = 2;

        private void addButtonX_Click(object sender, EventArgs e)
        {
            AddStatInfo();
        }

        private void AddStatInfo()
        {
            if (addCtr == 11)
                return;

            LabelX newStatTypeLabelX = new LabelX();
            newStatTypeLabelX.BackColor = Color.Transparent;

            lastLocY += 35;


            //Stat Type Label

            newStatTypeLabelX.BackgroundStyle.Class = "";
            newStatTypeLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            newStatTypeLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newStatTypeLabelX.Location = new System.Drawing.Point(13, lastLocY);
            newStatTypeLabelX.Name = "statTypeLabelX" + addCtr;
            newStatTypeLabelX.Size = new System.Drawing.Size(59, 23);
            newStatTypeLabelX.TabIndex = 8 + addCtr;
            newStatTypeLabelX.Text = "Stat Type";

            addButtonX.Location = new Point(addButtonX.Location.X, lastLocY + 25);


            itemStatsGroupPanel.Controls.Add(newStatTypeLabelX);


            //Stat Type ComboBoxEx

            DevComponents.DotNetBar.Controls.ComboBoxEx newStatTypeComboBoxEx = new DevComponents.DotNetBar.Controls.ComboBoxEx();

            foreach (DevComponents.Editors.ComboItem item in statTypeComboBoxEx1.Items)
            {
                newStatTypeComboBoxEx.Items.Add(item.Text);
            }

            newStatTypeComboBoxEx.DropDownStyle = ComboBoxStyle.DropDownList;
            newStatTypeComboBoxEx.DisplayMember = "Text";
            newStatTypeComboBoxEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            newStatTypeComboBoxEx.FormattingEnabled = true;
            newStatTypeComboBoxEx.ItemHeight = 14;
            newStatTypeComboBoxEx.Location = new System.Drawing.Point(79, lastLocY);
            newStatTypeComboBoxEx.Name = "statTypeComboBoxEx" + addCtr;
            newStatTypeComboBoxEx.Size = new System.Drawing.Size(152, 20);
            newStatTypeComboBoxEx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            newStatTypeComboBoxEx.TabIndex = 9 + addCtr;


            itemStatsGroupPanel.Controls.Add(newStatTypeComboBoxEx);


            //Stat Type Value Label

            LabelX newStatValueLabelX = new LabelX();

            newStatValueLabelX.BackColor = Color.Transparent;
            newStatValueLabelX.BackgroundStyle.Class = "";
            newStatValueLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            newStatValueLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newStatValueLabelX.Location = new System.Drawing.Point(237, lastLocY);
            newStatValueLabelX.Name = "statTypeValueLabelX" + addCtr;
            newStatValueLabelX.Size = new System.Drawing.Size(39, 23);
            newStatValueLabelX.TabIndex = 10 + addCtr;
            newStatValueLabelX.Text = "Value";

            itemStatsGroupPanel.Controls.Add(newStatValueLabelX);


            //Stat Type Value IntegerInput

            DevComponents.Editors.IntegerInput newStatTypeValueIntegerInput = new DevComponents.Editors.IntegerInput();

            newStatTypeValueIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            newStatTypeValueIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            newStatTypeValueIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            newStatTypeValueIntegerInput.Location = new System.Drawing.Point(283, lastLocY);
            newStatTypeValueIntegerInput.Name = "statTypeValueIntegerInput1";
            newStatTypeValueIntegerInput.ShowUpDown = true;
            newStatTypeValueIntegerInput.Size = new System.Drawing.Size(80, 20);
            newStatTypeValueIntegerInput.TabIndex = 11;


            itemStatsGroupPanel.Controls.Add(newStatTypeValueIntegerInput);


            addCtr++;
        }

        private void wizard_FinishButtonClick(object sender, CancelEventArgs e)
        {

            try
            {
                int sheath = 0;
                int subclass = 0;
                int inventorytype = 0;
                int socket_bonus = 0;

                int weapon_speed = Convert.ToInt32(TimeSpan.FromSeconds(Convert.ToDouble(weaponSpeedNumericUpDown.Value)).TotalMilliseconds);
                int buymoney = (Convert.ToInt32(buyCopperIntegerInput.Value) % 100) + (Convert.ToInt32(buySilverIntegerInput.Value) * 100) + (Convert.ToInt32(buyGoldIntegerInput.Value) * 10000);
                int sellmoney = (Convert.ToInt32(sellCopperIntegerInput.Value) % 100) + (Convert.ToInt32(sellSilverIntegerInput.Value) * 100) + (Convert.ToInt32(sellGoldIntegerInput.Value) * 10000);

                switch (weaponTypeComboBoxEx.SelectedIndex)
                {
                    case 9:
                        subclass = 10;
                        break;
                    case 10:
                        subclass = 13;
                        break;
                    case 11:
                        subclass = 15;
                        break;
                    case 12:
                        subclass = 16;
                        break;
                    case 13:
                        subclass = 17;
                        break;
                    case 14:
                        subclass = 18;
                        break;
                    case 15:
                        subclass = 19;
                        break;
                    default:
                        subclass = weaponTypeComboBoxEx.SelectedIndex;
                        break;
                }

                switch (equipComboBoxEx.SelectedIndex)
                {
                    case 0:
                        inventorytype = 13;
                        break;
                    case 1:
                        inventorytype = 21;
                        break;
                    case 2:
                        inventorytype = 22;
                        break;
                    case 3:
                        inventorytype = 17;
                        break;
                    case 4:
                        inventorytype = 15;
                        break;
                    case 5:
                        inventorytype = 25;
                        break;
                    case 6:
                        inventorytype = 26;
                        break;
                    case 7:
                        inventorytype = 26;
                        break;
                    default:
                        inventorytype = 13;
                        break;
                }

                switch (sheathComboBoxEx.SelectedIndex)
                {
                    case 0:
                        sheath = 3;
                        break;
                    case 1:
                        sheath = 1;
                        break;
                    case 2:
                        sheath = 2;
                        break;
                    case 3:
                        sheath = 0;
                        break;
                    case 4:
                        sheath = 0;
                        break;
                }

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

                if (bloodElfCheckBoxX.Checked)
                    AllowableRaces.Add(512);

                if (draeneiCheckBoxX.Checked)
                    AllowableRaces.Add(1024);

                foreach (int p in AllowableRaces)
                    allowablerace += p;

                if (allowablerace == 0)
                    allowablerace = -1;


                switch (bonusComboBoxEx.SelectedIndex)
                {
                    case 1:
                        socket_bonus = 3312;
                        break;
                    case 2:
                        socket_bonus = 3313;
                        break;
                    case 3:
                        socket_bonus = 3305;
                        break;
                    case 4:
                        socket_bonus = 3353;
                        break;
                    case 5:
                        socket_bonus = 2872;
                        break;
                    case 6:
                        socket_bonus = 3853;
                        break;
                    case 7:
                        socket_bonus = 3356;
                        break;
                }

                int socket_1 = 0;
                int socket_2 = 0;
                int socket_3 = 0;

                if (!socket1BlueButtonX.Enabled)
                    socket_1 = 8;
                else if (!socket1RedButtonX.Enabled)
                    socket_1 = 2;
                else if (!socket1YellowButtonX.Enabled)
                    socket_1 = 4;
                else if (!socket1MetaButtonX.Enabled)
                    socket_1 = 1;

                if (!socket2BlueButtonX.Enabled)
                    socket_2 = 8;
                else if (!socket2RedButtonX.Enabled)
                    socket_2 = 2;
                else if (!socket2YellowButtonX.Enabled)
                    socket_2 = 4;
                else if (!socket2MetaButtonX.Enabled)
                    socket_2 = 1;

                if (!socket3BlueButtonX.Enabled)
                    socket_3 = 8;
                else if (!socket3RedButtonX.Enabled)
                    socket_3 = 2;
                else if (!socket3YellowButtonX.Enabled)
                    socket_3 = 4;
                else if (!socket3MetaButtonX.Enabled)
                    socket_3 = 1;

                int statType1 = 0;
                int statType2 = 0;
                int statType3 = 0;
                int statType4 = 0;
                int statType5 = 0;
                int statType6 = 0;
                int statType7 = 0;
                int statType8 = 0;
                int statType9 = 0;
                int statType10 = 0;

                int statValue1 = 0;
                int statValue2 = 0;
                int statValue3 = 0;
                int statValue4 = 0;
                int statValue5 = 0;
                int statValue6 = 0;
                int statValue7 = 0;
                int statValue8 = 0;
                int statValue9 = 0;
                int statValue10 = 0;

                int i = 0;
                int w = 0;

                foreach (Control c in itemStatsGroupPanel.Controls)
                {

                    if (c is DevComponents.DotNetBar.Controls.ComboBoxEx && c.Name.Contains("statTypeComboBoxEx"))
                    {

                        DevComponents.DotNetBar.Controls.ComboBoxEx combobox = (DevComponents.DotNetBar.Controls.ComboBoxEx)c;

                        int statType = 0;

                        switch (combobox.SelectedIndex)
                        {
                            case 0:
                                statType = 1;
                                break;
                            case 1:
                                statType = 3;
                                break;
                            case 2:
                                statType = 4;
                                break;
                            case 3:
                                statType = 5;
                                break;
                            case 4:
                                statType = 6;
                                break;
                            case 5:
                                statType = 7;
                                break;
                            case 6:
                                statType = 12;
                                break;
                            case 7:
                                statType = 13;
                                break;
                            case 8:
                                statType = 14;
                                break;
                            case 9:
                                statType = 15;
                                break;
                            case 10:
                                statType = 37;
                                break;
                            case 11:
                                statType = 35;
                                break;
                            case 12:
                                statType = 41;
                                break;
                            case 13:
                                statType = 42;
                                break;
                            case 14:
                                statType = 0;
                                break;
                            case 15:
                                statType = 48;
                                break;
                            case 16:
                                statType = 46;
                                break;
                            case 17:
                                statType = 47;
                                break;
                            case 18:
                                statType = 44;
                                break;
                            case 19:
                                statType = 38;
                                break;
                            case 20:
                                statType = 31;
                                break;
                            case 21:
                                statType = 33;
                                break;
                            case 22:
                                statType = 32;
                                break;
                            case 23:
                                statType = 34;
                                break;
                            case 24:
                                statType = 36;
                                break;
                            case 25:
                                statType = 16;
                                break;
                            case 26:
                                statType = 19;
                                break;
                            case 27:
                                statType = 22;
                                break;
                            case 28:
                                statType = 25;
                                break;
                            case 29:
                                statType = 28;
                                break;
                            case 30:
                                statType = 39;
                                break;
                            case 31:
                                statType = 17;
                                break;
                            case 32:
                                statType = 20;
                                break;
                            case 33:
                                statType = 23;
                                break;
                            case 34:
                                statType = 26;
                                break;
                            case 35:
                                statType = 29;
                                break;
                            case 36:
                                statType = 18;
                                break;
                            case 37:
                                statType = 21;
                                break;
                            case 38:
                                statType = 24;
                                break;
                            case 39:
                                statType = 27;
                                break;
                            case 40:
                                statType = 30;
                                break;
                            case 41:
                                statType = 45;
                                break;
                            case 42:
                                statType = 40;
                                break;
                        }

                        if (i == 0)
                            statType1 = statType;
                        else if (i == 1)
                            statType2 = statType;
                        else if (i == 2)
                            statType3 = statType;
                        else if (i == 3)
                            statType4 = statType;
                        else if (i == 4)
                            statType5 = statType;
                        else if (i == 5)
                            statType6 = statType;
                        else if (i == 6)
                            statType7 = statType;
                        else if (i == 7)
                            statType8 = statType;
                        else if (i == 8)
                            statType9 = statType;
                        else if (i == 9)
                            statType10 = statType;

                        i++;
                    }
                    else if (c is DevComponents.Editors.IntegerInput && c.Name.Contains("statTypeValueIntegerInput"))
                    {
                        DevComponents.Editors.IntegerInput integerInput = (DevComponents.Editors.IntegerInput)c;

                        int intInput = integerInput.Value;

                        if (w == 0)
                            statValue1 = intInput;
                        else if (w == 1)
                            statValue2 = intInput;
                        else if (w == 2)
                            statValue3 = intInput;
                        else if (w == 3)
                            statValue4 = intInput;
                        else if (w == 4)
                            statValue5 = intInput;
                        else if (w == 5)
                            statValue6 = intInput;
                        else if (w == 6)
                            statValue7 = intInput;
                        else if (w == 7)
                            statValue8 = intInput;
                        else if (w == 8)
                            statValue9 = intInput;
                        else if (w == 9)
                            statValue10 = intInput;

                        w++;
                    }
                }

                int statsAmount = 0;

                if (statType1 != 0)
                    statsAmount += 1;

                if (statType2 != 0)
                    statsAmount += 1;

                if (statType3 != 0)
                    statsAmount += 1;

                if (statType4 != 0)
                    statsAmount += 1;

                if (statType5 != 0)
                    statsAmount += 1;

                if (statType6 != 0)
                    statsAmount += 1;

                if (statType7 != 0)
                    statsAmount += 1;

                if (statType8 != 0)
                    statsAmount += 1;

                if (statType9 != 0)
                    statsAmount += 1;

                if (statType10 != 0)
                    statsAmount += 1;


                string weaponName = weaponNameTextBoxX.Text.Replace("'", "\\'");
                string weaponQuote = quoteTextBoxX.Text.Replace("'", "\\'");


                string[] columns = new string[] { "entry", "class", "subclass", "name", "displayid", "Quality", "BuyPrice", "SellPrice", "InventoryType", "AllowableClass", "AllowableRace", "ItemLevel", "RequiredLevel", "maxcount", "stackable", "StatsCount", "stat_type1", "stat_value1", "stat_type2", "stat_value2", "stat_type3", "stat_value3", "stat_type4", "stat_value4", "stat_type5", "stat_value5", "stat_type6", "stat_value6", "stat_type7", "stat_value7", "stat_type8", "stat_value8", "stat_type9", "stat_value9", "stat_type10", "stat_value10", "dmg_min1", "dmg_max1", "dmg_type1", "dmg_min2", "dmg_max2", "dmg_type2", "armor", "holy_res", "fire_res", "nature_res", "frost_res", "shadow_res", "arcane_res", "delay", "ammo_type", "bonding", "description", "sheath", "block", "MaxDurability", "socketColor_1", "socketColor_2", "socketColor_3", "socketBonus" };
                string[] values = new string[] { entryIDIntegerInput.Value.ToString(), "2", subclass.ToString(), weaponName, displayIDIntegerInput.Value.ToString(), qualityComboBoxEx.SelectedIndex.ToString(), buymoney.ToString(), sellmoney.ToString(), inventorytype.ToString(), allowableclass.ToString(), allowablerace.ToString(), itemLevelIntegerInput.Value.ToString(), requiredLevelIntegerInput.Value.ToString(), maxAllowedIntegerInput.Value.ToString(), stackableIntegerInput.Value.ToString(), statsAmount.ToString(), statType1.ToString(), statValue1.ToString(), statType2.ToString(), statValue2.ToString(), statType3.ToString(), statValue3.ToString(), statType4.ToString(), statValue4.ToString(), statType5.ToString(), statValue5.ToString(), statType6.ToString(), statValue6.ToString(), statType7.ToString(), statValue7.ToString(), statType8.ToString(), statValue8.ToString(), statType9.ToString(), statValue9.ToString(), statType10.ToString(), statValue10.ToString(), damageMinIntegerInput.Value.ToString(), damageMaxIntegerInput.Value.ToString(), damageTypeComboBox.SelectedIndex.ToString(), damageMinIntegerInput2.Value.ToString(), damageMaxIntegerInput2.Value.ToString(), damageTypeComboBoxEx2.SelectedIndex.ToString(), armorIntegerInput.Value.ToString(), holyIntegerInput.Value.ToString(), fireIntegerInput.Value.ToString(), natureIntegerInput.Value.ToString(), frostIntegerInput.Value.ToString(), shadowIntegerInput.Value.ToString(), arcaneIntegerInput.Value.ToString(), weapon_speed.ToString(), ammoTypeComboBoxEx.SelectedIndex.ToString(), bindsComboBoxEx.SelectedIndex.ToString(), weaponQuote, sheath.ToString(), blockIntegerInput.Value.ToString(), durabilityIntegerInput.Value.ToString(), socket_1.ToString(), socket_2.ToString(), socket_3.ToString(), socket_bonus.ToString() };


                finished = true;

                if (saveToDBRadioButton.Checked)
                {
                    mysql.ReplaceIntoDatabase(Settings.Default.WorldDB, "item_template", values, columns);

                    TaskDialog.Show(new TaskDialogInfo("Done", eTaskDialogIcon.Information2, "Weapon imported into database", String.Empty, eTaskDialogButton.Ok));

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

        private void socket1NoneButtonX_Click(object sender, EventArgs e)
        {
            socket1NoneButtonX.Enabled = false;

            socket1BlueButtonX.Enabled = true;
            socket1RedButtonX.Enabled = true;
            socket1YellowButtonX.Enabled = true;
            socket1MetaButtonX.Enabled = true;
        }

        private void socket1BlueButtonX_Click(object sender, EventArgs e)
        {
            socket1BlueButtonX.Enabled = false;

            socket1NoneButtonX.Enabled = true;
            socket1RedButtonX.Enabled = true;
            socket1YellowButtonX.Enabled = true;
            socket1MetaButtonX.Enabled = true;
        }

        private void socket1RedButtonX_Click(object sender, EventArgs e)
        {
            socket1RedButtonX.Enabled = false;

            socket1NoneButtonX.Enabled = true;
            socket1BlueButtonX.Enabled = true;
            socket1YellowButtonX.Enabled = true;
            socket1MetaButtonX.Enabled = true;
        }

        private void socket1YellowButtonX_Click(object sender, EventArgs e)
        {
            socket1YellowButtonX.Enabled = false;

            socket1NoneButtonX.Enabled = true;
            socket1BlueButtonX.Enabled = true;
            socket1RedButtonX.Enabled = true;
            socket1MetaButtonX.Enabled = true;
        }

        private void socket1MetaButtonX_Click(object sender, EventArgs e)
        {
            socket1MetaButtonX.Enabled = false;

            socket1BlueButtonX.Enabled = true;
            socket1RedButtonX.Enabled = true;
            socket1YellowButtonX.Enabled = true;
            socket1MetaButtonX.Enabled = true;
        }

        private void socket2NoneButtonX_Click(object sender, EventArgs e)
        {
            socket2NoneButtonX.Enabled = false;

            socket2BlueButtonX.Enabled = true;
            socket2RedButtonX.Enabled = true;
            socket2YellowButtonX.Enabled = true;
            socket2MetaButtonX.Enabled = true;
        }

        private void socket2BlueButtonX_Click(object sender, EventArgs e)
        {
            socket2BlueButtonX.Enabled = false;

            socket2NoneButtonX.Enabled = true;
            socket2RedButtonX.Enabled = true;
            socket2YellowButtonX.Enabled = true;
            socket2MetaButtonX.Enabled = true;
        }

        private void socket2RedButtonX_Click(object sender, EventArgs e)
        {
            socket2RedButtonX.Enabled = false;

            socket2NoneButtonX.Enabled = true;
            socket2BlueButtonX.Enabled = true;
            socket2YellowButtonX.Enabled = true;
            socket2MetaButtonX.Enabled = true;
        }

        private void socket2YellowButtonX_Click(object sender, EventArgs e)
        {
            socket2YellowButtonX.Enabled = false;

            socket2NoneButtonX.Enabled = true;
            socket2BlueButtonX.Enabled = true;
            socket2RedButtonX.Enabled = true;
            socket2MetaButtonX.Enabled = true;
        }

        private void socket2MetaButtonX_Click(object sender, EventArgs e)
        {
            socket2MetaButtonX.Enabled = false;

            socket2NoneButtonX.Enabled = true;
            socket2BlueButtonX.Enabled = true;
            socket2RedButtonX.Enabled = true;
            socket2YellowButtonX.Enabled = true;
        }

        private void socket3NoneButtonX_Click(object sender, EventArgs e)
        {
            socket3NoneButtonX.Enabled = false;

            socket3BlueButtonX.Enabled = true;
            socket3RedButtonX.Enabled = true;
            socket3YellowButtonX.Enabled = true;
            socket3MetaButtonX.Enabled = true;
        }

        private void socket3BlueButtonX_Click(object sender, EventArgs e)
        {
            socket3BlueButtonX.Enabled = false;

            socket3NoneButtonX.Enabled = true;
            socket3RedButtonX.Enabled = true;
            socket3YellowButtonX.Enabled = true;
            socket3MetaButtonX.Enabled = true;
        }

        private void socket3RedButtonX_Click(object sender, EventArgs e)
        {
            socket3RedButtonX.Enabled = false;

            socket3NoneButtonX.Enabled = true;
            socket3BlueButtonX.Enabled = true;
            socket3YellowButtonX.Enabled = true;
            socket3MetaButtonX.Enabled = true;
        }

        private void socket3YellowButtonX_Click(object sender, EventArgs e)
        {
            socket3YellowButtonX.Enabled = false;

            socket3NoneButtonX.Enabled = true;
            socket3BlueButtonX.Enabled = true;
            socket3RedButtonX.Enabled = true;
            socket3MetaButtonX.Enabled = true;
        }

        private void socket3MetaButtonX_Click(object sender, EventArgs e)
        {
            socket3MetaButtonX.Enabled = false;

            socket3NoneButtonX.Enabled = true;
            socket3BlueButtonX.Enabled = true;
            socket3RedButtonX.Enabled = true;
            socket3YellowButtonX.Enabled = true;
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
                bloodElfCheckBoxX.Checked = true;
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
                bloodElfCheckBoxX.Checked = false;
                draeneiCheckBoxX.Checked = false;
            }
        }

        private void WeaponCreator_FormClosing(object sender, FormClosingEventArgs e)
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
