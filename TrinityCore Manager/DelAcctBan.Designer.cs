namespace TrinityCore_Manager
{
    partial class DelAcctBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelAcctBan));
            this.delAcctBanButtonX = new DevComponents.DotNetBar.ButtonX();
            this.accountLabelX = new DevComponents.DotNetBar.LabelX();
            this.accountComboBoxItem = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // delAcctBanButtonX
            // 
            this.delAcctBanButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.delAcctBanButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.delAcctBanButtonX.Location = new System.Drawing.Point(166, 167);
            this.delAcctBanButtonX.Name = "delAcctBanButtonX";
            this.delAcctBanButtonX.Size = new System.Drawing.Size(75, 23);
            this.delAcctBanButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.delAcctBanButtonX.TabIndex = 6;
            this.delAcctBanButtonX.Text = "Delete Ban";
            this.delAcctBanButtonX.Click += new System.EventHandler(this.delAcctBanButtonX_Click);
            // 
            // accountLabelX
            // 
            // 
            // 
            // 
            this.accountLabelX.BackgroundStyle.Class = "";
            this.accountLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.accountLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountLabelX.Location = new System.Drawing.Point(85, 78);
            this.accountLabelX.Name = "accountLabelX";
            this.accountLabelX.Size = new System.Drawing.Size(61, 23);
            this.accountLabelX.TabIndex = 5;
            this.accountLabelX.Text = "Account";
            // 
            // accountComboBoxItem
            // 
            this.accountComboBoxItem.DisplayMember = "Text";
            this.accountComboBoxItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.accountComboBoxItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountComboBoxItem.FormattingEnabled = true;
            this.accountComboBoxItem.ItemHeight = 14;
            this.accountComboBoxItem.Location = new System.Drawing.Point(152, 81);
            this.accountComboBoxItem.Name = "accountComboBoxItem";
            this.accountComboBoxItem.Size = new System.Drawing.Size(144, 20);
            this.accountComboBoxItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.accountComboBoxItem.TabIndex = 4;
            // 
            // DelAcctBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 202);
            this.Controls.Add(this.delAcctBanButtonX);
            this.Controls.Add(this.accountLabelX);
            this.Controls.Add(this.accountComboBoxItem);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DelAcctBan";
            this.Text = "Delete Account Ban";
            this.Load += new System.EventHandler(this.DelAcctBan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX delAcctBanButtonX;
        private DevComponents.DotNetBar.LabelX accountLabelX;
        private DevComponents.DotNetBar.Controls.ComboBoxEx accountComboBoxItem;
    }
}