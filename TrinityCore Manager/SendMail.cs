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

namespace TrinityCore_Manager
{
    public partial class SendMail : DevComponents.DotNetBar.Office2007Form
    {
        public SendMail()
        {
            InitializeComponent();
        }

        private SQLMethods mysql = null;

        private void SendMail_Load(object sender, EventArgs e)
        {
            mysql = new SQLMethods(Settings.Default.MySQLHost, Settings.Default.MySQLPort, Settings.Default.MySQLUsername, Settings.Default.MySQLPassword);

            mysqlBackgroundWorker.RunWorkerAsync();

        }

        Dictionary<string, List<string>> charDict = new Dictionary<string, List<string>>();

        private void mysqlBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            charDict = mysql.ReadAll(Settings.Default.CharactersDB, "characters");
        }

        private void mysqlBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (charDict != null && charDict.ContainsKey("account") && charDict.ContainsKey("online") && charDict.ContainsKey("name"))
            {
                for (int i = 0; i < charDict["account"].Count; i++)
                {
                    if (int.Parse(charDict["online"][i]) == 1)
                    {
                        onlineUsersComboBoxEx.Items.Add(charDict["name"][i]);
                    }
                    else
                    {
                        offlineUsersComboBoxEx.Items.Add(charDict["name"][i]);
                    }
                }
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (onlineUsersComboBoxEx.SelectedIndex != -1 && offlineUsersComboBoxEx.SelectedIndex != -1)
            {

                string userName = String.Empty;

                if (onlineUsersComboBoxEx.SelectedIndex != -1)
                    userName = onlineUsersComboBoxEx.SelectedItem.ToString();
                else if (offlineUsersComboBoxEx.SelectedIndex != -1)
                    userName = offlineUsersComboBoxEx.SelectedItem.ToString();

                if (subjectTextBoxX.Text != String.Empty && messageTextBoxX.Text != String.Empty)
                {
                    if (MailSubmitted != null)
                        MailSubmitted(this, new SendMailSubmittedEventArgs(userName, subjectTextBoxX.Text, messageTextBoxX.Text, itemIDIntegerInput.Value, goldIntegerInput.Value, silverIntegerInput.Value, copperIntegerInput.Value));

                    this.Close();
                }
            }
        }

        private void onlineUsersComboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            offlineUsersComboBoxEx.SelectedIndex = -1;
            offlineUsersComboBoxEx.SelectedIndex = -1;
        }

        private void offlineUsersComboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            onlineUsersComboBoxEx.SelectedIndex = -1;
            onlineUsersComboBoxEx.SelectedIndex = -1;
        }

        public delegate void SendMailSubmittedEventHandler(object sender, SendMailSubmittedEventArgs e);

        public event SendMailSubmittedEventHandler MailSubmitted;

        public class SendMailSubmittedEventArgs : EventArgs
        {
            public string userName { get; set; }
            public int itemEntryID { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
            public int gold { get; set; }
            public int silver { get; set; }
            public int copper { get; set; }


            public SendMailSubmittedEventArgs(string user, string subj, string msg, int itemEntry, int goldAmnt, int silverAmnt, int copperAmnt)
            {
                userName = user;
                subject = subj;
                message = msg;
                itemEntryID = itemEntry;
                gold = goldAmnt;
                silver = silverAmnt;
                copper = copperAmnt;
            }

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            offlineUsersComboBoxEx.SelectedIndex = -1;
            offlineUsersComboBoxEx.SelectedIndex = -1;

            onlineUsersComboBoxEx.SelectedIndex = -1;
            onlineUsersComboBoxEx.SelectedIndex = -1;

            itemIDIntegerInput.Value = 0;
            subjectTextBoxX.Text = String.Empty;
            messageTextBoxX.Text = String.Empty;
            goldIntegerInput.Value = 0;
            silverIntegerInput.Value = 0;
            copperIntegerInput.Value = 0;
        }

    }
}
