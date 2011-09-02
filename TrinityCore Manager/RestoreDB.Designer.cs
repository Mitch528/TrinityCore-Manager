namespace TrinityCore_Manager
{
    partial class RestoreDB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreDB));
            this.restoreButtonX = new DevComponents.DotNetBar.ButtonX();
            this.restoreDBdateTimeInput = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.restoreDBBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.restoreCircularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.backupListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.restoreDBdateTimeInput)).BeginInit();
            this.SuspendLayout();
            // 
            // restoreButtonX
            // 
            this.restoreButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.restoreButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.restoreButtonX.Location = new System.Drawing.Point(192, 416);
            this.restoreButtonX.Name = "restoreButtonX";
            this.restoreButtonX.Size = new System.Drawing.Size(75, 23);
            this.restoreButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.restoreButtonX.TabIndex = 2;
            this.restoreButtonX.Text = "Restore";
            this.restoreButtonX.Click += new System.EventHandler(this.restoreButtonX_Click);
            // 
            // restoreDBdateTimeInput
            // 
            // 
            // 
            // 
            this.restoreDBdateTimeInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.restoreDBdateTimeInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.restoreDBdateTimeInput.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.restoreDBdateTimeInput.ButtonDropDown.Visible = true;
            this.restoreDBdateTimeInput.IsPopupCalendarOpen = false;
            this.restoreDBdateTimeInput.Location = new System.Drawing.Point(123, 33);
            // 
            // 
            // 
            this.restoreDBdateTimeInput.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.restoreDBdateTimeInput.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.restoreDBdateTimeInput.MonthCalendar.BackgroundStyle.Class = "";
            this.restoreDBdateTimeInput.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.restoreDBdateTimeInput.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.restoreDBdateTimeInput.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.restoreDBdateTimeInput.MonthCalendar.DisplayMonth = new System.DateTime(2011, 5, 1, 0, 0, 0, 0);
            this.restoreDBdateTimeInput.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.restoreDBdateTimeInput.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.restoreDBdateTimeInput.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.restoreDBdateTimeInput.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.restoreDBdateTimeInput.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.restoreDBdateTimeInput.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.restoreDBdateTimeInput.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.restoreDBdateTimeInput.MonthCalendar.TodayButtonVisible = true;
            this.restoreDBdateTimeInput.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.restoreDBdateTimeInput.Name = "restoreDBdateTimeInput";
            this.restoreDBdateTimeInput.Size = new System.Drawing.Size(200, 20);
            this.restoreDBdateTimeInput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.restoreDBdateTimeInput.TabIndex = 3;
            this.restoreDBdateTimeInput.ValueChanged += new System.EventHandler(this.restoreDBdateTimeInput_ValueChanged);
            // 
            // restoreDBBackgroundWorker
            // 
            this.restoreDBBackgroundWorker.WorkerSupportsCancellation = true;
            this.restoreDBBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.restoreDBBackgroundWorker_DoWork);
            this.restoreDBBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.restoreDBBackgroundWorker_RunWorkerCompleted);
            // 
            // restoreCircularProgress
            // 
            // 
            // 
            // 
            this.restoreCircularProgress.BackgroundStyle.Class = "";
            this.restoreCircularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.restoreCircularProgress.Location = new System.Drawing.Point(273, 416);
            this.restoreCircularProgress.Name = "restoreCircularProgress";
            this.restoreCircularProgress.Size = new System.Drawing.Size(38, 23);
            this.restoreCircularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.restoreCircularProgress.TabIndex = 4;
            // 
            // backupListBox
            // 
            this.backupListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupListBox.FormattingEnabled = true;
            this.backupListBox.ItemHeight = 16;
            this.backupListBox.Location = new System.Drawing.Point(13, 72);
            this.backupListBox.Name = "backupListBox";
            this.backupListBox.Size = new System.Drawing.Size(443, 324);
            this.backupListBox.TabIndex = 5;
            // 
            // RestoreDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 451);
            this.Controls.Add(this.backupListBox);
            this.Controls.Add(this.restoreCircularProgress);
            this.Controls.Add(this.restoreDBdateTimeInput);
            this.Controls.Add(this.restoreButtonX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RestoreDB";
            this.Text = "Restore Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RestoreDB_FormClosing);
            this.Load += new System.EventHandler(this.RestoreDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.restoreDBdateTimeInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX restoreButtonX;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput restoreDBdateTimeInput;
        private System.ComponentModel.BackgroundWorker restoreDBBackgroundWorker;
        private DevComponents.DotNetBar.Controls.CircularProgress restoreCircularProgress;
        private System.Windows.Forms.ListBox backupListBox;
    }
}