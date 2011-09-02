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

namespace TrinityCore_Manager
{
    public partial class EditAccount : DevComponents.DotNetBar.Office2007Form
    {

        public string accountName = String.Empty;
        public int gmLevel = 0;
        public int expansion = 0;


        public EditAccount()
        {
            InitializeComponent();
        }

        private void EditAccount_Load(object sender, EventArgs e)
        {
            accountNameLabelX.Text = "Account: " + accountName;
            accountNameLabelX.Size = TextRenderer.MeasureText(accountNameLabelX.Text, accountNameLabelX.Font);
            accountNameLabelX.Left = (this.Width / 2) - (accountNameLabelX.Width / 2);

            gmLevelIntegerInput.Value = gmLevel;
            expansionIntegerInput.Value = expansion;
            
        }

        private void cButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            AccountModified(this, new AccountModifiedEventArgs(accountName, passwordTextBoxX.Text, gmLevelIntegerInput.Value, expansionIntegerInput.Value));

            this.Close();
        }

        public delegate void AccountModifiedEventHandler(object sender, AccountModifiedEventArgs e);

        public event AccountModifiedEventHandler AccountModified;

        public class AccountModifiedEventArgs : EventArgs
        {
            public string accountName { get; set; }
            public string password { get; set; }
            public int gmLevel { get; set; }
            public int expansion { get; set; }

            public AccountModifiedEventArgs(string acct, string pwd, int gm, int expan)
            {
                accountName = acct;
                gmLevel = gm;
                expansion = expan;
                password = pwd;
            }
        }

    }
}
