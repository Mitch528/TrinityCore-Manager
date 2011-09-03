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
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace TrinityCore_Manager
{

    internal class App : WindowsFormsApplicationBase
    {
        public App()
            : base()
        {
            this.IsSingleInstance = true;
            this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
        }

        protected override void OnCreateSplashScreen()
        {
            base.OnCreateSplashScreen();

            this.SplashScreen = new Splash();
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();

            this.MainForm = new MainForm();
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var app = new App();

            app.Run(Environment.GetCommandLineArgs());

        }
    }
}
