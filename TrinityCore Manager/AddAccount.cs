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

namespace TrinityCore_Manager
{
    public partial class AddAccount : DevComponents.DotNetBar.Office2007Form
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        public delegate void AccountAddedEventHandler(object sender, AccountAddedEventArgs e);

        public event AccountAddedEventHandler AccountAdded;

        public class AccountAddedEventArgs : EventArgs
        {
            public string account { get; set; }
            public string password { get; set; }
            public int gmlevel { get; set; }

            public AccountAddedEventArgs(string acct, string pwd, int level)
            {
                account = acct;
                password = pwd;
                gmlevel = level;
            }
        }

        private void AddAccount_Load(object sender, EventArgs e)
        {

        }

        private void cButtonX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButtonX_Click(object sender, EventArgs e)
        {
            if (accountNameTextBoxX.Text != String.Empty && passwordTextBoxX.Text != String.Empty)
            {
                if (AccountAdded != null)
                    AccountAdded(this, new AccountAddedEventArgs(accountNameTextBoxX.Text, passwordTextBoxX.Text, GMLevelIntegerInput.Value));

                this.Close();
            }
        }
    }
}
