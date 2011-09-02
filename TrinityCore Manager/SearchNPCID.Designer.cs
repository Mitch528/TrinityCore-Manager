namespace TrinityCore_Manager
{
    partial class SearchNPCID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchNPCID));
            this.npcNameLabelX = new DevComponents.DotNetBar.LabelX();
            this.npcSearchTextBoxX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.npcSearchButtonX = new DevComponents.DotNetBar.ButtonX();
            this.entryIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.displayIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.submitButtonX = new DevComponents.DotNetBar.ButtonX();
            this.npcSearchListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // npcNameLabelX
            // 
            // 
            // 
            // 
            this.npcNameLabelX.BackgroundStyle.Class = "";
            this.npcNameLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.npcNameLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcNameLabelX.Location = new System.Drawing.Point(171, 41);
            this.npcNameLabelX.Name = "npcNameLabelX";
            this.npcNameLabelX.Size = new System.Drawing.Size(79, 23);
            this.npcNameLabelX.TabIndex = 0;
            this.npcNameLabelX.Text = "NPC Name";
            // 
            // npcSearchTextBoxX
            // 
            // 
            // 
            // 
            this.npcSearchTextBoxX.Border.Class = "TextBoxBorder";
            this.npcSearchTextBoxX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.npcSearchTextBoxX.Location = new System.Drawing.Point(257, 43);
            this.npcSearchTextBoxX.Name = "npcSearchTextBoxX";
            this.npcSearchTextBoxX.Size = new System.Drawing.Size(259, 20);
            this.npcSearchTextBoxX.TabIndex = 1;
            // 
            // npcSearchButtonX
            // 
            this.npcSearchButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.npcSearchButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.npcSearchButtonX.Location = new System.Drawing.Point(522, 43);
            this.npcSearchButtonX.Name = "npcSearchButtonX";
            this.npcSearchButtonX.Size = new System.Drawing.Size(75, 23);
            this.npcSearchButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.npcSearchButtonX.TabIndex = 2;
            this.npcSearchButtonX.Text = "Search";
            this.npcSearchButtonX.Click += new System.EventHandler(this.npcSearchButtonX_Click);
            // 
            // entryIDLabelX
            // 
            // 
            // 
            // 
            this.entryIDLabelX.BackgroundStyle.Class = "";
            this.entryIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.entryIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryIDLabelX.Location = new System.Drawing.Point(507, 351);
            this.entryIDLabelX.Name = "entryIDLabelX";
            this.entryIDLabelX.Size = new System.Drawing.Size(109, 23);
            this.entryIDLabelX.TabIndex = 4;
            this.entryIDLabelX.Text = "Entry ID:";
            // 
            // displayIDLabelX
            // 
            // 
            // 
            // 
            this.displayIDLabelX.BackgroundStyle.Class = "";
            this.displayIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.displayIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayIDLabelX.Location = new System.Drawing.Point(632, 351);
            this.displayIDLabelX.Name = "displayIDLabelX";
            this.displayIDLabelX.Size = new System.Drawing.Size(133, 23);
            this.displayIDLabelX.TabIndex = 5;
            this.displayIDLabelX.Text = "Display ID:";
            // 
            // submitButtonX
            // 
            this.submitButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.submitButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.submitButtonX.Location = new System.Drawing.Point(329, 338);
            this.submitButtonX.Name = "submitButtonX";
            this.submitButtonX.Size = new System.Drawing.Size(122, 41);
            this.submitButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.submitButtonX.TabIndex = 6;
            this.submitButtonX.Text = "Submit";
            this.submitButtonX.Click += new System.EventHandler(this.submitButtonX_Click);
            // 
            // npcSearchListBox
            // 
            this.npcSearchListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcSearchListBox.FormattingEnabled = true;
            this.npcSearchListBox.ItemHeight = 18;
            this.npcSearchListBox.Location = new System.Drawing.Point(12, 75);
            this.npcSearchListBox.Name = "npcSearchListBox";
            this.npcSearchListBox.Size = new System.Drawing.Size(753, 238);
            this.npcSearchListBox.TabIndex = 7;
            // 
            // SearchNPCID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 386);
            this.Controls.Add(this.npcSearchListBox);
            this.Controls.Add(this.submitButtonX);
            this.Controls.Add(this.displayIDLabelX);
            this.Controls.Add(this.entryIDLabelX);
            this.Controls.Add(this.npcSearchButtonX);
            this.Controls.Add(this.npcSearchTextBoxX);
            this.Controls.Add(this.npcNameLabelX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchNPCID";
            this.Text = "Search For NPC ID";
            this.Load += new System.EventHandler(this.SearchNPCID_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX npcNameLabelX;
        private DevComponents.DotNetBar.Controls.TextBoxX npcSearchTextBoxX;
        private DevComponents.DotNetBar.ButtonX npcSearchButtonX;
        private DevComponents.DotNetBar.LabelX entryIDLabelX;
        private DevComponents.DotNetBar.LabelX displayIDLabelX;
        private DevComponents.DotNetBar.ButtonX submitButtonX;
        private System.Windows.Forms.ListBox npcSearchListBox;
    }
}