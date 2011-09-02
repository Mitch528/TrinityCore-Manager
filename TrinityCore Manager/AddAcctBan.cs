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
    public partial class AddAcctBan : DevComponents.DotNetBar.Office2007Form
    {
        public AddAcctBan()
        {
            InitializeComponent();
        }

        public List<string> acctList = new List<string>();

        private void AddAcctBan_Load(object sender, EventArgs e)
        {
            foreach (string acct in acctList)
            {
                accountComboBoxItem.Items.Add(acct);
            }

            if (accountComboBoxItem.Items.Count != 0)
                accountComboBoxItem.SelectedIndex = 0;

        }

        private void addAcctBanButtonX_Click(object sender, EventArgs e)
        {
            if (accountComboBoxItem.SelectedIndex != -1)
            {
                if (AcctBanSubmitted != null)
                    AcctBanSubmitted(this, new AcctBanSubmittedEventArgs(accountComboBoxItem.SelectedItem.ToString()));

                this.Close();

            }
        }

        public delegate void AcctBanSubmittedEventHandler(object sender, AcctBanSubmittedEventArgs e);

        public event AcctBanSubmittedEventHandler AcctBanSubmitted;

        public class AcctBanSubmittedEventArgs : EventArgs
        {
            public string account { get; set; }

            public AcctBanSubmittedEventArgs(string acct)
            {
                account = acct;
            }
        }

    }
}
