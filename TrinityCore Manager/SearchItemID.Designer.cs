namespace TrinityCore_Manager
{
    partial class SearchItemID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchItemID));
            this.submitButtonX = new DevComponents.DotNetBar.ButtonX();
            this.itemNameLabelX = new DevComponents.DotNetBar.LabelX();
            this.itemNameTextBoxX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.itemsListBox = new System.Windows.Forms.ListBox();
            this.searchButtonX = new DevComponents.DotNetBar.ButtonX();
            this.itemEntryLabelX = new DevComponents.DotNetBar.LabelX();
            this.itemDisplayIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.SuspendLayout();
            // 
            // submitButtonX
            // 
            this.submitButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.submitButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.submitButtonX.Location = new System.Drawing.Point(298, 384);
            this.submitButtonX.Name = "submitButtonX";
            this.submitButtonX.Size = new System.Drawing.Size(113, 42);
            this.submitButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.submitButtonX.TabIndex = 0;
            this.submitButtonX.Text = "Submit";
            this.submitButtonX.Click += new System.EventHandler(this.submitButtonX_Click);
            // 
            // itemNameLabelX
            // 
            // 
            // 
            // 
            this.itemNameLabelX.BackgroundStyle.Class = "";
            this.itemNameLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemNameLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemNameLabelX.Location = new System.Drawing.Point(143, 47);
            this.itemNameLabelX.Name = "itemNameLabelX";
            this.itemNameLabelX.Size = new System.Drawing.Size(75, 23);
            this.itemNameLabelX.TabIndex = 1;
            this.itemNameLabelX.Text = "Item Name";
            // 
            // itemNameTextBoxX
            // 
            // 
            // 
            // 
            this.itemNameTextBoxX.Border.Class = "TextBoxBorder";
            this.itemNameTextBoxX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemNameTextBoxX.Location = new System.Drawing.Point(225, 49);
            this.itemNameTextBoxX.Name = "itemNameTextBoxX";
            this.itemNameTextBoxX.Size = new System.Drawing.Size(278, 20);
            this.itemNameTextBoxX.TabIndex = 2;
            // 
            // itemsListBox
            // 
            this.itemsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemsListBox.FormattingEnabled = true;
            this.itemsListBox.ItemHeight = 18;
            this.itemsListBox.Location = new System.Drawing.Point(12, 90);
            this.itemsListBox.Name = "itemsListBox";
            this.itemsListBox.Size = new System.Drawing.Size(700, 274);
            this.itemsListBox.TabIndex = 3;
            // 
            // searchButtonX
            // 
            this.searchButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.searchButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.searchButtonX.Location = new System.Drawing.Point(509, 47);
            this.searchButtonX.Name = "searchButtonX";
            this.searchButtonX.Size = new System.Drawing.Size(75, 23);
            this.searchButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.searchButtonX.TabIndex = 18;
            this.searchButtonX.Text = "Search";
            this.searchButtonX.Click += new System.EventHandler(this.searchButtonX_Click);
            // 
            // itemEntryLabelX
            // 
            // 
            // 
            // 
            this.itemEntryLabelX.BackgroundStyle.Class = "";
            this.itemEntryLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemEntryLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemEntryLabelX.Location = new System.Drawing.Point(474, 403);
            this.itemEntryLabelX.Name = "itemEntryLabelX";
            this.itemEntryLabelX.Size = new System.Drawing.Size(101, 23);
            this.itemEntryLabelX.TabIndex = 19;
            this.itemEntryLabelX.Text = "Entry:";
            // 
            // itemDisplayIDLabelX
            // 
            // 
            // 
            // 
            this.itemDisplayIDLabelX.BackgroundStyle.Class = "";
            this.itemDisplayIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemDisplayIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemDisplayIDLabelX.Location = new System.Drawing.Point(581, 403);
            this.itemDisplayIDLabelX.Name = "itemDisplayIDLabelX";
            this.itemDisplayIDLabelX.Size = new System.Drawing.Size(131, 23);
            this.itemDisplayIDLabelX.TabIndex = 20;
            this.itemDisplayIDLabelX.Text = "Display ID:";
            // 
            // circularProgress
            // 
            // 
            // 
            // 
            this.circularProgress.BackgroundStyle.Class = "";
            this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress.Location = new System.Drawing.Point(336, 12);
            this.circularProgress.Name = "circularProgress";
            this.circularProgress.Size = new System.Drawing.Size(75, 23);
            this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress.TabIndex = 21;
            // 
            // SearchItemID
            // 
            this.AcceptButton = this.submitButtonX;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 438);
            this.Controls.Add(this.circularProgress);
            this.Controls.Add(this.itemDisplayIDLabelX);
            this.Controls.Add(this.itemEntryLabelX);
            this.Controls.Add(this.searchButtonX);
            this.Controls.Add(this.itemsListBox);
            this.Controls.Add(this.itemNameTextBoxX);
            this.Controls.Add(this.itemNameLabelX);
            this.Controls.Add(this.submitButtonX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SearchItemID";
            this.Text = "Search For Item IDs";
            this.Load += new System.EventHandler(this.SearchDisplayID_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX submitButtonX;
        private DevComponents.DotNetBar.LabelX itemNameLabelX;
        private DevComponents.DotNetBar.Controls.TextBoxX itemNameTextBoxX;
        private System.Windows.Forms.ListBox itemsListBox;
        private DevComponents.DotNetBar.ButtonX searchButtonX;
        private DevComponents.DotNetBar.LabelX itemEntryLabelX;
        private DevComponents.DotNetBar.LabelX itemDisplayIDLabelX;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
    }
}