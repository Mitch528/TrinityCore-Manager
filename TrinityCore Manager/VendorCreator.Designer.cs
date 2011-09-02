namespace TrinityCore_Manager
{
    partial class VendorCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VendorCreator));
            this.vendorEntryIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.vendorIntegerInput = new DevComponents.Editors.IntegerInput();
            this.itemEntryIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.itemEntryIntegerInput = new DevComponents.Editors.IntegerInput();
            this.addButtonX = new DevComponents.DotNetBar.ButtonX();
            this.removeButtonX = new DevComponents.DotNetBar.ButtonX();
            this.resetButtonX = new DevComponents.DotNetBar.ButtonX();
            this.vendorListViewEx = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.entryColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.exportSQLButtonX = new DevComponents.DotNetBar.ButtonX();
            this.saveToDBButtonX = new DevComponents.DotNetBar.ButtonX();
            this.vendorInfoBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.exportSQLFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.vendorIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemEntryIntegerInput)).BeginInit();
            this.SuspendLayout();
            // 
            // vendorEntryIDLabelX
            // 
            // 
            // 
            // 
            this.vendorEntryIDLabelX.BackgroundStyle.Class = "";
            this.vendorEntryIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.vendorEntryIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vendorEntryIDLabelX.Location = new System.Drawing.Point(186, 54);
            this.vendorEntryIDLabelX.Name = "vendorEntryIDLabelX";
            this.vendorEntryIDLabelX.Size = new System.Drawing.Size(108, 23);
            this.vendorEntryIDLabelX.TabIndex = 0;
            this.vendorEntryIDLabelX.Text = "Vendor Entry ID";
            // 
            // vendorIntegerInput
            // 
            this.vendorIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.vendorIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.vendorIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.vendorIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.vendorIntegerInput.Location = new System.Drawing.Point(300, 57);
            this.vendorIntegerInput.Name = "vendorIntegerInput";
            this.vendorIntegerInput.ShowUpDown = true;
            this.vendorIntegerInput.Size = new System.Drawing.Size(143, 20);
            this.vendorIntegerInput.TabIndex = 1;
            // 
            // itemEntryIDLabelX
            // 
            // 
            // 
            // 
            this.itemEntryIDLabelX.BackgroundStyle.Class = "";
            this.itemEntryIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemEntryIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemEntryIDLabelX.Location = new System.Drawing.Point(204, 117);
            this.itemEntryIDLabelX.Name = "itemEntryIDLabelX";
            this.itemEntryIDLabelX.Size = new System.Drawing.Size(90, 23);
            this.itemEntryIDLabelX.TabIndex = 2;
            this.itemEntryIDLabelX.Text = "Item Entry ID";
            // 
            // itemEntryIntegerInput
            // 
            this.itemEntryIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.itemEntryIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.itemEntryIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemEntryIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.itemEntryIntegerInput.Location = new System.Drawing.Point(300, 120);
            this.itemEntryIntegerInput.Name = "itemEntryIntegerInput";
            this.itemEntryIntegerInput.ShowUpDown = true;
            this.itemEntryIntegerInput.Size = new System.Drawing.Size(143, 20);
            this.itemEntryIntegerInput.TabIndex = 3;
            // 
            // addButtonX
            // 
            this.addButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addButtonX.Location = new System.Drawing.Point(186, 182);
            this.addButtonX.Name = "addButtonX";
            this.addButtonX.Size = new System.Drawing.Size(75, 23);
            this.addButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.addButtonX.TabIndex = 4;
            this.addButtonX.Text = "Add";
            this.addButtonX.Click += new System.EventHandler(this.addButtonX_Click);
            // 
            // removeButtonX
            // 
            this.removeButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.removeButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.removeButtonX.Location = new System.Drawing.Point(287, 182);
            this.removeButtonX.Name = "removeButtonX";
            this.removeButtonX.Size = new System.Drawing.Size(75, 23);
            this.removeButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.removeButtonX.TabIndex = 5;
            this.removeButtonX.Text = "Remove";
            this.removeButtonX.Click += new System.EventHandler(this.removeButtonX_Click);
            // 
            // resetButtonX
            // 
            this.resetButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.resetButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.resetButtonX.Location = new System.Drawing.Point(386, 182);
            this.resetButtonX.Name = "resetButtonX";
            this.resetButtonX.Size = new System.Drawing.Size(75, 23);
            this.resetButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.resetButtonX.TabIndex = 6;
            this.resetButtonX.Text = "Reset";
            this.resetButtonX.Click += new System.EventHandler(this.resetButtonX_Click);
            // 
            // vendorListViewEx
            // 
            // 
            // 
            // 
            this.vendorListViewEx.Border.Class = "ListViewBorder";
            this.vendorListViewEx.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.vendorListViewEx.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.entryColumnHeader,
            this.itemColumnHeader});
            this.vendorListViewEx.Location = new System.Drawing.Point(12, 224);
            this.vendorListViewEx.Name = "vendorListViewEx";
            this.vendorListViewEx.Size = new System.Drawing.Size(648, 201);
            this.vendorListViewEx.TabIndex = 7;
            this.vendorListViewEx.UseCompatibleStateImageBehavior = false;
            this.vendorListViewEx.View = System.Windows.Forms.View.Details;
            // 
            // entryColumnHeader
            // 
            this.entryColumnHeader.Text = "Vendor Entry ID";
            this.entryColumnHeader.Width = 323;
            // 
            // itemColumnHeader
            // 
            this.itemColumnHeader.Text = "Item Entry ID";
            this.itemColumnHeader.Width = 296;
            // 
            // exportSQLButtonX
            // 
            this.exportSQLButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.exportSQLButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.exportSQLButtonX.Location = new System.Drawing.Point(158, 447);
            this.exportSQLButtonX.Name = "exportSQLButtonX";
            this.exportSQLButtonX.Size = new System.Drawing.Size(136, 48);
            this.exportSQLButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.exportSQLButtonX.TabIndex = 8;
            this.exportSQLButtonX.Text = "Export as SQL File";
            this.exportSQLButtonX.Click += new System.EventHandler(this.exportSQLButtonX_Click);
            // 
            // saveToDBButtonX
            // 
            this.saveToDBButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveToDBButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.saveToDBButtonX.Location = new System.Drawing.Point(369, 447);
            this.saveToDBButtonX.Name = "saveToDBButtonX";
            this.saveToDBButtonX.Size = new System.Drawing.Size(136, 48);
            this.saveToDBButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.saveToDBButtonX.TabIndex = 9;
            this.saveToDBButtonX.Text = "Save To Database";
            this.saveToDBButtonX.Click += new System.EventHandler(this.saveToDBButtonX_Click);
            // 
            // vendorInfoBackgroundWorker
            // 
            this.vendorInfoBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.vendorInfoBackgroundWorker_DoWork);
            this.vendorInfoBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.vendorInfoBackgroundWorker_RunWorkerCompleted);
            // 
            // exportSQLFileDialog
            // 
            this.exportSQLFileDialog.Filter = "SQL File | *.sql";
            // 
            // VendorCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 507);
            this.Controls.Add(this.saveToDBButtonX);
            this.Controls.Add(this.exportSQLButtonX);
            this.Controls.Add(this.vendorListViewEx);
            this.Controls.Add(this.resetButtonX);
            this.Controls.Add(this.removeButtonX);
            this.Controls.Add(this.addButtonX);
            this.Controls.Add(this.itemEntryIntegerInput);
            this.Controls.Add(this.itemEntryIDLabelX);
            this.Controls.Add(this.vendorIntegerInput);
            this.Controls.Add(this.vendorEntryIDLabelX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VendorCreator";
            this.Text = "Vendor Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VendorCreator_FormClosing);
            this.Load += new System.EventHandler(this.VendorCreator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vendorIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemEntryIntegerInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX vendorEntryIDLabelX;
        private DevComponents.Editors.IntegerInput vendorIntegerInput;
        private DevComponents.Editors.IntegerInput itemEntryIntegerInput;
        private DevComponents.DotNetBar.LabelX itemEntryIDLabelX;
        private DevComponents.DotNetBar.ButtonX addButtonX;
        private DevComponents.DotNetBar.ButtonX removeButtonX;
        private DevComponents.DotNetBar.ButtonX resetButtonX;
        private DevComponents.DotNetBar.Controls.ListViewEx vendorListViewEx;
        private System.Windows.Forms.ColumnHeader entryColumnHeader;
        private System.Windows.Forms.ColumnHeader itemColumnHeader;
        private DevComponents.DotNetBar.ButtonX exportSQLButtonX;
        private DevComponents.DotNetBar.ButtonX saveToDBButtonX;
        private System.ComponentModel.BackgroundWorker vendorInfoBackgroundWorker;
        private System.Windows.Forms.SaveFileDialog exportSQLFileDialog;
    }
}