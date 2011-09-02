namespace TrinityCore_Manager
{
    partial class LootCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LootCreator));
            this.lootListViewEx = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.npcIDColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemIDColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chanceColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.minColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.maxColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.npcEntryIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.npcEntryIDIntegerInput = new DevComponents.Editors.IntegerInput();
            this.itemEntryIDIntegerInput = new DevComponents.Editors.IntegerInput();
            this.itemEntryIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.minAmntIntegerInput = new DevComponents.Editors.IntegerInput();
            this.minAmntLabelX = new DevComponents.DotNetBar.LabelX();
            this.chanceIntegerInput = new DevComponents.Editors.IntegerInput();
            this.chanceLabelX = new DevComponents.DotNetBar.LabelX();
            this.percentLabelX = new DevComponents.DotNetBar.LabelX();
            this.maxAmntIntegerInput = new DevComponents.Editors.IntegerInput();
            this.maxAmntLabelX = new DevComponents.DotNetBar.LabelX();
            this.searchNPCEntryIDButtonX = new DevComponents.DotNetBar.ButtonX();
            this.searchItemIDButtonX = new DevComponents.DotNetBar.ButtonX();
            this.lootInfoBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.addButtonX = new DevComponents.DotNetBar.ButtonX();
            this.removeButtonX = new DevComponents.DotNetBar.ButtonX();
            this.resetButtonX = new DevComponents.DotNetBar.ButtonX();
            this.saveToDBButtonX = new DevComponents.DotNetBar.ButtonX();
            this.exportSQLButtonX = new DevComponents.DotNetBar.ButtonX();
            this.exportSQLFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.npcEntryIDIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemEntryIDIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minAmntIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chanceIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxAmntIntegerInput)).BeginInit();
            this.SuspendLayout();
            // 
            // lootListViewEx
            // 
            // 
            // 
            // 
            this.lootListViewEx.Border.Class = "ListViewBorder";
            this.lootListViewEx.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lootListViewEx.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.npcIDColumnHeader,
            this.itemIDColumnHeader,
            this.chanceColumnHeader,
            this.minColumnHeader,
            this.maxColumnHeader});
            this.lootListViewEx.Location = new System.Drawing.Point(12, 224);
            this.lootListViewEx.Name = "lootListViewEx";
            this.lootListViewEx.Size = new System.Drawing.Size(801, 227);
            this.lootListViewEx.TabIndex = 0;
            this.lootListViewEx.UseCompatibleStateImageBehavior = false;
            this.lootListViewEx.View = System.Windows.Forms.View.Details;
            // 
            // npcIDColumnHeader
            // 
            this.npcIDColumnHeader.Text = "NPC Entry ID";
            this.npcIDColumnHeader.Width = 229;
            // 
            // itemIDColumnHeader
            // 
            this.itemIDColumnHeader.Text = "Item Entry ID";
            this.itemIDColumnHeader.Width = 191;
            // 
            // chanceColumnHeader
            // 
            this.chanceColumnHeader.Text = "Chance (%)";
            this.chanceColumnHeader.Width = 76;
            // 
            // minColumnHeader
            // 
            this.minColumnHeader.Text = "Min";
            this.minColumnHeader.Width = 101;
            // 
            // maxColumnHeader
            // 
            this.maxColumnHeader.Text = "Max";
            this.maxColumnHeader.Width = 103;
            // 
            // npcEntryIDLabelX
            // 
            // 
            // 
            // 
            this.npcEntryIDLabelX.BackgroundStyle.Class = "";
            this.npcEntryIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.npcEntryIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcEntryIDLabelX.Location = new System.Drawing.Point(97, 82);
            this.npcEntryIDLabelX.Name = "npcEntryIDLabelX";
            this.npcEntryIDLabelX.Size = new System.Drawing.Size(93, 23);
            this.npcEntryIDLabelX.TabIndex = 1;
            this.npcEntryIDLabelX.Text = "NPC Entry ID";
            // 
            // npcEntryIDIntegerInput
            // 
            this.npcEntryIDIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.npcEntryIDIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.npcEntryIDIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.npcEntryIDIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.npcEntryIDIntegerInput.Location = new System.Drawing.Point(197, 84);
            this.npcEntryIDIntegerInput.MinValue = 0;
            this.npcEntryIDIntegerInput.Name = "npcEntryIDIntegerInput";
            this.npcEntryIDIntegerInput.ShowUpDown = true;
            this.npcEntryIDIntegerInput.Size = new System.Drawing.Size(80, 20);
            this.npcEntryIDIntegerInput.TabIndex = 2;
            // 
            // itemEntryIDIntegerInput
            // 
            this.itemEntryIDIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.itemEntryIDIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.itemEntryIDIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemEntryIDIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.itemEntryIDIntegerInput.Location = new System.Drawing.Point(196, 136);
            this.itemEntryIDIntegerInput.MinValue = 0;
            this.itemEntryIDIntegerInput.Name = "itemEntryIDIntegerInput";
            this.itemEntryIDIntegerInput.ShowUpDown = true;
            this.itemEntryIDIntegerInput.Size = new System.Drawing.Size(80, 20);
            this.itemEntryIDIntegerInput.TabIndex = 4;
            // 
            // itemEntryIDLabelX
            // 
            // 
            // 
            // 
            this.itemEntryIDLabelX.BackgroundStyle.Class = "";
            this.itemEntryIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemEntryIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemEntryIDLabelX.Location = new System.Drawing.Point(97, 133);
            this.itemEntryIDLabelX.Name = "itemEntryIDLabelX";
            this.itemEntryIDLabelX.Size = new System.Drawing.Size(93, 23);
            this.itemEntryIDLabelX.TabIndex = 3;
            this.itemEntryIDLabelX.Text = "Item Entry ID";
            // 
            // minAmntIntegerInput
            // 
            this.minAmntIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.minAmntIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.minAmntIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.minAmntIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.minAmntIntegerInput.Location = new System.Drawing.Point(418, 85);
            this.minAmntIntegerInput.MinValue = 0;
            this.minAmntIntegerInput.Name = "minAmntIntegerInput";
            this.minAmntIntegerInput.ShowUpDown = true;
            this.minAmntIntegerInput.Size = new System.Drawing.Size(80, 20);
            this.minAmntIntegerInput.TabIndex = 6;
            // 
            // minAmntLabelX
            // 
            // 
            // 
            // 
            this.minAmntLabelX.BackgroundStyle.Class = "";
            this.minAmntLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.minAmntLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minAmntLabelX.Location = new System.Drawing.Point(331, 82);
            this.minAmntLabelX.Name = "minAmntLabelX";
            this.minAmntLabelX.Size = new System.Drawing.Size(81, 23);
            this.minAmntLabelX.TabIndex = 5;
            this.minAmntLabelX.Text = "Min Amount";
            // 
            // chanceIntegerInput
            // 
            this.chanceIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.chanceIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.chanceIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chanceIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.chanceIntegerInput.Location = new System.Drawing.Point(418, 136);
            this.chanceIntegerInput.MinValue = 0;
            this.chanceIntegerInput.Name = "chanceIntegerInput";
            this.chanceIntegerInput.ShowUpDown = true;
            this.chanceIntegerInput.Size = new System.Drawing.Size(80, 20);
            this.chanceIntegerInput.TabIndex = 8;
            // 
            // chanceLabelX
            // 
            // 
            // 
            // 
            this.chanceLabelX.BackgroundStyle.Class = "";
            this.chanceLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chanceLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chanceLabelX.Location = new System.Drawing.Point(359, 133);
            this.chanceLabelX.Name = "chanceLabelX";
            this.chanceLabelX.Size = new System.Drawing.Size(53, 23);
            this.chanceLabelX.TabIndex = 7;
            this.chanceLabelX.Text = "Chance";
            // 
            // percentLabelX
            // 
            // 
            // 
            // 
            this.percentLabelX.BackgroundStyle.Class = "";
            this.percentLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.percentLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentLabelX.Location = new System.Drawing.Point(504, 133);
            this.percentLabelX.Name = "percentLabelX";
            this.percentLabelX.Size = new System.Drawing.Size(16, 23);
            this.percentLabelX.TabIndex = 9;
            this.percentLabelX.Text = "%";
            // 
            // maxAmntIntegerInput
            // 
            this.maxAmntIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.maxAmntIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.maxAmntIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.maxAmntIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.maxAmntIntegerInput.Location = new System.Drawing.Point(642, 113);
            this.maxAmntIntegerInput.MinValue = 0;
            this.maxAmntIntegerInput.Name = "maxAmntIntegerInput";
            this.maxAmntIntegerInput.ShowUpDown = true;
            this.maxAmntIntegerInput.Size = new System.Drawing.Size(80, 20);
            this.maxAmntIntegerInput.TabIndex = 11;
            // 
            // maxAmntLabelX
            // 
            // 
            // 
            // 
            this.maxAmntLabelX.BackgroundStyle.Class = "";
            this.maxAmntLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.maxAmntLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxAmntLabelX.Location = new System.Drawing.Point(541, 110);
            this.maxAmntLabelX.Name = "maxAmntLabelX";
            this.maxAmntLabelX.Size = new System.Drawing.Size(95, 23);
            this.maxAmntLabelX.TabIndex = 10;
            this.maxAmntLabelX.Text = "Max Amount";
            // 
            // searchNPCEntryIDButtonX
            // 
            this.searchNPCEntryIDButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.searchNPCEntryIDButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.searchNPCEntryIDButtonX.Location = new System.Drawing.Point(257, 12);
            this.searchNPCEntryIDButtonX.Name = "searchNPCEntryIDButtonX";
            this.searchNPCEntryIDButtonX.Size = new System.Drawing.Size(136, 44);
            this.searchNPCEntryIDButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.searchNPCEntryIDButtonX.TabIndex = 12;
            this.searchNPCEntryIDButtonX.Text = "Search For NPC Entry ID";
            this.searchNPCEntryIDButtonX.Click += new System.EventHandler(this.searchNPCEntryIDButtonX_Click);
            // 
            // searchItemIDButtonX
            // 
            this.searchItemIDButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.searchItemIDButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.searchItemIDButtonX.Location = new System.Drawing.Point(399, 12);
            this.searchItemIDButtonX.Name = "searchItemIDButtonX";
            this.searchItemIDButtonX.Size = new System.Drawing.Size(136, 44);
            this.searchItemIDButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.searchItemIDButtonX.TabIndex = 13;
            this.searchItemIDButtonX.Text = "Search For Item Entry ID";
            this.searchItemIDButtonX.Click += new System.EventHandler(this.searchItemIDButtonX_Click);
            // 
            // lootInfoBackgroundWorker
            // 
            this.lootInfoBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.lootInfoBackgroundWorker_DoWork);
            this.lootInfoBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.lootInfoBackgroundWorker_RunWorkerCompleted);
            // 
            // addButtonX
            // 
            this.addButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addButtonX.Location = new System.Drawing.Point(295, 195);
            this.addButtonX.Name = "addButtonX";
            this.addButtonX.Size = new System.Drawing.Size(75, 23);
            this.addButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.addButtonX.TabIndex = 14;
            this.addButtonX.Text = "Add";
            this.addButtonX.Click += new System.EventHandler(this.addButtonX_Click);
            // 
            // removeButtonX
            // 
            this.removeButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.removeButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.removeButtonX.Location = new System.Drawing.Point(376, 195);
            this.removeButtonX.Name = "removeButtonX";
            this.removeButtonX.Size = new System.Drawing.Size(75, 23);
            this.removeButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.removeButtonX.TabIndex = 15;
            this.removeButtonX.Text = "Remove";
            this.removeButtonX.Click += new System.EventHandler(this.removeButtonX_Click);
            // 
            // resetButtonX
            // 
            this.resetButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.resetButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.resetButtonX.Location = new System.Drawing.Point(457, 195);
            this.resetButtonX.Name = "resetButtonX";
            this.resetButtonX.Size = new System.Drawing.Size(75, 23);
            this.resetButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.resetButtonX.TabIndex = 16;
            this.resetButtonX.Text = "Reset";
            this.resetButtonX.Click += new System.EventHandler(this.resetButtonX_Click);
            // 
            // saveToDBButtonX
            // 
            this.saveToDBButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveToDBButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.saveToDBButtonX.Location = new System.Drawing.Point(500, 460);
            this.saveToDBButtonX.Name = "saveToDBButtonX";
            this.saveToDBButtonX.Size = new System.Drawing.Size(136, 48);
            this.saveToDBButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.saveToDBButtonX.TabIndex = 17;
            this.saveToDBButtonX.Text = "Save To Database";
            this.saveToDBButtonX.Click += new System.EventHandler(this.saveToDBButtonX_Click);
            // 
            // exportSQLButtonX
            // 
            this.exportSQLButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.exportSQLButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.exportSQLButtonX.Location = new System.Drawing.Point(197, 460);
            this.exportSQLButtonX.Name = "exportSQLButtonX";
            this.exportSQLButtonX.Size = new System.Drawing.Size(136, 48);
            this.exportSQLButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.exportSQLButtonX.TabIndex = 18;
            this.exportSQLButtonX.Text = "Export As SQL File";
            this.exportSQLButtonX.Click += new System.EventHandler(this.exportSQLButtonX_Click);
            // 
            // exportSQLFileDialog
            // 
            this.exportSQLFileDialog.Filter = "SQL File | *.sql";
            // 
            // LootCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 520);
            this.Controls.Add(this.exportSQLButtonX);
            this.Controls.Add(this.saveToDBButtonX);
            this.Controls.Add(this.resetButtonX);
            this.Controls.Add(this.removeButtonX);
            this.Controls.Add(this.addButtonX);
            this.Controls.Add(this.searchItemIDButtonX);
            this.Controls.Add(this.searchNPCEntryIDButtonX);
            this.Controls.Add(this.maxAmntIntegerInput);
            this.Controls.Add(this.maxAmntLabelX);
            this.Controls.Add(this.percentLabelX);
            this.Controls.Add(this.chanceIntegerInput);
            this.Controls.Add(this.chanceLabelX);
            this.Controls.Add(this.minAmntIntegerInput);
            this.Controls.Add(this.minAmntLabelX);
            this.Controls.Add(this.itemEntryIDIntegerInput);
            this.Controls.Add(this.itemEntryIDLabelX);
            this.Controls.Add(this.npcEntryIDIntegerInput);
            this.Controls.Add(this.npcEntryIDLabelX);
            this.Controls.Add(this.lootListViewEx);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LootCreator";
            this.Text = "Loot Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LootCreator_FormClosing);
            this.Load += new System.EventHandler(this.LootCreator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.npcEntryIDIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemEntryIDIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minAmntIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chanceIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxAmntIntegerInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lootListViewEx;
        private DevComponents.DotNetBar.LabelX npcEntryIDLabelX;
        private DevComponents.Editors.IntegerInput npcEntryIDIntegerInput;
        private DevComponents.Editors.IntegerInput itemEntryIDIntegerInput;
        private DevComponents.DotNetBar.LabelX itemEntryIDLabelX;
        private DevComponents.Editors.IntegerInput minAmntIntegerInput;
        private DevComponents.DotNetBar.LabelX minAmntLabelX;
        private DevComponents.Editors.IntegerInput chanceIntegerInput;
        private DevComponents.DotNetBar.LabelX chanceLabelX;
        private DevComponents.DotNetBar.LabelX percentLabelX;
        private DevComponents.DotNetBar.LabelX maxAmntLabelX;
        private DevComponents.Editors.IntegerInput maxAmntIntegerInput;
        private DevComponents.DotNetBar.ButtonX searchNPCEntryIDButtonX;
        private DevComponents.DotNetBar.ButtonX searchItemIDButtonX;
        private System.ComponentModel.BackgroundWorker lootInfoBackgroundWorker;
        private DevComponents.DotNetBar.ButtonX addButtonX;
        private DevComponents.DotNetBar.ButtonX removeButtonX;
        private DevComponents.DotNetBar.ButtonX resetButtonX;
        private System.Windows.Forms.ColumnHeader npcIDColumnHeader;
        private System.Windows.Forms.ColumnHeader itemIDColumnHeader;
        private System.Windows.Forms.ColumnHeader chanceColumnHeader;
        private System.Windows.Forms.ColumnHeader minColumnHeader;
        private System.Windows.Forms.ColumnHeader maxColumnHeader;
        private DevComponents.DotNetBar.ButtonX saveToDBButtonX;
        private DevComponents.DotNetBar.ButtonX exportSQLButtonX;
        private System.Windows.Forms.SaveFileDialog exportSQLFileDialog;

    }
}