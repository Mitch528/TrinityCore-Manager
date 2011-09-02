namespace TrinityCore_Manager
{
    partial class AddIPBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddIPBan));
            this.ipAddressLabelX = new DevComponents.DotNetBar.LabelX();
            this.submitBanButtonX = new DevComponents.DotNetBar.ButtonX();
            this.ipAddressInput = new DevComponents.Editors.IpAddressInput();
            ((System.ComponentModel.ISupportInitialize)(this.ipAddressInput)).BeginInit();
            this.SuspendLayout();
            // 
            // ipAddressLabelX
            // 
            // 
            // 
            // 
            this.ipAddressLabelX.BackgroundStyle.Class = "";
            this.ipAddressLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ipAddressLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressLabelX.Location = new System.Drawing.Point(90, 43);
            this.ipAddressLabelX.Name = "ipAddressLabelX";
            this.ipAddressLabelX.Size = new System.Drawing.Size(79, 23);
            this.ipAddressLabelX.TabIndex = 0;
            this.ipAddressLabelX.Text = "IP Address";
            // 
            // submitBanButtonX
            // 
            this.submitBanButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.submitBanButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.submitBanButtonX.Location = new System.Drawing.Point(162, 107);
            this.submitBanButtonX.Name = "submitBanButtonX";
            this.submitBanButtonX.Size = new System.Drawing.Size(75, 23);
            this.submitBanButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.submitBanButtonX.TabIndex = 2;
            this.submitBanButtonX.Text = "Submit";
            this.submitBanButtonX.Click += new System.EventHandler(this.submitBanButtonX_Click);
            // 
            // ipAddressInput
            // 
            this.ipAddressInput.AutoOverwrite = true;
            // 
            // 
            // 
            this.ipAddressInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ipAddressInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ipAddressInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ipAddressInput.ButtonFreeText.Visible = true;
            this.ipAddressInput.Location = new System.Drawing.Point(172, 45);
            this.ipAddressInput.Name = "ipAddressInput";
            this.ipAddressInput.Size = new System.Drawing.Size(123, 20);
            this.ipAddressInput.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ipAddressInput.TabIndex = 3;
            // 
            // AddIPBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 142);
            this.Controls.Add(this.ipAddressInput);
            this.Controls.Add(this.submitBanButtonX);
            this.Controls.Add(this.ipAddressLabelX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddIPBan";
            this.Text = "Add IP Ban";
            ((System.ComponentModel.ISupportInitialize)(this.ipAddressInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX ipAddressLabelX;
        private DevComponents.DotNetBar.ButtonX submitBanButtonX;
        private DevComponents.Editors.IpAddressInput ipAddressInput;
    }
}