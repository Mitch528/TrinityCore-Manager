namespace TrinityCore_Manager
{
    partial class AddAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAccount));
            this.accountNameLabelX = new DevComponents.DotNetBar.LabelX();
            this.accountNameTextBoxX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.passwordTextBoxX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.passwordLabelX = new DevComponents.DotNetBar.LabelX();
            this.gmLevelLabelX = new DevComponents.DotNetBar.LabelX();
            this.GMLevelIntegerInput = new DevComponents.Editors.IntegerInput();
            this.okButtonX = new DevComponents.DotNetBar.ButtonX();
            this.cButtonX = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.GMLevelIntegerInput)).BeginInit();
            this.SuspendLayout();
            // 
            // accountNameLabelX
            // 
            // 
            // 
            // 
            this.accountNameLabelX.BackgroundStyle.Class = "";
            this.accountNameLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.accountNameLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountNameLabelX.Location = new System.Drawing.Point(73, 54);
            this.accountNameLabelX.Name = "accountNameLabelX";
            this.accountNameLabelX.Size = new System.Drawing.Size(102, 23);
            this.accountNameLabelX.TabIndex = 0;
            this.accountNameLabelX.Text = "Name of Account";
            // 
            // accountNameTextBoxX
            // 
            // 
            // 
            // 
            this.accountNameTextBoxX.Border.Class = "TextBoxBorder";
            this.accountNameTextBoxX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.accountNameTextBoxX.Location = new System.Drawing.Point(181, 57);
            this.accountNameTextBoxX.Name = "accountNameTextBoxX";
            this.accountNameTextBoxX.Size = new System.Drawing.Size(164, 20);
            this.accountNameTextBoxX.TabIndex = 1;
            // 
            // passwordTextBoxX
            // 
            // 
            // 
            // 
            this.passwordTextBoxX.Border.Class = "TextBoxBorder";
            this.passwordTextBoxX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.passwordTextBoxX.Location = new System.Drawing.Point(181, 104);
            this.passwordTextBoxX.Name = "passwordTextBoxX";
            this.passwordTextBoxX.Size = new System.Drawing.Size(164, 20);
            this.passwordTextBoxX.TabIndex = 3;
            this.passwordTextBoxX.UseSystemPasswordChar = true;
            // 
            // passwordLabelX
            // 
            // 
            // 
            // 
            this.passwordLabelX.BackgroundStyle.Class = "";
            this.passwordLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.passwordLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabelX.Location = new System.Drawing.Point(112, 101);
            this.passwordLabelX.Name = "passwordLabelX";
            this.passwordLabelX.Size = new System.Drawing.Size(63, 23);
            this.passwordLabelX.TabIndex = 2;
            this.passwordLabelX.Text = "Password";
            // 
            // gmLevelLabelX
            // 
            // 
            // 
            // 
            this.gmLevelLabelX.BackgroundStyle.Class = "";
            this.gmLevelLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gmLevelLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gmLevelLabelX.Location = new System.Drawing.Point(115, 144);
            this.gmLevelLabelX.Name = "gmLevelLabelX";
            this.gmLevelLabelX.Size = new System.Drawing.Size(60, 23);
            this.gmLevelLabelX.TabIndex = 4;
            this.gmLevelLabelX.Text = "GM Level";
            // 
            // GMLevelIntegerInput
            // 
            // 
            // 
            // 
            this.GMLevelIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.GMLevelIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.GMLevelIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.GMLevelIntegerInput.Location = new System.Drawing.Point(181, 147);
            this.GMLevelIntegerInput.Name = "GMLevelIntegerInput";
            this.GMLevelIntegerInput.ShowUpDown = true;
            this.GMLevelIntegerInput.Size = new System.Drawing.Size(54, 20);
            this.GMLevelIntegerInput.TabIndex = 5;
            // 
            // okButtonX
            // 
            this.okButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.okButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.okButtonX.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButtonX.Location = new System.Drawing.Point(385, 192);
            this.okButtonX.Name = "okButtonX";
            this.okButtonX.Size = new System.Drawing.Size(75, 23);
            this.okButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.okButtonX.TabIndex = 6;
            this.okButtonX.Text = "Ok";
            this.okButtonX.Click += new System.EventHandler(this.okButtonX_Click);
            // 
            // cButtonX
            // 
            this.cButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cButtonX.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cButtonX.Location = new System.Drawing.Point(12, 192);
            this.cButtonX.Name = "cButtonX";
            this.cButtonX.Size = new System.Drawing.Size(75, 23);
            this.cButtonX.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cButtonX.TabIndex = 7;
            this.cButtonX.Text = "Cancel";
            this.cButtonX.Click += new System.EventHandler(this.cButtonX_Click);
            // 
            // AddAccount
            // 
            this.AcceptButton = this.okButtonX;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cButtonX;
            this.ClientSize = new System.Drawing.Size(472, 227);
            this.Controls.Add(this.cButtonX);
            this.Controls.Add(this.okButtonX);
            this.Controls.Add(this.GMLevelIntegerInput);
            this.Controls.Add(this.gmLevelLabelX);
            this.Controls.Add(this.passwordTextBoxX);
            this.Controls.Add(this.passwordLabelX);
            this.Controls.Add(this.accountNameTextBoxX);
            this.Controls.Add(this.accountNameLabelX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddAccount";
            this.Text = "Add Account";
            this.Load += new System.EventHandler(this.AddAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GMLevelIntegerInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX accountNameLabelX;
        private DevComponents.DotNetBar.Controls.TextBoxX accountNameTextBoxX;
        private DevComponents.DotNetBar.Controls.TextBoxX passwordTextBoxX;
        private DevComponents.DotNetBar.LabelX passwordLabelX;
        private DevComponents.DotNetBar.LabelX gmLevelLabelX;
        private DevComponents.Editors.IntegerInput GMLevelIntegerInput;
        private DevComponents.DotNetBar.ButtonX okButtonX;
        private DevComponents.DotNetBar.ButtonX cButtonX;
    }
}