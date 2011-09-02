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
    public partial class DelIPBan : DevComponents.DotNetBar.Office2007Form
    {

        public List<string> ipBanList = new List<string>();

        public DelIPBan()
        {
            InitializeComponent();
        }

        private void DelIPBan_Load(object sender, EventArgs e)
        {
            foreach (string ip in ipBanList)
            {
                ipAddressComboBoxItemEX.Items.Add(ip);
            }

            if (ipAddressComboBoxItemEX.Items.Count != 0)
                ipAddressComboBoxItemEX.SelectedIndex = 0;

        }

        private void deleteIPBanButtonX_Click(object sender, EventArgs e)
        {
            if (ipAddressComboBoxItemEX.SelectedIndex != -1)
            {
                if (DelIPBanSubmitted != null)
                    DelIPBanSubmitted(this, new DelIPBanSubmittedEventArgs(ipAddressComboBoxItemEX.SelectedItem.ToString()));

                this.Close();

            }
        }

        public delegate void DelIPBanSubmmitedEventHandler(object sender, DelIPBanSubmittedEventArgs e);

        public event DelIPBanSubmmitedEventHandler DelIPBanSubmitted;

        public class DelIPBanSubmittedEventArgs : EventArgs
        {
            public string ipAddress { get; set; }

            public DelIPBanSubmittedEventArgs(string ip)
            {
                ipAddress = ip;
            }
        }

    }
}
