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
using System.Text;
using DevComponents.DotNetBar;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar;
using TrinityCore_Manager.Properties;
using System.IO;

namespace TrinityCore_Manager
{
    class Methods
    {
        public static Process GetProcByName(string name)
        {

            Process process = null;

            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName == name)
                    process = proc;
            }

            return process;
        }

        public static Process[] GetProcsByNameArray(string[] names)
        {

            Process[] procs = new Process[names.Length];

            int i = 0;

            foreach (Process proc in Process.GetProcesses())
            {

                foreach (string name in names)
                {
                    if (proc.ProcessName == name)
                    {
                        procs[i] = proc;

                        i++;
                    }
                }
            }

            return procs;

        }

        public static bool KillProcByName(string name)
        {
            Process proc = GetProcByName(name);

            try
            {
                try
                {
                    proc.Kill();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, Logger.LogType.Error);
                }

                return true;

            }
            catch
            {
            }

            return false;
        }

        public static bool KillProcsByNameArray(string[] name)
        {
            Process[] procs = GetProcsByNameArray(name);

            try
            {
                foreach (Process p in procs)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Logger.LogType.Error);
                    }
                }

                return true;

            }
            catch
            {
            }

            return false;
        }

        public static bool KillProcByPID(int pid)
        {
            Process proc = Process.GetProcessById(pid);


            try
            {
                proc.Kill();

                return true;
            }
            catch
            {
            }

            return false;

        }

        public static bool CheckIfProcExistsByName(string name)
        {
            bool exists = false;

            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == name)
                    exists = true;
            }

            return exists;

        }

        public static bool CheckIfProcExistsByPID(int pid)
        {
            bool exists = false;

            foreach (Process p in Process.GetProcesses())
            {
                if (p.Id == pid)
                    exists = true;
            }

            return exists;

        }

        public static string CombineTextFiles(List<string> files)
        {

            StringBuilder sb = new StringBuilder();

            foreach (string f in files)
            {
                using (StreamReader reader = new StreamReader(f))
                {
                    sb.Append(reader.ReadToEnd());
                }
            }

            return sb.ToString();
        }

        public static void DeleteDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory);
            string[] dirs = Directory.GetDirectories(directory);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(directory, true);

        }

        /////////////////////// http://www.csharp-examples.net/inputbox/ //////////////////////
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Office2007Form form = new Office2007Form();
            LabelX label = new LabelX();
            DevComponents.DotNetBar.Controls.TextBoxX textBox = new DevComponents.DotNetBar.Controls.TextBoxX();
            ButtonX buttonOk = new ButtonX();
            ButtonX buttonCancel = new ButtonX();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;


            textBox.Border.Class = "TextBoxBorder";
            textBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            textBox.Location = new System.Drawing.Point(240, 151);
            textBox.Name = "textbox";
            textBox.ReadOnly = false;
            textBox.Size = new System.Drawing.Size(275, 20);
            textBox.TabIndex = 1;

            buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;

            buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;


            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            form.EnableGlass = false;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }


    }
}
