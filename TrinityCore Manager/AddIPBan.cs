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
    public partial class AddIPBan : DevComponents.DotNetBar.Office2007Form
    {
        public AddIPBan()
        {
            InitializeComponent();
        }

        private void submitBanButtonX_Click(object sender, EventArgs e)
        {
            if (ipAddressInput.Value != String.Empty)
            {
                if (IPBanSubmitted != null)
                    IPBanSubmitted(this, new IPBanSubmittedEventArgs(ipAddressInput.Value));

                this.Close();
            }
        }

        public delegate void IPBanSubmittedEventHandler(object sender, IPBanSubmittedEventArgs e);

        public event IPBanSubmittedEventHandler IPBanSubmitted;

        public class IPBanSubmittedEventArgs : EventArgs
        {
            public string ipBan { get; set; }

            public IPBanSubmittedEventArgs(string ip)
            {
                ipBan = ip;
            }
        }

    }
}
