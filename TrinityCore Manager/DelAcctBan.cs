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
    public partial class DelAcctBan : DevComponents.DotNetBar.Office2007Form
    {
        public DelAcctBan()
        {
            InitializeComponent();
        }

        public List<string> acctList = new List<string>();

        private void DelAcctBan_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < acctList.Count; i++)
            {
                accountComboBoxItem.Items.Add(acctList[i]);
            }

            if (accountComboBoxItem.Items.Count != 0)
                accountComboBoxItem.SelectedIndex = 0;

        }

        private void delAcctBanButtonX_Click(object sender, EventArgs e)
        {
            if (accountComboBoxItem.SelectedIndex != -1)
            {
                if (AcctDelBanSubmitted != null)
                    AcctDelBanSubmitted(this, new DelBanSubmittedEventArgs(accountComboBoxItem.SelectedItem.ToString()));

                this.Close();

            }
        }

        public delegate void DelBanSubmittedEventHandler(object sender, DelBanSubmittedEventArgs e);

        public event DelBanSubmittedEventHandler AcctDelBanSubmitted;

        public class DelBanSubmittedEventArgs : EventArgs
        {
            public string account { get; set; }

            public DelBanSubmittedEventArgs(string acct)
            {
                account = acct;
            }
        }

    }
}
