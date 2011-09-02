namespace TrinityCore_Manager
{
    partial class AddAcctBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAcctBan));
            this.accountComboBoxItem = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.accountLabelX = new DevComponents.DotNetBar.LabelX();
            this.addAcctBanButtonX = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // accountComboBoxItem
            // 
            this.accountComboBoxItem.DisplayMember = "Text";
            this.accountComboBoxItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.accountComboBoxItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountComboBoxItem.FormattingEnabled = true;
            this.accountComboBoxItem.ItemHeight = 14;
            this.accountComboBoxItem.Location = new System.Drawing.Point(151, 81);
            this.accountComboBoxItem.Name = "accountComboBoxItem";
            this.accountComboBoxItem.Size = new System.Drawing.Size(144, 20);
            this.accountComboBoxItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.accountComboBoxItem.TabIndex = 1;
            // 
            // accountLabelX
            // 
            // 
            // 
            // 
            this.accountLabelX.BackgroundStyle.Class = "";
            this.accountLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.accountLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountLabelX.Location = new System.Drawing.Point(84, 78);
            this.accountLabelX.Name = "accountLabelX";
            this.accountLabelX.Size = new System.Drawing.Size(61, 23);
            this.accountLabelX.TabIndex = 2;
            this.accountLabelX.Text = "Account";
            // 
            // addAcctBanButtonX
            // 
            this.addAcctBanButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addAcctBanButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addAcctBanButtonX.Location = new System.Drawing.Point(165, 167);
            this.addAcctBanButtonX.Name = "addAcctBanButtonX";
            this.addAcctBanButtonX.Size = new System.Drawing.Size(75, 23);
            this.addAcctBanButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.addAcctBanButtonX.TabIndex = 3;
            this.addAcctBanButtonX.Text = "Add Ban";
            this.addAcctBanButtonX.Click += new System.EventHandler(this.addAcctBanButtonX_Click);
            // 
            // AddAcctBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 202);
            this.Controls.Add(this.addAcctBanButtonX);
            this.Controls.Add(this.accountLabelX);
            this.Controls.Add(this.accountComboBoxItem);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAcctBan";
            this.Text = "Add Account Ban";
            this.Load += new System.EventHandler(this.AddAcctBan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx accountComboBoxItem;
        private DevComponents.DotNetBar.LabelX accountLabelX;
        private DevComponents.DotNetBar.ButtonX addAcctBanButtonX;
    }
}