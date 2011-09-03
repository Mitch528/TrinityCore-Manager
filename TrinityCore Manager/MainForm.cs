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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Microsoft.Win32;
using TrinityCore_Manager.Properties;
using wyDay.Controls;

namespace TrinityCore_Manager
{
    public partial class MainForm : Office2007RibbonForm
    {
        private string CompilePath = String.Empty;
        private Process authProc;
        private bool compileFinishSuccess;
        private bool isDevenv;
        private SQLMethods mysql;
        private Client raClient;

        private int restartAttempts;
        private bool stopButtonPressed;
        private Process worldProc;

        public MainForm()
        {
            InitializeComponent();
        }

        private void checkForUpdatesButtonItem_Click(object sender, EventArgs e)
        {
            switch (automaticUpdater.UpdateStepOn)
            {
                case UpdateStepOn.Checking:
                case UpdateStepOn.DownloadingUpdate:
                case UpdateStepOn.ExtractingUpdate:

                    automaticUpdater.Cancel();

                    break;

                case UpdateStepOn.UpdateReadyToInstall:
                case UpdateStepOn.UpdateAvailable:
                case UpdateStepOn.UpdateDownloaded:

                    automaticUpdater.InstallNow();

                    break;

                default:

                    automaticUpdater.Visible = true;
                    automaticUpdater.ForceCheckForUpdate();

                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetProgramType();
            EnableDisableFeatures(false);
            SetOtherOptions();


            //Form Events

            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                Methods.KillProcByPID(authProc.Id);
                Methods.KillProcByPID(worldProc.Id);
            }
            catch
            {
            }
        }

        private void SetProgramType(bool start = true)
        {
            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort,
            Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);

            if (Settings.Default.raEnabled)
            {
                startServerButtonItem.Enabled = false;
                stopServerButtonItem.Enabled = false;

                backupDBButtonItem.Enabled = false;
                restoreDBButtonItem.Enabled = false;

                compileButtonItem.Enabled = false;

                if (Settings.Default.raHost == String.Empty || Settings.Default.raPort == 0 ||
                Settings.Default.raUsername == String.Empty || Settings.Default.raPassword == String.Empty ||
                Settings.Default.MySQLHost == String.Empty || Settings.Default.MySQLPort == 0 ||
                Settings.Default.MySQLUsername == String.Empty || Settings.Default.MySQLPassword == String.Empty)
                {
                    using (var uSettings = new UserSettings())
                    {
                        uSettings.WizardFinished += uSettings_WizardFinished;

                        uSettings.ShowDialog();
                    }
                }
                else
                {
                    if (start)
                        SetupRAConnection();

                    refreshValuesTimer.Enabled = true;
                }
            }
            else
            {
                backupDBButtonItem.Enabled = true;
                restoreDBButtonItem.Enabled = true;

                startServerButtonItem.Enabled = true;
                stopServerButtonItem.Enabled = false;

                compileButtonItem.Enabled = true;

                if (Settings.Default.trinityFolder == String.Empty || Settings.Default.MySQLHost == String.Empty ||
                Settings.Default.MySQLPort == 0 || Settings.Default.MySQLUsername == String.Empty ||
                Settings.Default.MySQLPassword == String.Empty)
                {
                    using (var uSettings = new UserSettings())
                    {
                        uSettings.WizardFinished += uSettings_WizardFinished;

                        uSettings.ShowDialog();
                    }
                }
                else
                {
                    refreshValuesTimer.Enabled = true;
                }
            }
        }

        private void SetupRAConnection()
        {
            if (raClient != null && raClient.isConnected())
                raClient.Disconnect();

            raClient = new Client(Settings.Default.raHost, Settings.Default.raPort, Settings.Default.raUsername,
            Settings.Default.raPassword);

            raClient.msgReceived += raClient_msgReceived;
            raClient.receiveFailed += raClient_receiveFailed;
            raClient.connFailed += raClient_connFailed;
            raClient.clientConnected += raClient_clientConnected;

            raClient.StartConnection();
        }

        private void raClient_clientConnected(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                SendToServer(Settings.Default.raUsername, false);
                SendToServer(Settings.Default.raPassword, false);


                EnableDisableFeatures(true);
            });
        }

        private void raClient_msgReceived(object sender, Client.MessageReceivedEventArgs e)
        {
            string msg = e.message;


            if (msg.ToLower().Contains("authentication failed"))
            {
                Invoke((MethodInvoker) delegate
                {
                    EnableDisableFeatures(false);

                    TaskDialog.Show(new TaskDialogInfo("Authentication Failed",
                    eTaskDialogIcon.Stop,
                    "Authentication Failed!",
                    "Disabling Features...",
                    eTaskDialogButton.Ok));
                });
            }

            Invoke((MethodInvoker) delegate
            {
                consoleTextBoxX.AppendText(msg + Environment.NewLine);
                consoleTextBoxX.SelectionStart = consoleTextBoxX.Text.Length;
                consoleTextBoxX.ScrollToCaret();
            });
        }

        private void raClient_receiveFailed(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                EnableDisableFeatures(false);

                raClient.Disconnect();

                TaskDialog.Show(new TaskDialogInfo("Connection Failed", eTaskDialogIcon.Stop,
                "Failed to receive data from server!",
                "Disabling Features...",
                eTaskDialogButton.Ok));
            });
        }

        private void raClient_connFailed(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                EnableDisableFeatures(false);

                raClient.Disconnect();

                TaskDialog.Show(new TaskDialogInfo("Connection Failed", eTaskDialogIcon.Stop,
                "Failed to connect to server!",
                "Disabling Features...",
                eTaskDialogButton.Ok));
            });
        }

        private void CheckMySQLConnection()
        {
            MySQLFillBackgroundWorker.DoWork += MySQLFillBackgroundWorker_DoWork;
            MySQLFillBackgroundWorker.RunWorkerCompleted += MySQLFillBackgroundWorker_RunWorkerCompleted;

            if (!MySQLFillBackgroundWorker.IsBusy)
                MySQLFillBackgroundWorker.RunWorkerAsync();
        }

        private void MySQLFillBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                if (mysql != null)
                {
                    bool testMYSQL = mysql.TestMySQLConnection();

                    if (!testMYSQL)
                    {
                        EnableDisableFeatures(false);
                        refreshValuesTimer.Stop();

                        TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop,
                        "Could not connect to MySQL!",
                        "Disabling Features...",
                        eTaskDialogButton.Ok));
                    }
                }
            });
        }

        private void MySQLFillBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Fill in MySQL values

            //Accounts

            accountsComboBoxItem.Items.Clear();
            charactersComboBoxItem.Items.Clear();

            //GetSetAccounts();
            //GetSetCharacters();
            GetSetOnline();
        }

        private void GetSetOnline()
        {
            Dictionary<string, List<string>> onlinePlayers = mysql.ReadAll(Settings.Default.CharactersDB, "characters",
            new [] { "online" }, new [] { "1" });

            if (onlinePlayers.ContainsKey("online") && onlinePlayers.ContainsKey("name"))
            {
                int plrAmnt = onlinePlayers["online"].Count;

                playersOnlineLabelItem.Text = "Players Online: " + plrAmnt;
            }
        }

        private void GetSetAccounts()
        {
            Dictionary<string, List<string>> accountDict = mysql.ReadAll(Settings.Default.AuthDB, "account");

            accountsComboBoxItem.Items.Clear();

            if (accountDict.ContainsKey("username"))
            {
                for (int i = 0; i < accountDict["username"].Count; i++)
                {
                    accountsComboBoxItem.Items.Add(accountDict["username"][i].ToLower());
                }
            }
        }

        private void GetSetCharacters()
        {
            Dictionary<string, List<string>> onlinePlayers = mysql.ReadAll(Settings.Default.CharactersDB, "characters",
            new [] { "online" }, new [] { "1" });

            charactersComboBoxItem.Items.Clear();
            kickPlayerComboBoxItem.Items.Clear();

            if (onlinePlayers.ContainsKey("online") && onlinePlayers.ContainsKey("name"))
            {
                for (int i = 0; i < onlinePlayers["online"].Count; i++)
                {
                    charactersComboBoxItem.Items.Add(onlinePlayers["name"][i]);
                    kickPlayerComboBoxItem.Items.Add(onlinePlayers["name"][i]);
                }
            }
        }

        private void SetOtherOptions()
        {
            platformComboBoxItem.SelectedIndex = 0;
        }

        private void EnableDisableFeatures(bool enabled)
        {
            if (enabled)
            {
                //Server

                executeCommandButtonItem.Enabled = true;

                //Accounts

                addAcctButtonItem.Enabled = true;
                editAcctButtonItem.Enabled = true;
                deleteAcctButtonItem.Enabled = true;
                refreshAccountsButtonItem.Enabled = true;


                //Commands

                announceCheckBoxItem.Enabled = true;
                notifyCheckBoxItem.Enabled = true;
                sendMessageCheckBoxItem.Enabled = true;
                sendMessageButtonItem.Enabled = true;
                kickPlayerButtonItem.Enabled = true;
                kickRefreshPlayersButtonItem.Enabled = true;
                messageTextBoxItem.Enabled = true;


                //Characters

                refreshCharactersButtonItem.Enabled = true;
                charReviveButtonItem.Enabled = true;
                charBanButtonItem.Enabled = true;
                charForceRenameButton.Enabled = true;
                charRequestCustomizeButtonItem.Enabled = true;
                charRequestChgFactionButtonItem.Enabled = true;
                charRequestChgRaceButtonItem.Enabled = true;
                charDeleteButtonItem.Enabled = true;
                chgCharLevelButtonItem.Enabled = true;


                //Bans

                addIPBanButtonItem.Enabled = true;
                deleteIPBanButtonItem.Enabled = true;
                addAcctBanButtonItem.Enabled = true;
                delAcctBanButtonItem.Enabled = true;

                /*
                                                //Create/Editor

                                                createItemButtonItem.Enabled = true;
                                                editItemButtonItem.Enabled = true;
                                                createWeaponButtonItem.Enabled = true;
                                                editWeaponButtonItem.Enabled = true;
                                                createArmorButtonItem.Enabled = true;
                                                editArmorButtonItem.Enabled = true;
                                                createNPCButtonItem.Enabled = true;
                                                editNPCButtonItem.Enabled = true;
                                                createVendorButtonItem.Enabled = true;
                                                editVendorButtonItem.Enabled = true;
                                                createLootButtonItem.Enabled = true;
                                                editLootButtonItem.Enabled = true;
                                                */

                //Other

                sendMailButtonItem.Enabled = true;
            }
            else
            {
                //Server

                executeCommandButtonItem.Enabled = false;

                //Accounts

                addAcctButtonItem.Enabled = false;
                editAcctButtonItem.Enabled = false;
                deleteAcctButtonItem.Enabled = false;
                refreshAccountsButtonItem.Enabled = false;


                //Commands

                kickPlayerButtonItem.Enabled = false;
                announceCheckBoxItem.Enabled = false;
                notifyCheckBoxItem.Enabled = false;
                sendMessageCheckBoxItem.Enabled = false;
                sendMessageButtonItem.Enabled = false;
                kickRefreshPlayersButtonItem.Enabled = false;
                messageTextBoxItem.Enabled = false;


                //Characters

                refreshCharactersButtonItem.Enabled = false;
                charReviveButtonItem.Enabled = false;
                charBanButtonItem.Enabled = false;
                charForceRenameButton.Enabled = false;
                charRequestCustomizeButtonItem.Enabled = false;
                charRequestChgFactionButtonItem.Enabled = false;
                charRequestChgRaceButtonItem.Enabled = false;
                charDeleteButtonItem.Enabled = false;
                chgCharLevelButtonItem.Enabled = false;


                //Bans

                addIPBanButtonItem.Enabled = false;
                deleteIPBanButtonItem.Enabled = false;
                addAcctBanButtonItem.Enabled = false;
                delAcctBanButtonItem.Enabled = false;

                /*
                                                //Create/Editor

                                                createItemButtonItem.Enabled = false;
                                                editItemButtonItem.Enabled = false;
                                                createWeaponButtonItem.Enabled = false;
                                                editWeaponButtonItem.Enabled = false;
                                                createArmorButtonItem.Enabled = false;
                                                editArmorButtonItem.Enabled = false;
                                                createNPCButtonItem.Enabled = false;
                                                editNPCButtonItem.Enabled = false;
                                                createVendorButtonItem.Enabled = false;
                                                editVendorButtonItem.Enabled = false;
                                                createLootButtonItem.Enabled = false;
                                                editLootButtonItem.Enabled = false;
                                                */

                //Other

                sendMailButtonItem.Enabled = false;
            }
        }

        private void uSettings_WizardFinished(object sender, UserSettings.TrinityWizardFinishedEventArgs e)
        {
            SetProgramType();

            if (Settings.Default.raEnabled)
            {
                if (Settings.Default.raHost == String.Empty || Settings.Default.raPort == 0 ||
                Settings.Default.raUsername == String.Empty || Settings.Default.raPassword == String.Empty)
                {
                    TaskDialog.Show(new TaskDialogInfo("Wizard Failed", eTaskDialogIcon.Stop,
                    "You failed to complete the setup wizard...",
                    "TrinityCore Manager is now closing", eTaskDialogButton.Ok));

                    Application.Exit();
                }
            }
            else
            {
                if (Settings.Default.trinityFolder == String.Empty)
                {
                    TaskDialog.Show(new TaskDialogInfo("Wizard Failed", eTaskDialogIcon.Stop,
                    "You failed to complete the setup wizard...",
                    "TrinityCore Manager is now closing", eTaskDialogButton.Ok));

                    Application.Exit();
                }
            }
        }

        private void setupButtonItem_Click(object sender, EventArgs e)
        {
            using (var uSettings = new UserSettings())
            {
                uSettings.WizardFinished += uSettings_WizardFinished;
                uSettings.ShowDialog();
            }
        }

        private void startServerButtonItem_Click(object sender, EventArgs e)
        {
            stopButtonPressed = false;
            restartAttempts = 0;

            BeginStartServer();
        }

        private void BeginStartServer()
        {
            bool authExists = false;
            bool worldExists = false;
            int authPid = 0;
            int worldPid = 0;

            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "authserver")
                {
                    authExists = true;
                    authPid = p.Id;
                }

                if (p.ProcessName == "worldserver")
                {
                    worldExists = true;
                    worldPid = p.Id;
                }
            }

            if (!authExists && !worldExists)
            {
                if (!File.Exists(Path.Combine(Settings.Default.trinityFolder, "authserver.exe")))
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Not Found",
                    "Could not find authserver.exe.", eTaskDialogButton.Ok));

                    return;
                }

                if (!File.Exists(Path.Combine(Settings.Default.trinityFolder, "worldserver.exe")))
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Not Found",
                    "Could not find worldserver.exe.", eTaskDialogButton.Ok));

                    return;
                }

                StartServerThread();
            }
            else
                if (authExists && worldExists)
                {
                    eTaskDialogButton button = eTaskDialogButton.Yes;

                    button |= eTaskDialogButton.No;

                    eTaskDialogResult result =
                    TaskDialog.Show(new TaskDialogInfo("Already Running", eTaskDialogIcon.Information,
                    "Authserver and Worldserver are currently running", "Stop it?",
                    button));

                    if (result == eTaskDialogResult.Yes)
                    {
                        Process authServerProc = Process.GetProcessById(authPid);
                        Process worldServerProc = Process.GetProcessById(worldPid);

                        authServerProc.Kill();
                        worldServerProc.Kill();

                        authServerProc.Dispose();
                        worldServerProc.Dispose();
                    }
                }
                else
                    if (authExists)
                    {
                        eTaskDialogButton button = eTaskDialogButton.Yes;

                        button |= eTaskDialogButton.No;

                        eTaskDialogResult result =
                        TaskDialog.Show(new TaskDialogInfo("Already Running", eTaskDialogIcon.Information,
                        "Authserver is currently running", "Stop it?", button));

                        if (result == eTaskDialogResult.Yes)
                        {
                            using (Process authServerProc = Process.GetProcessById(authPid))
                            {
                                authServerProc.Kill();
                            }
                        }
                    }
                    else
                    {
                        eTaskDialogButton button = eTaskDialogButton.Yes;

                        button |= eTaskDialogButton.No;

                        eTaskDialogResult result = TaskDialog.Show(new TaskDialogInfo("Already Running", eTaskDialogIcon.Information, "Worldserver is currently running", "Stop it?", button));

                        if (result == eTaskDialogResult.Yes)
                        {
                            using (Process worldServerProc = Process.GetProcessById(worldPid))
                            {
                                worldServerProc.Kill();
                            }
                        }
                    }
        }

        private void StartServerThread()
        {
            var serverThread = new Thread(StartServer) { IsBackground = true };

            serverThread.Start();
        }

        private void StartServer()
        {
            string authServerFName = Path.Combine(Settings.Default.trinityFolder, "authserver.exe");

            Invoke((MethodInvoker) delegate
            {
                startServerButtonItem.Enabled = false;
                stopServerButtonItem.Enabled = true;
            });

            if (File.Exists(authServerFName))
            {
                ////////////////////////////////////////////Auth Server///////////////////////////////////////////


                try
                {
                    var psi = new ProcessStartInfo(authServerFName);

                    psi.WorkingDirectory = Settings.Default.trinityFolder;

                    psi.UseShellExecute = false;

                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;

                    authProc = new Process();
                    authProc.StartInfo = psi;

                    authProc.Start();

                    Invoke((MethodInvoker) delegate
                    {
                        authServerLabelItem.Image = Resources.checkmark_16;
                    });

                    authProc.EnableRaisingEvents = true;

                    authProc.Exited += authServerProc_Exited;
                }
                catch (Exception ex)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "An error has occured", ex.Message,
                    eTaskDialogButton.Ok));
                }
            }
            else
            {
                return;
            }


            ////////////////////////////////////////////World Server///////////////////////////////////////////


            string worldServerFName = Path.Combine(Settings.Default.trinityFolder, "worldserver.exe");


            if (File.Exists(worldServerFName))
            {
                try
                {
                    var psi = new ProcessStartInfo(worldServerFName);

                    psi.WorkingDirectory = Settings.Default.trinityFolder;

                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardInput = true;

                    psi.UseShellExecute = false;

                    psi.CreateNoWindow = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;

                    worldProc = new Process();
                    worldProc.StartInfo = psi;

                    worldProc.Start();

                    Invoke((MethodInvoker) delegate
                    {
                        worldServerLabelItem.Image = Resources.checkmark_16;

                        EnableDisableFeatures(true);

                        if (restartAttempts != 0)
                            WriteConsoleOutput(String.Format("Restart Attempt: {0}",
                            restartAttempts));
                    });

                    worldProc.EnableRaisingEvents = true;

                    worldProc.Exited += worldServerProc_Exited;


                    worldProc.BeginOutputReadLine();
                    worldProc.OutputDataReceived += worldProc_OutputDataReceived;

                    worldProc.WaitForExit();
                    worldProc.Close();
                    worldProc.Dispose();
                }
                catch (Exception ex)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "An error has occured", ex.Message,
                    eTaskDialogButton.Ok));
                }
            }
            else
            {
                return;
            }
        }

        private void worldProc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                WriteConsoleOutput(e.Data);
            });
        }

        private void WriteConsoleOutput(string output)
        {
            consoleTextBoxX.AppendText(output + Environment.NewLine);
            consoleTextBoxX.SelectionStart = consoleTextBoxX.Text.Length;
            consoleTextBoxX.ScrollToCaret();
        }

        private void worldServerProc_Exited(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                worldServerLabelItem.Image = Resources.error_16;

                KillServer(Enums.TrinityServer.Auth);

                EnableDisableFeatures(false);

                if (!stopButtonPressed && Settings.Default.AutoRestart)
                {
                    if (restartAttempts < Settings.Default.RestartAttempts ||
                    Settings.Default.RestartAttempts == 0)
                    {
                        restartAttempts++;

                        BeginStartServer();
                    }
                    else
                    {
                        TaskDialog.Show(new TaskDialogInfo("Aborted", eTaskDialogIcon.Stop,
                        "Auto Restart Aborted",
                        String.Format(
                        "{0} attempts made to restart",
                        restartAttempts),
                        eTaskDialogButton.Ok));
                    }
                }
                else
                {
                    restartAttempts = 0;

                    startServerButtonItem.Enabled = true;
                    stopServerButtonItem.Enabled = false;
                }
            });
        }

        private void authServerProc_Exited(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                startServerButtonItem.Enabled = true;
                stopServerButtonItem.Enabled = false;

                authServerLabelItem.Image = Resources.error_16;

                KillServer(Enums.TrinityServer.World);

                EnableDisableFeatures(false);
            });
        }

        private void KillServer(Enums.TrinityServer server)
        {
            switch (server)
            {
                case Enums.TrinityServer.Auth:

                    Methods.KillProcByName("authserver");

                    break;

                case Enums.TrinityServer.World:

                    Methods.KillProcByName("worldserver");

                    break;
            }
        }

        private void stopServerButtonItem_Click(object sender, EventArgs e)
        {
            try
            {
                Methods.KillProcsByNameArray(new [] { "authserver", "worldserver" });

                EnableDisableFeatures(false);

                restartAttempts = 0;
                stopButtonPressed = true;
            }
            catch (Exception ex)
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "An error has occured", ex.Message,
                eTaskDialogButton.Ok));
            }
        }

        private void addAcctButtonItem_Click(object sender, EventArgs e)
        {
            using (var addAcct = new AddAccount())
            {
                addAcct.AccountAdded += addAcct_AccountAdded;

                addAcct.ShowDialog();
            }
        }

        private void addAcct_AccountAdded(object sender, AddAccount.AccountAddedEventArgs e)
        {
            SendToServer(String.Format("{0} {1} {2}", Constants.AccountAdd, e.account, e.password));
            SendToServer(String.Format("{0} {1} {2} -1", Constants.SetGMLevel, e.account, e.gmlevel));

            GetSetAccounts();
        }

        private void SendToServer(string message, bool showMessage = false)
        {
            if (showMessage)
            {
                consoleTextBoxX.AppendText(message + Environment.NewLine);
                consoleTextBoxX.SelectionStart = consoleTextBoxX.Text.Length;
                consoleTextBoxX.ScrollToCaret();
            }

            if (Settings.Default.raEnabled)
            {
                if (raClient != null && raClient.isConnected())
                {
                    raClient.SendMessage(message);
                }
            }
            else
            {
                if (worldProc != null && Methods.CheckIfProcExistsByPID(worldProc.Id) && !worldProc.HasExited)
                {
                    worldProc.StandardInput.WriteLine(message);
                    worldProc.StandardInput.Flush();
                }
            }
        }

        private void deleteAcctButtonItem_Click(object sender, EventArgs e)
        {
            if (accountsComboBoxItem.SelectedIndex != -1)
            {
                SendToServer(String.Format("{0} {1}", Constants.AccountDelete, accountsComboBoxItem.SelectedItem));

                GetSetAccounts();
            }
        }

        private void refreshAccountsButtonItem_Click(object sender, EventArgs e)
        {
            GetSetAccounts();
        }

        private void executeCommandButtonItem_Click(object sender, EventArgs e)
        {
            if (commandTextBoxItem.Text != String.Empty)
                SendToServer(commandTextBoxItem.Text, true);
        }

        private void kickPlayerButtonItem_Click(object sender, EventArgs e)
        {
            if (kickPlayerComboBoxItem.SelectedIndex != -1)
            {
                SendToServer(String.Format("{0} {1}", Constants.KickPlayer, kickPlayerComboBoxItem.SelectedItem));
            }
        }

        private void sendMessageButtonItem_Click(object sender, EventArgs e)
        {
            if (announceCheckBoxItem.Checked && messageTextBoxItem.Text != String.Empty)
                SendToServer(String.Format("{0} {1}", Constants.Announce, messageTextBoxItem.Text));
            else
                if (notifyCheckBoxItem.Checked && messageTextBoxItem.Text != String.Empty)
                    SendToServer(String.Format("{0} {1}", Constants.Notify, messageTextBoxItem.Text));
                else
                    if (sendMessageCheckBoxItem.Checked && messageTextBoxItem.Text != String.Empty)
                    {
                        string player = String.Empty;

                        if (Methods.InputBox("Player", "Who do you want to send this message to?", ref player) ==
                        DialogResult.OK)
                        {
                            if (player != String.Empty)
                            {
                                SendToServer(String.Format("{0} {1} {2}", Constants.SendMessage, player, messageTextBoxItem.Text));
                            }
                        }
                    }
        }

        private void editAcctButtonItem_Click(object sender, EventArgs e)
        {
            if (mysql != null && accountsComboBoxItem.SelectedIndex != -1)
            {
                using (var editAcct = new EditAccount())
                {
                    editAcct.AccountModified += editAcct_AccountModified;

                    Dictionary<string, List<string>> acctDict = mysql.ReadAll(Settings.Default.AuthDB, "account",
                    new[] { "username" },
                    new[] {
                accountsComboBoxItem.SelectedItem.
                ToString() });

                    if (acctDict.ContainsKey("username"))
                    {
                        if (acctDict["username"].Count == 1)
                        {
                            editAcct.accountName = acctDict["username"][0];

                            int id = 0;

                            if (acctDict.ContainsKey("id"))
                                int.TryParse(acctDict["id"][0], out id);

                            int expansion = 0;

                            if (acctDict.ContainsKey("expansion"))
                                int.TryParse(acctDict["expansion"][0], out expansion);

                            editAcct.expansion = expansion;


                            Dictionary<string, List<string>> accessDict = mysql.ReadAll(Settings.Default.AuthDB,
                            "account_access", new[] { "id" },
                            new[] { id.ToString() });

                            if (accessDict.ContainsKey("gmlevel"))
                            {
                                if (accessDict["gmlevel"].Count == 1)
                                {
                                    int gmLevel = 0;

                                    int.TryParse(accessDict["gmlevel"][0], out gmLevel);

                                    editAcct.gmLevel = gmLevel;
                                }
                            }

                            editAcct.ShowDialog();
                        }
                    }
                }
            }
        }

        private void editAcct_AccountModified(object sender, EditAccount.AccountModifiedEventArgs e)
        {
            SendToServer(String.Format("{0} {1} {2} -1", Constants.SetGMLevel, e.accountName, e.gmLevel));

            if (e.password != String.Empty)
                SendToServer(String.Format("{0} {1} {2} {3}", Constants.SetAcctPassword, e.accountName, e.password,
                e.password));

            SendToServer(String.Format("{0} {1} {2}", Constants.SetExpansion, e.accountName, e.expansion));
        }

        private void refreshValuesTimer_Tick(object sender, EventArgs e)
        {
            //CheckMySQLConnection();
        }

        private void kickRefreshPlayersButtonItem_Click(object sender, EventArgs e)
        {
            GetSetOnline();
            GetSetCharacters();
        }

        private void reconnectToRAButtonItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.raEnabled)
            {
                SetupRAConnection();

                if (!refreshValuesTimer.Enabled)
                    refreshValuesTimer.Enabled = true;
            }
        }

        private void refreshCharactersButtonItem_Click(object sender, EventArgs e)
        {
            GetSetCharacters();
        }

        private void charReviveButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1}", Constants.ReviveCharacter, charactersComboBoxItem.SelectedItem));
        }

        private void charBanButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1} -1 {2}", Constants.BanCharacter, charactersComboBoxItem.SelectedItem,
                Constants.BanReason));
        }

        private void commandTextBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                executeCommandButtonItem.RaiseClick();
        }

        private void charForceRenameButton_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1}", Constants.ForceRename, charactersComboBoxItem.SelectedItem));
        }

        private void charRequestCustomizeButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1}", Constants.RequestCustomize, charactersComboBoxItem.SelectedItem));
        }

        private void charRequestChgFactionButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1}", Constants.RequestChangeFaction,
                charactersComboBoxItem.SelectedItem));
        }

        private void charRequestChgRaceButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1}", Constants.RequestChangeRace, charactersComboBoxItem.SelectedItem));
        }

        private void charDeleteButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
                SendToServer(String.Format("{0} {1}", Constants.DeleteCharacter, charactersComboBoxItem.SelectedItem));
        }

        private void addIPBanButtonItem_Click(object sender, EventArgs e)
        {
            using (var addIPBan = new AddIPBan())
            {
                addIPBan.IPBanSubmitted += addIPBan_IPBanSubmitted;

                addIPBan.ShowDialog();
            }
        }

        private void addIPBan_IPBanSubmitted(object sender, AddIPBan.IPBanSubmittedEventArgs e)
        {
            SendToServer(String.Format("{0} {1} -1 {2}", Constants.AddIPBan, e.ipBan, Constants.BanReason));
        }

        private void deleteIPBanButtonItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> ipBanList = mysql.ReadAll(Settings.Default.AuthDB, "ip_banned");

            var IPs = new List<string>();

            if (ipBanList.ContainsKey("ip"))
            {
                for (int i = 0; i < ipBanList["ip"].Count; i++)
                {
                    IPs.Add(ipBanList["ip"][i]);
                }
            }

            using (var delIP = new DelIPBan())
            {
                delIP.DelIPBanSubmitted += delIP_DelIPBanSubmitted
                    ;
                delIP.ipBanList = IPs;


                delIP.ShowDialog();
            }
        }

        private void delIP_DelIPBanSubmitted(object sender, DelIPBan.DelIPBanSubmittedEventArgs e)
        {
            SendToServer(String.Format("{0} {1}", Constants.DelIPBan, e.ipAddress));
        }

        private void addAcctBanButtonItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> acctDict = mysql.ReadAll(Settings.Default.AuthDB, "account");

            var acctList = new List<string>();

            if (acctDict.ContainsKey("username"))
            {
                for (int i = 0; i < acctDict["username"].Count; i++)
                {
                    acctList.Add(acctDict["username"][i]);
                }
            }

            using (var addAcctBan = new AddAcctBan())
            {
                addAcctBan.AcctBanSubmitted += addAcctBan_AcctBanSubmitted;

                addAcctBan.acctList = acctList;

                addAcctBan.ShowDialog();
            }
        }

        private void addAcctBan_AcctBanSubmitted(object sender, AddAcctBan.AcctBanSubmittedEventArgs e)
        {
            SendToServer(String.Format("{0} {1} -1 {2}", Constants.BanAccount, e.account, Constants.BanReason));
        }

        private void delAcctBanButtonItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> acctDict = mysql.ReadAll(Settings.Default.AuthDB, "account");

            var acctDictionary = new Dictionary<string, string>();

            if (acctDict.ContainsKey("id") && acctDict.ContainsKey("username"))
            {
                for (int i = 0; i < acctDict["id"].Count; i++)
                {
                    acctDictionary.Add(acctDict["id"][i], acctDict["username"][i]);
                }
            }

            Dictionary<string, List<string>> banDict = mysql.ReadAll(Settings.Default.AuthDB, "account_banned",
            new [] { "active" }, new [] { "1" });

            var banList = new List<string>();

            if (banDict.ContainsKey("id"))
            {
                for (int i = 0; i < banDict["id"].Count; i++)
                {
                    banList.Add(banDict["id"][i]);
                }
            }

            var bannedPlayers = new List<string>();

            for (int i = 0; i < banList.Count; i++)
            {
                if (acctDictionary.ContainsKey(banList[i]))
                    bannedPlayers.Add(acctDictionary[banList[i]]);
            }


            using (var delAcctBan = new DelAcctBan())
            {
                delAcctBan.AcctDelBanSubmitted += delAcctBan_AcctDelBanSubmitted;

                delAcctBan.acctList = bannedPlayers;


                delAcctBan.ShowDialog();
            }
        }

        private void delAcctBan_AcctDelBanSubmitted(object sender, DelAcctBan.DelBanSubmittedEventArgs e)
        {
            SendToServer(String.Format("{0} {1}", Constants.UnBanAccount, e.account));
        }

        private void setTrunkLocationButtonItem_Click(object sender, EventArgs e)
        {
            if (setTrunkLocationFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.trunkLocation = setTrunkLocationFolderBrowserDialog.SelectedPath;
                Settings.Default.Save();

                TaskDialog.Show(new TaskDialogInfo("Success", eTaskDialogIcon.Information2, "Trunk Location Set!",
                "Press Ok to continue", eTaskDialogButton.Ok));
            }
        }

        private void updateTCButtonItem_Click(object sender, EventArgs e)
        {
            if (Settings.Default.trunkLocation != String.Empty && !updateTCTrunkBackgroundWorker.IsBusy)
                updateTCTrunkBackgroundWorker.RunWorkerAsync();
            else
                if (updateTCTrunkBackgroundWorker.IsBusy)
                {
                    TaskDialog.Show(new TaskDialogInfo("Busy", eTaskDialogIcon.Stop, "Update is already running!",
                    "Please try again later", eTaskDialogButton.Ok));
                }
                else
                    if (Settings.Default.trunkLocation == String.Empty)
                    {
                        TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop,
                        "You must set the trunk location first!", String.Empty,
                        eTaskDialogButton.Ok));
                    }
        }

        private void gitProc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string message = e.Data;

            if (message != null)
            {
                if (message.Contains("fatal"))
                {
                    Invoke(
                    (MethodInvoker)
                    delegate
                    {
                        eTaskDialogResult result =
                        TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", e.Data,
                        eTaskDialogButton.Ok));
                    });
                }
                else
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker) delegate
                        {
                            if (message.Contains("%"))
                            {
                                string[] ex = message.Split(char.Parse("%"));

                                int index = ex[0].LastIndexOf(char.Parse(" "));

                                string percent = ex[0].Substring(index);

                                int gitPercent = 0;

                                int.TryParse(percent, out gitPercent);

                                compileCircularProgressItem.Value = gitPercent;
                            }
                        });
                    }
                }
            }
        }

        private void gitProc_Exited(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                compileCircularProgressItem.Value = 0;

                TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2,
                "Finished Updating",
                "TrinityCore Trunk Update Completed.",
                eTaskDialogButton.Ok));
            });
        }

        private void updateTCTrunkBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo();

                string loc = Settings.Default.trunkLocation;

                psi.WorkingDirectory = loc;


                string git = String.Empty;

                if (Environment.Is64BitOperatingSystem)
                {
                    git = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Git",
                    "bin", "git.exe");
                }
                else
                {
                    git = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Git", "bin",
                    "git.exe");
                }

                psi.FileName = git;

                if (!File.Exists(git))
                {
                    Invoke(
                    (MethodInvoker)
                    delegate
                    {
                        eTaskDialogResult result =
                        TaskDialog.Show(new TaskDialogInfo("Git not found!", eTaskDialogIcon.Stop,
                        "Git Not Found!",
                        "You can download Git here: http://git-scm.com - You must install it in the Program Files Directory!",
                        eTaskDialogButton.Ok));
                    });

                    return;
                }

                if (Directory.Exists(Path.Combine(loc, "TrinityCore", ".git")) ||
                Directory.Exists(Path.Combine(loc, ".git")))
                {
                    psi.Arguments = "pull -v --progress";

                    if (!Directory.Exists(Path.Combine(loc, ".git")))
                        psi.WorkingDirectory = Path.Combine(loc, "TrinityCore");
                }
                else
                {
                    psi.Arguments = "clone -v --progress https://github.com/TrinityCore/TrinityCore.git";
                }

                //psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;

                using (var gitProc = new Process() { StartInfo = psi })
                {
                    gitProc.Start();

                    gitProc.EnableRaisingEvents = true;

                    gitProc.BeginErrorReadLine();

                    gitProc.ErrorDataReceived += gitProc_ErrorDataReceived;
                    gitProc.Exited += gitProc_Exited;


                    gitProc.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void compileButtonItem_Click(object sender, EventArgs e)
        {
            if (Methods.CheckIfProcExistsByName("authserver") || Methods.CheckIfProcExistsByName("worldserver"))
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Server is still running!",
                "You need to stop the server before you can compile!",
                eTaskDialogButton.Ok));

                return;
            }

            if (Settings.Default.trunkLocation == String.Empty || Settings.Default.trinityFolder == String.Empty)
                return;

            RegistryKey dev =
            Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\devenv.exe");
            RegistryKey vc =
            Registry.LocalMachine.OpenSubKey(
            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\VCExpress.exe");

            bool found = false;

            if (dev != null)
            {
                isDevenv = true;
                found = true;
                CompilePath = dev.GetValue(null).ToString().Replace("exe", "com");
            }
            else
                if (vc != null)
                {
                    CompilePath = vc.GetValue(null).ToString();
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "Could not find devenv.exe/VCExpress.exe, which is required to compile TrinityCore. Try installing/reinstalling Microsoft Visual Studio/C++ Express (2010), then try again.",
                    eTaskDialogButton.Ok));
                }

            dev.Dispose();
            vc.Dispose();


            RegistryKey KitWare;

            if (Environment.Is64BitOperatingSystem)
                KitWare = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Kitware");
            else
                KitWare = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Kitware");

            if (KitWare == null)
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                "Could not find CMake! You can find it here: http://www.cmake.org/",
                eTaskDialogButton.Ok));

                return;
            }

            string cMakeFolder = String.Empty;

            foreach (string Subkey in KitWare.GetSubKeyNames())
            {
                cMakeFolder = Subkey;
            }

            using (RegistryKey cMake = KitWare.OpenSubKey(cMakeFolder))
            {
                string path = cMake.GetValue(null).ToString();
                if (path == String.Empty)
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!", "Could not find CMake! You can find it here: http://www.cmake.org/", eTaskDialogButton.Ok));
                    return;
                }
            }

            if (Directory.Exists(String.Format("{0}\\OpenSSL-Win32", Environment.GetEnvironmentVariable("SystemDrive")))
            ||
            Directory.Exists(String.Format("{0}\\OpenSSL-Win64", Environment.GetEnvironmentVariable("SystemDrive"))))
            {
                Settings.Default.trinityCoreCompilePlatform = platformComboBoxItem.SelectedIndex;
                Settings.Default.Save();

                compileCircularProgressItem.Start();

                compileButtonItem.Enabled = false;

                TrinityCoreCompileBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                "Could not find OpenSSL! You can find it here: http://www.slproweb.com/products/Win32OpenSSL.html - It must be installed in the system drive directory.",
                eTaskDialogButton.Ok));

                return;
            }
        }

        private void TrinityCoreCompileBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ////////////////////////////////////////////CMake////////////////////////////////////////////

            string dirname =
            Path.GetFullPath(String.Format("{0}\\TrinityBuild", Environment.GetEnvironmentVariable("SystemDrive")));

            if (!Directory.Exists(dirname))
                Directory.CreateDirectory(dirname);
            else
            {
                string[] files = Directory.GetFiles(dirname);

                foreach (string f in files)
                    File.Delete(f);
            }

            RegistryKey KitWare;

            if (Environment.Is64BitOperatingSystem)
                KitWare = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Kitware");
            else
                KitWare = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Kitware");

            string cMakeFolder = String.Empty;

            foreach (string Subkey in KitWare.GetSubKeyNames())
            {
                cMakeFolder = Subkey;
            }

            RegistryKey cMake = KitWare.OpenSubKey(cMakeFolder);

            string path = cMake.GetValue(null).ToString();


            cMake.Dispose();


            var procStartInfo = new ProcessStartInfo(Path.GetFullPath(String.Format("{0}\\bin\\cmake.exe", path)));

            String TrinityCoreLocation = String.Empty;

            if (!Directory.Exists(Path.Combine(Path.GetFullPath(Settings.Default.trunkLocation), "TrinityCore")))
                TrinityCoreLocation = Path.GetFullPath(Settings.Default.trunkLocation);
            else
                TrinityCoreLocation =
                Path.GetFullPath(
                Path.Combine(Path.Combine(Path.GetFullPath(Settings.Default.trunkLocation), "TrinityCore")));

            if (Settings.Default.trinityCoreCompilePlatform == 1)
                procStartInfo.Arguments = String.Format("-G \"Visual Studio 10 Win64\" \"{0}\"",
                Path.GetFullPath(TrinityCoreLocation));
            else
                procStartInfo.Arguments = String.Format("-G \"Visual Studio 10\" \"{0}\"",
                Path.GetFullPath(TrinityCoreLocation));


            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            procStartInfo.RedirectStandardOutput = true;

            procStartInfo.WorkingDirectory = dirname;

            using (var cmake = new Process() { StartInfo = procStartInfo })
            {
                cmake.Start();

                cmake.EnableRaisingEvents = true;

                cmake.BeginOutputReadLine();

                cmake.OutputDataReceived += cmake_OutputDataReceived;

                cmake.WaitForExit();
            }


            //Compile TrinityCore//

            procStartInfo = new ProcessStartInfo(CompilePath);

            procStartInfo.UseShellExecute = false;

            procStartInfo.RedirectStandardOutput = true;

            procStartInfo.CreateNoWindow = true;

            if (isDevenv)
            {
                if (Settings.Default.trinityCoreCompilePlatform == 1)
                    procStartInfo.Arguments = String.Format("\"{0}\\TrinityCore.sln\" /Clean \"Release|x64\"", dirname);
                else
                    procStartInfo.Arguments = String.Format("\"{0}\\TrinityCore.sln\" /Clean Release", dirname);
            }
            else
                procStartInfo.Arguments = String.Format("\"{0}\\TrinityCore.sln\" /t:Clean /p:Configuration=Release",
                dirname);

            using (var clean = new Process() { StartInfo = procStartInfo })
            {
                clean.Start();

                clean.EnableRaisingEvents = true;

                clean.BeginOutputReadLine();

                clean.OutputDataReceived += clean_OutputDataReceived;


                clean.WaitForExit();
            }

            procStartInfo = new ProcessStartInfo(CompilePath);

            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            procStartInfo.RedirectStandardOutput = true;

            if (isDevenv)
            {
                if (Settings.Default.trinityCoreCompilePlatform == 1)
                    procStartInfo.Arguments = String.Format("\"{0}\\TrinityCore.sln\" /Build \"Release|x64\"", dirname);
                else
                    procStartInfo.Arguments = String.Format("\"{0}\\TrinityCore.sln\" /Build Release", dirname);
            }
            else
                procStartInfo.Arguments = String.Format("\"{0}\\TrinityCore.sln\" /t:Build /p:Configuration=Release",
                dirname);

            using (var compile = new Process() { StartInfo = procStartInfo })
            {
                compile.Start();

                compile.EnableRaisingEvents = true;

                compile.BeginOutputReadLine();

                compile.OutputDataReceived += compile_OutputDataReceived;
                compile.Exited += compile_Exited;


                compile.WaitForExit();
            }
        }

        private void clean_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Invoke((MethodInvoker) delegate
                {
                    outputTextBoxX.AppendText(e.Data + Environment.NewLine);
                    outputTextBoxX.SelectionLength = 0;
                    outputTextBoxX.SelectionStart = outputTextBoxX.Text.Length;
                    outputTextBoxX.ScrollToCaret();
                });
            }
        }

        private void cmake_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Invoke((MethodInvoker) delegate
                {
                    outputTextBoxX.AppendText(e.Data + Environment.NewLine);
                    outputTextBoxX.SelectionLength = 0;
                    outputTextBoxX.SelectionStart = outputTextBoxX.Text.Length;
                    outputTextBoxX.ScrollToCaret();
                });
            }
        }

        private void compile_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string message = e.Data;

                Invoke((MethodInvoker) delegate
                {
                    if (message.Contains("0 failed") || message.Contains("Build Succeeded") ||
                    message.Contains("0 Error") || message.Contains("0 Error(s)"))
                        compileFinishSuccess = true;

                    outputTextBoxX.AppendText(message + Environment.NewLine);
                    outputTextBoxX.SelectionLength = 0;
                    outputTextBoxX.SelectionStart = outputTextBoxX.Text.Length;
                    outputTextBoxX.ScrollToCaret();
                });
            }
        }

        private void compile_Exited(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                compileCircularProgressItem.Stop();

                compileButtonItem.Enabled = true;

                if (compileFinishSuccess)
                {
                    try
                    {
                        var Trunkdi =
                        new DirectoryInfo(
                        Path.Combine(
                        String.Format("{0}\\TrinityBuild",
                        Environment.GetEnvironmentVariable(
                        "SystemDrive")), "bin\\Release\\"));
                        var Serverdi = new DirectoryInfo(Settings.Default.trinityFolder);

                        foreach (FileInfo fi in Trunkdi.GetFiles())
                        {
                            if (File.Exists(Path.Combine(Serverdi.FullName, fi.Name)))
                                File.Delete(Path.Combine(Serverdi.FullName, fi.Name));

                            File.Copy(fi.FullName, Path.Combine(Serverdi.FullName, fi.Name));
                        }
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop,
                        "Error!", ex.Message,
                        eTaskDialogButton.Ok));

                        return;
                    }

                    TaskDialog.Show(new TaskDialogInfo("Success",
                    eTaskDialogIcon.Information2,
                    "Success!",
                    "Compile has completed successfully. Files have been moved to your server folder.",
                    eTaskDialogButton.Ok));
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Failed", eTaskDialogIcon.Stop,
                    "Compile Failed!",
                    "Failed to compile TrinityCore.",
                    eTaskDialogButton.Ok));
                }
            });
        }

        private void TrinityCoreCompileBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            compileCircularProgressItem.Stop();
        }

        private void backupDBButtonItem_Click(object sender, EventArgs e)
        {
            RegistryKey MySQLServer = null;

            if (Environment.Is64BitOperatingSystem)
            {
                MySQLServer = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\MySQL AB");
            }
            else
            {
                MySQLServer = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MySQL AB");
            }


            if (MySQLServer != null)
            {
                RegistryKey regKey = null;

                foreach (string key in MySQLServer.GetSubKeyNames())
                {
                    RegistryKey reg = MySQLServer.OpenSubKey(key);

                    if (reg.Name.ToLower().Contains("mysql server"))
                    {
                        regKey = reg;
                    }
                }

                if (regKey != null)
                {
                    dbCircularProgressItem.Start();

                    dbBackupBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Not Found", eTaskDialogIcon.Stop, "MySQL Server Not Found",
                    "You can download it here: http://dev.mysql.com/downloads/mysql/",
                    eTaskDialogButton.Ok));
                }
            }
            else
            {
                TaskDialog.Show(new TaskDialogInfo("Not Found", eTaskDialogIcon.Stop, "MySQL Server Not Found",
                "You can download it here: http://dev.mysql.com/downloads/mysql/",
                eTaskDialogButton.Ok));
            }
        }

        private void dbBackupBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RegistryKey MySQLServer = null;

                if (Environment.Is64BitOperatingSystem)
                {
                    MySQLServer = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\MySQL AB");
                }
                else
                {
                    MySQLServer = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MySQL AB");
                }

                if (MySQLServer != null)
                {
                    RegistryKey regKey = null;

                    foreach (string key in MySQLServer.GetSubKeyNames())
                    {
                        RegistryKey reg = MySQLServer.OpenSubKey(key);

                        if (reg.Name.ToLower().Contains("mysql server"))
                        {
                            regKey = reg;
                        }
                    }

                    if (regKey != null)
                    {
                        string dir = String.Format("{0}\\TrinityCore Manager",
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                        string outLocation =
                        String.Format("{0}\\TrinityCore Manager\\backups\\Backup-{1:yyyy-MM-dd_hh-mm-ss-tt}.sql",
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DateTime.Now);

                        Console.WriteLine(outLocation);

                        string MySQLlocation = regKey.GetValue("Location").ToString();
                        string args = String.Format("-u{0} -p{1} --result-file=\"{2}\" --databases {3} {4} {5}",
                        Settings.Default.MySQLUsername, Settings.Default.MySQLPassword,
                        outLocation, Settings.Default.AuthDB, Settings.Default.CharactersDB,
                        Settings.Default.WorldDB);

                        string location = String.Format("{0}bin\\mysqldump.exe", MySQLlocation);

                        if (
                        !Directory.Exists(String.Format("{0}\\TrinityCore Manager",
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.MyDocuments))))
                            Directory.CreateDirectory(String.Format("{0}\\TrinityCore Manager",
                            Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments)));


                        if (
                        !Directory.Exists(String.Format("{0}\\TrinityCore Manager\\backups",
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.MyDocuments))))
                            Directory.CreateDirectory(String.Format("{0}\\TrinityCore Manager\\backups",
                            Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments)));

                        var psi = new ProcessStartInfo(location) { UseShellExecute = false, CreateNoWindow = true, Arguments = args, WorkingDirectory = Directory.GetCurrentDirectory() };

                        using (var backupProc = new Process() { StartInfo = psi })
                        {
                            backupProc.Start();

                            backupProc.EnableRaisingEvents = true;

                            backupProc.Exited += backupProc_Exited;


                            backupProc.WaitForExit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogType.Error);
            }
        }

        private void backupProc_Exited(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                dbCircularProgressItem.Stop();

                TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2,
                "Backup Finished",
                "Finished backing up the database",
                eTaskDialogButton.Ok));
            });
        }

        private void restoreDBButtonItem_Click(object sender, EventArgs e)
        {
            using (var restoreDB = new RestoreDB())
            {
                restoreDB.ShowDialog();
            }
        }

        private void createItemButtonItem_Click(object sender, EventArgs e)
        {
            using (var iCreator = new ItemCreator())
            {
                iCreator.ShowDialog();
            }
        }

        private void searchEntryIDButtonItem_Click(object sender, EventArgs e)
        {
            using (var searchEntryID = new SearchItemID())
            {
                searchEntryID.FormClosed += searchEntryID_FormClosed;

                searchEntryID.searchType = SearchItemID.SearchType.Item;


                searchEntryID.ShowDialog();
            }
        }

        private void searchEntryID_FormClosed(object sender, FormClosedEventArgs e)
        {
            var searchEntryID = (SearchItemID) sender;

            if (searchEntryID.GetEntryID() != 0)
                TaskDialog.Show(new TaskDialogInfo("Entry ID", eTaskDialogIcon.Information2, "Item Entry ID",
                "The item entry id is: " + searchEntryID.GetEntryID(),
                eTaskDialogButton.Ok));
        }

        private void editItemButtonItem_Click(object sender, EventArgs e)
        {
            string itemID = String.Empty;

            if (Methods.InputBox("Entry ID", "What is the entry id of the item?", ref itemID) == DialogResult.OK)
            {
                int id = 0;

                if (int.TryParse(itemID, out id))
                {
                    using (var itemEdit = new ItemCreator() { editor = true, editItemID = id })
                    {
                        itemEdit.ShowDialog();
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "The entry id must be a number", eTaskDialogButton.Ok));
                }
            }
        }

        private void createWeaponButtonItem_Click(object sender, EventArgs e)
        {
            using (var wCreator = new WeaponCreator())
            {
                wCreator.ShowDialog();
            }
        }

        private void editWeaponButtonItem_Click(object sender, EventArgs e)
        {
            string itemID = String.Empty;

            if (Methods.InputBox("Entry ID", "What is the entry id of the weapon?", ref itemID) == DialogResult.OK)
            {
                int id = 0;

                if (int.TryParse(itemID, out id))
                {
                    using (var wEditor = new WeaponCreator() { editor = true, editItemID = id })
                    {
                        wEditor.ShowDialog();
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "The entry id must be a number", eTaskDialogButton.Ok));
                }
            }
        }

        private void createArmorButtonItem_Click(object sender, EventArgs e)
        {
            using (var aCreator = new ArmorCreator())
            {
                aCreator.ShowDialog();
            }
        }

        private void editArmorButtonItem_Click(object sender, EventArgs e)
        {
            string itemID = String.Empty;

            if (Methods.InputBox("Entry ID", "What is the entry id of the armor?", ref itemID) == DialogResult.OK)
            {
                int id = 0;

                if (int.TryParse(itemID, out id))
                {
                    using (var aEditor = new ArmorCreator() { editor = true, editItemID = id })
                    {
                        aEditor.ShowDialog();
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "The entry id must be a number", eTaskDialogButton.Ok));
                }
            }
        }

        private void createNPCButtonItem_Click(object sender, EventArgs e)
        {
            using (var nCreator = new NPCCreator())
            {
                nCreator.ShowDialog();
            }
        }

        private void editNPCButtonItem_Click(object sender, EventArgs e)
        {
            string npcID = String.Empty;

            if (Methods.InputBox("Entry ID", "What is the entry ID of the NPC?", ref npcID) == DialogResult.OK)
            {
                int id = 0;

                if (int.TryParse(npcID, out id))
                {
                    using (var cCreator = new NPCCreator() { editor = true, editNPCID = id })
                    {
                        cCreator.ShowDialog();
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "The entry id must be a number", eTaskDialogButton.Ok));
                }
            }
        }

        private void applyCoreUpdatesButtonItem_Click(object sender, EventArgs e)
        {
            if (updateCoreBackgroundWorker.IsBusy)
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Busy",
                "An update is already in progress. Please wait until it has finished",
                eTaskDialogButton.Ok));

                return;
            }


            if (Settings.Default.trunkLocation == String.Empty)
            {
                TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop,
                "You must set the trunk location first!", String.Empty,
                eTaskDialogButton.Ok));

                return;
            }

            TaskDialog.Show(new TaskDialogInfo("Notice", eTaskDialogIcon.Bulb, "Notice",
            "Remember to update your TrinityCore trunk folder to get the latest updates.",
            eTaskDialogButton.Ok));


            string loc = Settings.Default.trunkLocation;
            string sqlFolder = String.Empty;

            if (Directory.Exists(Path.Combine(loc, "TrinityCore", ".git")))
            {
                sqlFolder = Path.Combine(loc, "TrinityCore", "sql");
            }
            else
                if (Directory.Exists(Path.Combine(loc, ".git")))
                {
                    sqlFolder = Path.Combine(loc, "sql");
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error",
                    "Could not find the folder \"sql\"", eTaskDialogButton.Ok));

                    return;
                }

            string updatesFolder = Path.Combine(sqlFolder, "updates");

            dbCircularProgressItem.Start();

            updateCoreBackgroundWorker.RunWorkerAsync(updatesFolder);
        }

        private void updateCoreBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mysql == null)
                return;

            var dInfo = new DirectoryInfo(e.Argument as string);

            FileInfo[] fInfoArr = dInfo.GetFiles("*.*", SearchOption.AllDirectories);


            var worldFiles = new List<FileInfo>();
            var charFiles = new List<FileInfo>();

            foreach (FileInfo fInfo in fInfoArr)
            {
                if (fInfo.Name.Contains("_world_"))
                    worldFiles.Add(fInfo);
                else
                    if (fInfo.Name.Contains("_characters_"))
                        charFiles.Add(fInfo);
            }


            mysql.ExecuteMySQLFiles(worldFiles, Settings.Default.WorldDB);

            mysql.ExecuteMySQLFiles(charFiles, Settings.Default.CharactersDB);
        }

        private void updateCoreBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dbCircularProgressItem.Stop();

            TaskDialog.Show(new TaskDialogInfo("Finished", eTaskDialogIcon.Information2,
            "Finished Applying Core Database Updates", String.Empty,
            eTaskDialogButton.Ok));
        }

        private void createVendorButtonItem_Click(object sender, EventArgs e)
        {
            using (var vCreator = new VendorCreator())
            {
                vCreator.ShowDialog();
            }
        }

        private void editVendorButtonItem_Click(object sender, EventArgs e)
        {
            string vendorID = String.Empty;

            if (Methods.InputBox("Entry ID", "What is the entry ID of the vendor?", ref vendorID) == DialogResult.OK)
            {
                int id = 0;

                if (int.TryParse(vendorID, out id))
                {
                    using (var vEditor = new VendorCreator() { editor = true, editVendorID = id })
                    {
                        vEditor.ShowDialog();
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "The entry id must be a number", eTaskDialogButton.Ok));
                }
            }
        }

        private void createLootButtonItem_Click(object sender, EventArgs e)
        {
            using (var lCreator = new LootCreator())
            {
                lCreator.ShowDialog();
            }
        }

        private void editLootButtonItem_Click(object sender, EventArgs e)
        {
            string lootID = String.Empty;

            if (Methods.InputBox("Entry ID", "What is the entry ID of the loot?", ref lootID) == DialogResult.OK)
            {
                int id = 0;

                if (int.TryParse(lootID, out id))
                {
                    using (var lEditor = new LootCreator() { editor = true, editLootID = id })
                    {
                        lEditor.ShowDialog();
                    }
                }
                else
                {
                    TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                    "The entry id must be a number", eTaskDialogButton.Ok));
                }
            }
        }

        private void searchNPCIDButtonX_Click(object sender, EventArgs e)
        {
            using (var searchNPCID = new SearchNPCID())
            {
                searchNPCID.SubmitButtonPressed += searchNPCID_SubmitButtonPressed;

                searchNPCID.ShowDialog();
            }
        }

        private void searchNPCID_SubmitButtonPressed(object sender, EventArgs e)
        {
            var searchNPCID = (SearchNPCID) sender;

            if (searchNPCID.GetEntryID() != 0)
                TaskDialog.Show(new TaskDialogInfo("Entry ID", eTaskDialogIcon.Information2, "NPC Entry ID",
                "The item entry id is: " + searchNPCID.GetEntryID(),
                eTaskDialogButton.Ok));
        }

        private void exitButtonItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sendMailButtonItem_Click(object sender, EventArgs e)
        {
            using (var sMail = new SendMail())
            {
                sMail.MailSubmitted += sMail_MailSubmitted;

                sMail.ShowDialog();
            }
        }

        private void sMail_MailSubmitted(object sender, SendMail.SendMailSubmittedEventArgs e)
        {
            if (e.gold != 0 && e.silver != 0 && e.copper != 0)
            {
                int totalmoney = (Convert.ToInt32(e.copper) % 100) + (Convert.ToInt32(e.silver) * 100) + (e.gold * 10000);

                SendToServer(String.Format("{0} {1} \"{2}\" \"{3}\" {4}", Constants.SendMoney, e.userName, e.subject,
                e.message, totalmoney));
            }
            else
            {
                SendToServer(String.Format("{0} {1} \"{2}\" \"{3}\"", Constants.SendMail, e.userName, e.subject,
                e.message));
            }

            if (e.itemEntryID != 0)
            {
                SendToServer(String.Format("{0} {1} \"{2}\" \"{3}\" {4}:1", Constants.SendItem, e.userName, e.subject,
                e.message, e.itemEntryID));
            }

            TaskDialog.Show(new TaskDialogInfo("Success", eTaskDialogIcon.Information2, "Mail Sent!",
            String.Format("You have sent mail to: {0}", e.userName),
            eTaskDialogButton.Ok));
        }

        private void automaticUpdater_Cancelled(object sender, EventArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.CheckForUpdatesMenu;
        }

        private void automaticUpdater_BeforeChecking(object sender, BeforeArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.CancelCheckingMenu;
        }

        private void automaticUpdater_BeforeDownloading(object sender, BeforeArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.CancelUpdatingMenu;
        }

        private void automaticUpdater_UpdateAvailable(object sender, EventArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.DownloadUpdateMenu;
        }

        private void automaticUpdater_ReadyToBeInstalled(object sender, EventArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.InstallUpdateMenu;
        }

        private void automaticUpdater_UpToDate(object sender, SuccessArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.CheckForUpdatesMenu;
        }

        private void automaticUpdater_CheckingFailed(object sender, FailArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.CheckForUpdatesMenu;
        }

        private void automaticUpdater_DownloadingOrExtractingFailed(object sender, FailArgs e)
        {
            checkForUpdatesButtonItem.Text = automaticUpdater.Translation.CheckForUpdatesMenu;
        }

        private void chgCharLevelButtonItem_Click(object sender, EventArgs e)
        {
            if (charactersComboBoxItem.SelectedIndex != -1)
            {
                string level = String.Empty;

                if (Methods.InputBox("Change Level", "What level?", ref level) == DialogResult.OK)
                {
                    int numLevel = Int32.MinValue;

                    int.TryParse(level, out numLevel);

                    if (numLevel != Int32.MinValue)
                    {
                        if (charactersComboBoxItem.SelectedIndex != -1)
                            SendToServer(String.Format("{0} {1} {2}", Constants.ChangeCharLevel,
                            charactersComboBoxItem.SelectedItem, numLevel));
                    }
                    else
                    {
                        TaskDialog.Show(new TaskDialogInfo("Error", eTaskDialogIcon.Stop, "Error!",
                        "The level must be a number", eTaskDialogButton.Ok));
                    }
                }
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                NotifyIcon.ShowBalloonTip(2);
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void clearConsoleTimer_Tick(object sender, EventArgs e)
        {
            consoleTextBoxX.Text = String.Empty;
        }
    }
}
