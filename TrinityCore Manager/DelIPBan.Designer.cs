namespace TrinityCore_Manager
{
    partial class DelIPBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelIPBan));
            this.ipAddressComboBoxItemEX = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.deleteIPBanButtonX = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // ipAddressComboBoxItemEX
            // 
            this.ipAddressComboBoxItemEX.DisplayMember = "Text";
            this.ipAddressComboBoxItemEX.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ipAddressComboBoxItemEX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ipAddressComboBoxItemEX.FormattingEnabled = true;
            this.ipAddressComboBoxItemEX.ItemHeight = 14;
            this.ipAddressComboBoxItemEX.Location = new System.Drawing.Point(202, 65);
            this.ipAddressComboBoxItemEX.Name = "ipAddressComboBoxItemEX";
            this.ipAddressComboBoxItemEX.Size = new System.Drawing.Size(144, 20);
            this.ipAddressComboBoxItemEX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ipAddressComboBoxItemEX.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(121, 62);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "IP Address";
            // 
            // deleteIPBanButtonX
            // 
            this.deleteIPBanButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.deleteIPBanButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.deleteIPBanButtonX.Location = new System.Drawing.Point(202, 134);
            this.deleteIPBanButtonX.Name = "deleteIPBanButtonX";
            this.deleteIPBanButtonX.Size = new System.Drawing.Size(75, 23);
            this.deleteIPBanButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.deleteIPBanButtonX.TabIndex = 2;
            this.deleteIPBanButtonX.Text = "Delete";
            this.deleteIPBanButtonX.Click += new System.EventHandler(this.deleteIPBanButtonX_Click);
            // 
            // DelIPBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 169);
            this.Controls.Add(this.deleteIPBanButtonX);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.ipAddressComboBoxItemEX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DelIPBan";
            this.Text = "Delete IP Ban";
            this.Load += new System.EventHandler(this.DelIPBan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx ipAddressComboBoxItemEX;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX deleteIPBanButtonX;
    }
}