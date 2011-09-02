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
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using TrinityCore_Manager.Properties;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using SevenZip;

namespace TrinityCore_Manager
{
    public partial class UserSettings : DevComponents.DotNetBar.Office2007Form
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        private bool finished = false;

        private SQLMethods mysql = null;

        private string host = String.Empty;
        private int port = 0;
        private string username = String.Empty;
        private string password = String.Empty;

        private string aDB = String.Empty;
        private string cDB = String.Empty;
        private string wDB = String.Empty;

        private bool aRestart = false;
        private int restartAttempts = 0;

        private bool remoteAccess = false;

        private void wizard_WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
        {
            if (e.OldPage == TCMTypeWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                switch (localRemoteComboBoxEX.SelectedIndex)
                {
                    case 0:
                        e.NewPage = localWizardPage;
                        break;

                    case 1:
                        e.NewPage = raWizardPage;
                        break;
                }
            }
            else if (e.OldPage == raWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (hostTextBoxX.Text != String.Empty && portIntegerInput.Value != 0 && usernameTextBoxX.Text != String.Empty && passwordTextBoxX.Text != String.Empty)
                {
                    remoteAccess = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.OldPage == localWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (trinityFolderTextBoxX.Text == String.Empty)
                    e.Cancel = true;
                else
                    e.NewPage = MySQLWizardPage;
            }
            else if (e.OldPage == MySQLWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {

                if (MySQLHostTextBoxX.Text != String.Empty && MySQLIntegerInputX.Value != 0 && MySQLUsernameTextBoxX.Text != String.Empty && MySQLPasswordTextBoxX.Text != String.Empty)
                {

                    host = MySQLHostTextBoxX.Text;
                    port = MySQLIntegerInputX.Value;
                    username = MySQLUsernameTextBoxX.Text;
                    password = MySQLPasswordTextBoxX.Text;

                    MySQLTestConnectionProgressBarX.Visible = true;

                    mysql = new SQLMethods(host, port, username, password);

                    if (mysql != null)
                    {
                        bool MySQLTest = mysql.TestMySQLConnection();
                        Application.DoEvents();

                        MySQLTestConnectionProgressBarX.Visible = false;

                        if (MySQLTest)
                        {
                            switch (MySQLNewExistComboBoxEX.SelectedIndex)
                            {
                                case 0: //New Database

                                    e.NewPage = setupTCDBWizardPage;

                                    break;

                                case 1: //Existing Database

                                    e.NewPage = ExistingTCDBWizardPage;

                                    break;
                            }
                        }
                        else
                        {

                            e.Cancel = true;

                            TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "An error has occured!", "Could not connect to MySQL!", eTaskDialogButton.Ok));
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if ((e.OldPage == localWizardPage || e.OldPage == raWizardPage) && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (MySQLHostTextBoxX.Text == String.Empty || portIntegerInput.Value == 0 || MySQLUsernameTextBoxX.Text == String.Empty || MySQLPasswordTextBoxX.Text == String.Empty)
                {
                    e.Cancel = true;
                }
            }
            else if (e.OldPage == setupTCDBWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (remoteAccess)
                    e.NewPage = finishedWizardPage;
                else
                    e.NewPage = otherWizardPage;
            }
            else if (e.OldPage == ExistingTCDBWizardPage && e.PageChangeSource == eWizardPageChangeSource.NextButton)
            {
                if (MySQLAuthDBTextBoxX.Text == String.Empty || MySQLCharDBTextBoxX.Text == String.Empty || MySQLWorldDBTextBoxX.Text == String.Empty)
                {
                    e.Cancel = true;
                }
                else
                {
                    aDB = MySQLAuthDBTextBoxX.Text;
                    cDB = MySQLCharDBTextBoxX.Text;
                    wDB = MySQLWorldDBTextBoxX.Text;

                    if (remoteAccess)
                        e.NewPage = finishedWizardPage;
                    else
                        e.NewPage = otherWizardPage;

                }
            }
            
            
            if (e.NewPage == setupTCDBWizardPage)
            {
                setupTCDBWizardPage.NextButtonEnabled = eWizardButtonState.False;
            }
        }

        private void browseButtonX_Click(object sender, EventArgs e)
        {
            if (trinityFolderBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                trinityFolderTextBoxX.Text = trinityFolderBrowseDialog.SelectedPath;
            }
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            localRemoteComboBoxEX.SelectedIndex = 0;
            MySQLNewExistComboBoxEX.SelectedIndex = 0;

            trinityFolderTextBoxX.Text = Settings.Default.trinityFolder;


            MySQLAuthDBTextBoxX.Text = Settings.Default.AuthDB;
            MySQLCharDBTextBoxX.Text = Settings.Default.CharactersDB;
            MySQLWorldDBTextBoxX.Text = Settings.Default.WorldDB;

            hostTextBoxX.Text = Settings.Default.raHost;
            portIntegerInput.Value = Settings.Default.raPort;
            usernameTextBoxX.Text = Settings.Default.raUsername;
            passwordTextBoxX.Text = Settings.Default.raPassword;
            autoConnectCheckBox.Checked = Settings.Default.raAutoStart;

            MySQLHostTextBoxX.Text = Settings.Default.MySQLHost;
            MySQLIntegerInputX.Value = Settings.Default.MySQLPort;
            MySQLUsernameTextBoxX.Text = Settings.Default.MySQLUsername;
            MySQLPasswordTextBoxX.Text = Settings.Default.MySQLPassword;

            autoRestartCheckBoxX.Checked = Settings.Default.AutoRestart;
            restartAttemptsIntegerInput.Value = Settings.Default.RestartAttempts;
        }

        public delegate void TrinityWizardFinishedEventHandler(object sender, TrinityWizardFinishedEventArgs e);

        public event TrinityWizardFinishedEventHandler WizardFinished;

        private void wizard_FinishButtonClick(object sender, CancelEventArgs e)
        {

            finished = true;

            Settings.Default.MySQLHost = host;
            Settings.Default.MySQLPort = port;
            Settings.Default.MySQLUsername = username;
            Settings.Default.MySQLPassword = password;

            Settings.Default.AuthDB = aDB;
            Settings.Default.CharactersDB = cDB;
            Settings.Default.WorldDB = wDB;

            Settings.Default.AutoRestart = autoRestartCheckBoxX.Checked;
            Settings.Default.RestartAttempts = restartAttemptsIntegerInput.Value;

            if (!remoteAccess)
            {
                Settings.Default.trinityFolder = trinityFolderTextBoxX.Text;
                Settings.Default.raEnabled = false;
            }
            else
            {
                Settings.Default.raHost = hostTextBoxX.Text;
                Settings.Default.raPort = portIntegerInput.Value;
                Settings.Default.raUsername = usernameTextBoxX.Text;
                Settings.Default.raPassword = passwordTextBoxX.Text;
                Settings.Default.raAutoStart = autoConnectCheckBox.Checked;
                Settings.Default.raEnabled = true;
            }

            Settings.Default.Save();

            if (WizardFinished != null)
                WizardFinished(this, new TrinityWizardFinishedEventArgs(true));

            this.Close();

        }

        public class TrinityWizardFinishedEventArgs : EventArgs
        {
            public bool WizardSuccess { get; set; }

            public TrinityWizardFinishedEventArgs(bool success)
            {
                WizardSuccess = success;
            }
        }

        private void wizard_CancelButtonClick(object sender, CancelEventArgs e)
        {
        }

        private void wizard_BackButtonClick(object sender, CancelEventArgs e)
        {

        }

        private string location = String.Empty;

        private string authDB = "auth";
        private string charDB = "characters";
        private string worldDB = "world";

        private void downloadGitRepoButtonX_Click(object sender, EventArgs e)
        {

            eTaskDialogButton button = eTaskDialogButton.Yes;

            button |= eTaskDialogButton.No;

            eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Are you sure?", eTaskDialogIcon.Hand, "Are you sure?", String.Format("By doing this, you will replace the databases: \"{0}\", \"{1}\", and \"{2}\". Are you sure you want to continue?", authDB, charDB, worldDB), button));

            if (result == eTaskDialogResult.Yes)
            {
                if (DBFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {

                    aDB = authDB;
                    cDB = charDB;
                    wDB = worldDB;

                    location = DBFolderBrowserDialog.SelectedPath;

                    MySQLPercentLabelX.Visible = true;
                    DBProgressBarX.Visible = true;
                    DBProgressBarX.ProgressType = eProgressItemType.Standard;


                    downloadGitRepoButtonX.Enabled = false;

                    gitBackgroundWorker.RunWorkerAsync(location);
                }
            }
        }

        private void gitBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is string)
            {

                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo();

                    string loc = e.Argument as string;

                    psi.WorkingDirectory = loc;


                    string git = String.Empty;

                    if (Environment.Is64BitOperatingSystem == true)
                    {
                        git = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Git", "bin", "git.exe");
                    }
                    else
                    {
                        git = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Git", "bin", "git.exe");
                    }

                    psi.FileName = git;

                    if (!File.Exists(git))
                    {

                        this.Invoke((MethodInvoker)delegate
                        {
                            eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Git not found!", eTaskDialogIcon.Stop, "Git Not Found!", "You can download Git here: http://git-scm.com - You must install it in the Program Files Directory!", eTaskDialogButton.Ok));
                        });

                        e.Cancel = true;

                        return;
                    }

                    if (Directory.Exists(Path.Combine(loc, "TrinityCore", ".git")) || Directory.Exists(Path.Combine(loc, ".git")))
                    {
                        psi.Arguments = "pull -v --progress";

                        if (!Directory.Exists(Path.Combine(loc, ".git")))
                            psi.WorkingDirectory = Path.Combine(loc, "TrinityCore");
                    }
                    else
                    {
                        psi.Arguments = "clone -v --progress https://github.com/TrinityCore/TrinityCore.git";
                    }

                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;

                    Process gitProc = new Process();
                    gitProc.StartInfo = psi;

                    gitProc.Start();

                    gitProc.EnableRaisingEvents = true;

                    gitProc.BeginOutputReadLine();
                    gitProc.OutputDataReceived += new DataReceivedEventHandler(gitProc_OutputDataReceived);

                    gitProc.BeginErrorReadLine();
                    gitProc.ErrorDataReceived += new DataReceivedEventHandler(gitProc_ErrorDataReceived);

                    gitProc.Exited += new EventHandler(gitProc_Exited);

                    gitProc.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void gitProc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {

            string message = e.Data;

            if (message != null)
            {

                if (message.Contains("fatal"))
                {
                    this.Invoke((MethodInvoker)delegate
                    {

                        eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", e.Data, eTaskDialogButton.Ok));

                    });
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (message.Contains("%"))
                            {
                                string[] ex = message.Split(char.Parse("%"));

                                int index = ex[0].LastIndexOf(char.Parse(" "));

                                string percent = ex[0].Substring(index);

                                int GitPercent = 0;

                                int.TryParse(percent, out GitPercent);

                                DBProgressBarX.Value = GitPercent;

                                MySQLPercentLabelX.Text = percent + "%";
                                MySQLPercentLabelX.Left = (setupTCDBWizardPage.Width / 2) - (MySQLPercentLabelX.Width / 2);
                            }
                        });
                    }
                }
            }
        }

        private void gitProc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void gitProc_Exited(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                DBProgressBarX.Value = 0;

                MySQLPercentLabelX.Visible = false;
                DBProgressBarX.Visible = false;

                if (location != String.Empty)
                {
                    if (!dbBackgroundWorker.IsBusy)
                    {
                        if (mysql == null)
                            mysql = new SQLMethods(host, port, username, password);

                        Thread createMySQLDBs = new Thread(new ThreadStart(CreateMySQL));

                        DBProgressBarX.Visible = true;
                        createMySQLDBs.Start();

                    }
                    else
                    {
                    }
                }
                else
                {

                }

            });
        }

        private void CreateMySQL()
        {
            if (mysql != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    DBProgressBarX.ProgressType = eProgressItemType.Marquee;
                });

                string loc = String.Empty;

                if (Directory.Exists(Path.Combine(location, "sql")))
                    loc = Path.Combine(location, "sql");
                else if (Directory.Exists(Path.Combine(location, "TrinityCore", "sql")))
                    loc = Path.Combine(location, "TrinityCore", "sql");
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {

                            DBProgressBarX.Visible = false;

                            TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find sql folder!", eTaskDialogButton.Ok));
                        });
                    }

                    return;

                }

                mysql.MySQLStatementExecuted += new EventHandler(mysql_MySQLStatementExecuted);

                string mysqlDeleteLoc = Path.Combine(loc, "create", "drop_mysql.sql");


                if (!File.Exists(mysqlDeleteLoc))
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {

                            DBProgressBarX.Visible = false;

                            TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find drop_mysql.sql!", eTaskDialogButton.Ok));
                        });
                    }

                    return;

                }

                try
                {
                    mysql.ExecuteMySQLScript(mysqlDeleteLoc);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", ex.Message, eTaskDialogButton.Ok));
                }

                string mysqlCreateLoc = Path.Combine(loc, "create", "create_mysql.sql");

                if (!File.Exists(mysqlCreateLoc))
                {
                    if (this.InvokeRequired)
                    {

                        DBProgressBarX.Visible = false;

                        TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find create_mysql.sql!", eTaskDialogButton.Ok));

                        return;
                    }
                }

                mysql.ExecuteMySQLScript(mysqlCreateLoc);



                string mysqlAuthDB = Path.Combine(loc, "base", "auth_database.sql");


                if (!File.Exists(mysqlAuthDB))
                {
                    if (this.InvokeRequired)
                    {
                        DBProgressBarX.Visible = false;

                        TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find auth_database.sql!", eTaskDialogButton.Ok));

                        return;
                    }
                }

                mysql.ExecuteMySQLScript(mysqlAuthDB, authDB);


                string mysqlCharDB = Path.Combine(loc, "base", "characters_database.sql");

                if (!File.Exists(mysqlCharDB))
                {
                    if (this.InvokeRequired)
                    {
                        DBProgressBarX.Visible = false;

                        TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find characters_database.sql!", eTaskDialogButton.Ok));

                        return;
                    }
                }

                mysql.ExecuteMySQLScript(mysqlCharDB, charDB);

                this.Invoke((MethodInvoker)delegate
                {

                    MySQLPercentLabelX.Visible = true;

                    DBProgressBarX.ProgressType = eProgressItemType.Standard;
                });

                WebClient downloadWorldDB = new WebClient();
                downloadWorldDB.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadWorldDB_DownloadProgressChanged);
                downloadWorldDB.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadWorldDB_DownloadFileCompleted);

                downloadWorldDB.DownloadFileAsync(new Uri("https://github.com/downloads/TrinityCore/TrinityCore/TDB_335.11.40_2011_05_09.rar"), Path.Combine(location, "TDB_335.11.40_2011_05_09.rar"));

            }
        }

        private string TDBLoc = String.Empty;
        private string TDBFile = String.Empty;

        private void downloadWorldDB_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            TDBLoc = Path.Combine(location, "TDB_335.11.40_2011_05_09.rar");

            if (File.Exists(TDBLoc))
            {
                try
                {
                    FileStream fs = new FileStream(TDBLoc, FileMode.Open, FileAccess.Read, FileShare.None);

                    SevenZipExtractor.SetLibraryPath(Application.StartupPath + "\\7z.dll");
                    SevenZipExtractor extract = new SevenZipExtractor(fs);

                    if (!Directory.Exists(Path.Combine(location, "world")))
                        Directory.CreateDirectory(Path.Combine(location, "world"));


                    extract.ExtractionFinished += new EventHandler<EventArgs>(extract_ExtractionFinished);
                    extract.Extracting += new EventHandler<ProgressEventArgs>(extract_Extracting);
                    extract.BeginExtractArchive(Path.Combine(location, "world"));


                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, Logger.LogType.Error);
                }
            }
            else
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        DBProgressBarX.Visible = false;

                        TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find TDB_335.11.40_2011_05_09.rar!", eTaskDialogButton.Ok));
                    });
                }

                return;
            }
        }

        private void extract_Extracting(object sender, ProgressEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                DBProgressBarX.Value = e.PercentDone;

                MySQLPercentLabelX.Text = e.PercentDone + "%";
                MySQLPercentLabelX.Left = (MySQLWizardPage.Width / 2) - (MySQLPercentLabelX.Width / 2);
            });
        }

        private void extract_ExtractionFinished(object sender, EventArgs e)
        {

            this.Invoke((MethodInvoker)delegate
            {

                MySQLPercentLabelX.Visible = false;

                DBProgressBarX.ProgressType = eProgressItemType.Marquee;
            });

            if (!Directory.Exists(Path.Combine(location, "world")))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    DBProgressBarX.Visible = false;

                    TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", "Could not find TDB folder!", eTaskDialogButton.Ok));

                    return;
                });
            }

            TDBFile = "TDB_full_335.11.40_2011_05_09.sql";

            string TDBFileLoc = Path.Combine(location, "world", TDBFile);

            if (!File.Exists(Path.Combine(location, "world", TDBFileLoc)))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    DBProgressBarX.Visible = false;

                    TaskDialog.Show(new TaskDialogInfo("Error!", eTaskDialogIcon.Stop, "An error has occured!", String.Format("Could not find {0}!", TDBFile), eTaskDialogButton.Ok));

                    return;
                });
            }

            mysql.ExecuteMySQLScript(TDBFileLoc, worldDB);


            this.Invoke((MethodInvoker)delegate
            {

                downloadGitRepoButtonX.Enabled = true;

                DBProgressBarX.Visible = false;

                setupTCDBWizardPage.NextButtonEnabled = eWizardButtonState.True;

                TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.CheckMark, "Finished Operation", String.Format("The databases ({0}, {1}, {2}) have been created. Remember to update your server configuration file!", authDB, charDB, worldDB), eTaskDialogButton.Ok));
            });

        }

        private void downloadWorldDB_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    DBProgressBarX.Value = e.ProgressPercentage;

                    MySQLPercentLabelX.Text = e.ProgressPercentage + "%";
                    MySQLPercentLabelX.Left = (MySQLWizardPage.Width / 2) - (MySQLPercentLabelX.Width / 2);
                });
            }
        }

        private void mysql_MySQLStatementExecuted(object sender, EventArgs e)
        {

        }

        private void dbBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private bool exitFinished = false;

        private void UserSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finished)
            {

                eTaskDialogButton button = eTaskDialogButton.Yes;

                button |= eTaskDialogButton.No;

                eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Are you sure?", eTaskDialogIcon.Exclamation, "Confirm", "Are you sure you want to exit the setup wizard?", button));

                if (result == eTaskDialogResult.Yes)
                {
                    finished = true;

                    if (Settings.Default.raEnabled)
                    {
                        if (Settings.Default.raHost != String.Empty && Settings.Default.raPort != 0 && Settings.Default.raUsername != String.Empty && Settings.Default.raPassword != String.Empty)
                        {
                            e.Cancel = false;

                            return;
                        }
                    }
                    else
                    {
                        if (Settings.Default.trinityFolder != String.Empty)
                        {
                            e.Cancel = false;

                            return;
                        }
                    }


                    TaskDialog.Show(new TaskDialogInfo("Wizard Failed", eTaskDialogIcon.Stop, "You failed to complete the setup wizard...", "TrinityCore Manager is now closing", eTaskDialogButton.Ok));

                    Application.Exit();

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

    }
}