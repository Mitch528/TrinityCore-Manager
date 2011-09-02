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
using System.IO;
using TrinityCore_Manager.Properties;

namespace TrinityCore_Manager
{
    public partial class RestoreDB : DevComponents.DotNetBar.Office2007Form
    {
        public RestoreDB()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        private void RestoreDB_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(String.Format("{0}\\TrinityCore Manager\\backups", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))))
            {
                string[] files = Directory.GetFiles(String.Format("{0}\\TrinityCore Manager\\backups", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));

                if (files.Length == 0)
                    restoreDBdateTimeInput.Enabled = false;
                else
                {
                    Array.Sort(files);

                    foreach (string f in files)
                    {

                        string name = f;

                        //Min Date

                        DateTime dt = GetDate(name);

                        if (dt.Date == DateTime.MinValue)
                            continue;
                        else
                        {
                            restoreDBdateTimeInput.MinDate = dt.AddDays(-1);

                            break;
                        }


                    }

                    Array.Reverse(files);

                    foreach (string f in files)
                    {
                        //Max Date

                        DateTime dt = GetDate(f);

                        if (dt.Date == DateTime.MinValue)
                            continue;
                        else
                        {
                            restoreDBdateTimeInput.MaxDate = dt;

                            break;
                        }
                    }

                }
            }
            else
            {
                restoreDBdateTimeInput.Enabled = false;
            }
        }

        private DateTime GetDate(string input)
        {
            try
            {
                input = Path.GetFileNameWithoutExtension(input);

                if (input.StartsWith("Backup-"))
                {

                    string[] date = input.Split(char.Parse("-"));

                    int year = int.Parse(date[1]);
                    int month = int.Parse(date[2]);


                    int index = date[3].IndexOf(char.Parse("_"));

                    int day = int.Parse(date[3].Substring(0, index));

                    string[] ex = input.Split(char.Parse("_"));

                    string[] time = ex[1].Split(char.Parse("-"));

                    int hour = int.Parse(time[0]);
                    int minute = int.Parse(time[1]);
                    int second = int.Parse(time[2]);

                    string ampm = time[3];

                    DateTime dt = DateTime.Parse(String.Format("{0} {1}", hour, ampm));

                    hour = dt.Hour;

                    DateTime dTime = new DateTime(year, month, day, hour, minute, second);

                    return dTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Match Failed!", String.Format("File {0} does not match the correct format", input), eTaskDialogButton.Ok));
            }

            return DateTime.MinValue;
        }

        private void restoreButtonX_Click(object sender, EventArgs e)
        {
            if (backupListBox.SelectedIndex != -1)
            {

                if (restoreDBBackgroundWorker.IsBusy)
                    TaskDialog.Show(new TaskDialogInfo("Try again later", eTaskDialogIcon.Stop, "Restore Currently in Progress", "Try again after it has been completed", eTaskDialogButton.Ok));
                else
                {

                    eTaskDialogButton button = eTaskDialogButton.Yes;
                    button |= eTaskDialogButton.No;

                    eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Confirm", eTaskDialogIcon.Hand, "Are you sure?", String.Format("This will overwrite the databases: {0}, {1}, {2}. Once the restore has been started, it cannot be stopped.", Settings.Default.AuthDB, Settings.Default.CharactersDB, Settings.Default.WorldDB), button));

                    if (result == eTaskDialogResult.Yes)
                    {
                        restoreCircularProgress.IsRunning = true;

                        restoreDBBackgroundWorker.RunWorkerAsync(backupListBox.Items[backupListBox.SelectedIndex].ToString());
                    }
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "No Backup Selected", "Please select a backup, then try again.", eTaskDialogButton.Ok));
            }
        }

        private void restoreDBdateTimeInput_ValueChanged(object sender, EventArgs e)
        {
            backupListBox.Items.Clear();

            foreach (string f in Directory.GetFiles(String.Format("{0}\\TrinityCore Manager\\backups", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))))
            {
                if (GetDate(f).Date == restoreDBdateTimeInput.Value)
                {
                    backupListBox.Items.Add(String.Format("{0:T}", GetDate(f)));
                }
            }
        }

        private bool restoreSuccess = false;

        private void restoreDBBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string loc = String.Empty;

            foreach (string f in Directory.GetFiles(String.Format("{0}\\TrinityCore Manager\\backups", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))))
            {
                if (String.Format("{0:T}", GetDate(f)) == e.Argument.ToString())
                {
                    loc = f;
                }
            }

            if (loc != String.Empty && File.Exists(loc))
            {
                mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);

                if (mysql != null)
                {
                    try
                    {
                        mysql.ExecuteMySQLScript(loc);

                        restoreSuccess = true;

                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", ex.Message, eTaskDialogButton.Ok));
                    }
                }

            }

        }

        private void restoreDBBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            restoreCircularProgress.IsRunning = false;

            if (restoreSuccess)
            {
                TaskDialog.Show(new TaskDialogInfo("Restore Finished", eTaskDialogIcon.Information2, "Database successfully restored!", "Press ok to continue", eTaskDialogButton.Ok));
            }
        }

        private void RestoreDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (restoreDBBackgroundWorker.IsBusy)
            {
                TaskDialog.Show(new TaskDialogInfo("In Progress", eTaskDialogIcon.Stop, "Restore currently in progress", "Please wait until the restore has been completed", eTaskDialogButton.Ok));

                e.Cancel = true;
            }
        }
    }
}
