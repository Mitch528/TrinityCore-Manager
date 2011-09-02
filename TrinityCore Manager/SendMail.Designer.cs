namespace TrinityCore_Manager
{
    partial class SendMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMail));
            this.sendToLabelX = new DevComponents.DotNetBar.LabelX();
            this.onlineUsersLabelX = new DevComponents.DotNetBar.LabelX();
            this.onlineUsersComboBoxEx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.offlineUsersComboBoxEx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.offlineUsersLabelX = new DevComponents.DotNetBar.LabelX();
            this.subjectLabelX = new DevComponents.DotNetBar.LabelX();
            this.subjectTextBoxX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.itemIDLabelX = new DevComponents.DotNetBar.LabelX();
            this.itemIDIntegerInput = new DevComponents.Editors.IntegerInput();
            this.messageTextBoxX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.moneyLabelX = new DevComponents.DotNetBar.LabelX();
            this.goldIntegerInput = new DevComponents.Editors.IntegerInput();
            this.goldLabelX = new DevComponents.DotNetBar.LabelX();
            this.silverLabelX = new DevComponents.DotNetBar.LabelX();
            this.silverIntegerInput = new DevComponents.Editors.IntegerInput();
            this.copperLabelX = new DevComponents.DotNetBar.LabelX();
            this.copperIntegerInput = new DevComponents.Editors.IntegerInput();
            this.clearButton = new DevComponents.DotNetBar.ButtonX();
            this.sendButton = new DevComponents.DotNetBar.ButtonX();
            this.mysqlBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.itemIDIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goldIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.silverIntegerInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.copperIntegerInput)).BeginInit();
            this.SuspendLayout();
            // 
            // sendToLabelX
            // 
            // 
            // 
            // 
            this.sendToLabelX.BackgroundStyle.Class = "";
            this.sendToLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sendToLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendToLabelX.Location = new System.Drawing.Point(354, 21);
            this.sendToLabelX.Name = "sendToLabelX";
            this.sendToLabelX.Size = new System.Drawing.Size(75, 23);
            this.sendToLabelX.TabIndex = 0;
            this.sendToLabelX.Text = "Send To";
            // 
            // onlineUsersLabelX
            // 
            // 
            // 
            // 
            this.onlineUsersLabelX.BackgroundStyle.Class = "";
            this.onlineUsersLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.onlineUsersLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineUsersLabelX.Location = new System.Drawing.Point(121, 86);
            this.onlineUsersLabelX.Name = "onlineUsersLabelX";
            this.onlineUsersLabelX.Size = new System.Drawing.Size(89, 23);
            this.onlineUsersLabelX.TabIndex = 1;
            this.onlineUsersLabelX.Text = "Online Users";
            // 
            // onlineUsersComboBoxEx
            // 
            this.onlineUsersComboBoxEx.DisplayMember = "Text";
            this.onlineUsersComboBoxEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.onlineUsersComboBoxEx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.onlineUsersComboBoxEx.FormattingEnabled = true;
            this.onlineUsersComboBoxEx.ItemHeight = 14;
            this.onlineUsersComboBoxEx.Location = new System.Drawing.Point(217, 87);
            this.onlineUsersComboBoxEx.Name = "onlineUsersComboBoxEx";
            this.onlineUsersComboBoxEx.Size = new System.Drawing.Size(165, 20);
            this.onlineUsersComboBoxEx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.onlineUsersComboBoxEx.TabIndex = 2;
            this.onlineUsersComboBoxEx.SelectedIndexChanged += new System.EventHandler(this.onlineUsersComboBoxEx_SelectedIndexChanged);
            // 
            // offlineUsersComboBoxEx
            // 
            this.offlineUsersComboBoxEx.DisplayMember = "Text";
            this.offlineUsersComboBoxEx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.offlineUsersComboBoxEx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.offlineUsersComboBoxEx.FormattingEnabled = true;
            this.offlineUsersComboBoxEx.ItemHeight = 14;
            this.offlineUsersComboBoxEx.Location = new System.Drawing.Point(497, 86);
            this.offlineUsersComboBoxEx.Name = "offlineUsersComboBoxEx";
            this.offlineUsersComboBoxEx.Size = new System.Drawing.Size(165, 20);
            this.offlineUsersComboBoxEx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.offlineUsersComboBoxEx.TabIndex = 4;
            this.offlineUsersComboBoxEx.SelectedIndexChanged += new System.EventHandler(this.offlineUsersComboBoxEx_SelectedIndexChanged);
            // 
            // offlineUsersLabelX
            // 
            // 
            // 
            // 
            this.offlineUsersLabelX.BackgroundStyle.Class = "";
            this.offlineUsersLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.offlineUsersLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offlineUsersLabelX.Location = new System.Drawing.Point(401, 85);
            this.offlineUsersLabelX.Name = "offlineUsersLabelX";
            this.offlineUsersLabelX.Size = new System.Drawing.Size(89, 23);
            this.offlineUsersLabelX.TabIndex = 3;
            this.offlineUsersLabelX.Text = "Offline Users";
            // 
            // subjectLabelX
            // 
            // 
            // 
            // 
            this.subjectLabelX.BackgroundStyle.Class = "";
            this.subjectLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.subjectLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectLabelX.Location = new System.Drawing.Point(217, 187);
            this.subjectLabelX.Name = "subjectLabelX";
            this.subjectLabelX.Size = new System.Drawing.Size(54, 23);
            this.subjectLabelX.TabIndex = 5;
            this.subjectLabelX.Text = "Subject";
            // 
            // subjectTextBoxX
            // 
            // 
            // 
            // 
            this.subjectTextBoxX.Border.Class = "TextBoxBorder";
            this.subjectTextBoxX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.subjectTextBoxX.Location = new System.Drawing.Point(278, 189);
            this.subjectTextBoxX.Name = "subjectTextBoxX";
            this.subjectTextBoxX.Size = new System.Drawing.Size(270, 20);
            this.subjectTextBoxX.TabIndex = 6;
            // 
            // itemIDLabelX
            // 
            // 
            // 
            // 
            this.itemIDLabelX.BackgroundStyle.Class = "";
            this.itemIDLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemIDLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemIDLabelX.Location = new System.Drawing.Point(288, 135);
            this.itemIDLabelX.Name = "itemIDLabelX";
            this.itemIDLabelX.Size = new System.Drawing.Size(94, 23);
            this.itemIDLabelX.TabIndex = 7;
            this.itemIDLabelX.Text = "Item Entry ID";
            // 
            // itemIDIntegerInput
            // 
            this.itemIDIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.itemIDIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.itemIDIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemIDIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.itemIDIntegerInput.Location = new System.Drawing.Point(389, 137);
            this.itemIDIntegerInput.MinValue = 0;
            this.itemIDIntegerInput.Name = "itemIDIntegerInput";
            this.itemIDIntegerInput.ShowUpDown = true;
            this.itemIDIntegerInput.Size = new System.Drawing.Size(90, 20);
            this.itemIDIntegerInput.TabIndex = 8;
            // 
            // messageTextBoxX
            // 
            // 
            // 
            // 
            this.messageTextBoxX.Border.Class = "TextBoxBorder";
            this.messageTextBoxX.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.messageTextBoxX.Location = new System.Drawing.Point(12, 234);
            this.messageTextBoxX.Multiline = true;
            this.messageTextBoxX.Name = "messageTextBoxX";
            this.messageTextBoxX.Size = new System.Drawing.Size(778, 205);
            this.messageTextBoxX.TabIndex = 9;
            // 
            // moneyLabelX
            // 
            // 
            // 
            // 
            this.moneyLabelX.BackgroundStyle.Class = "";
            this.moneyLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.moneyLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moneyLabelX.Location = new System.Drawing.Point(177, 461);
            this.moneyLabelX.Name = "moneyLabelX";
            this.moneyLabelX.Size = new System.Drawing.Size(53, 23);
            this.moneyLabelX.TabIndex = 10;
            this.moneyLabelX.Text = "Money";
            // 
            // goldIntegerInput
            // 
            this.goldIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.goldIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.goldIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.goldIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.goldIntegerInput.Location = new System.Drawing.Point(257, 464);
            this.goldIntegerInput.MinValue = 0;
            this.goldIntegerInput.Name = "goldIntegerInput";
            this.goldIntegerInput.ShowUpDown = true;
            this.goldIntegerInput.Size = new System.Drawing.Size(67, 20);
            this.goldIntegerInput.TabIndex = 11;
            // 
            // goldLabelX
            // 
            // 
            // 
            // 
            this.goldLabelX.BackgroundStyle.Class = "";
            this.goldLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.goldLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goldLabelX.Location = new System.Drawing.Point(330, 464);
            this.goldLabelX.Name = "goldLabelX";
            this.goldLabelX.Size = new System.Drawing.Size(38, 23);
            this.goldLabelX.TabIndex = 12;
            this.goldLabelX.Text = "Gold";
            // 
            // silverLabelX
            // 
            // 
            // 
            // 
            this.silverLabelX.BackgroundStyle.Class = "";
            this.silverLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.silverLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.silverLabelX.Location = new System.Drawing.Point(462, 464);
            this.silverLabelX.Name = "silverLabelX";
            this.silverLabelX.Size = new System.Drawing.Size(38, 23);
            this.silverLabelX.TabIndex = 14;
            this.silverLabelX.Text = "Silver";
            // 
            // silverIntegerInput
            // 
            this.silverIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.silverIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.silverIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.silverIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.silverIntegerInput.Location = new System.Drawing.Point(389, 464);
            this.silverIntegerInput.MinValue = 0;
            this.silverIntegerInput.Name = "silverIntegerInput";
            this.silverIntegerInput.ShowUpDown = true;
            this.silverIntegerInput.Size = new System.Drawing.Size(67, 20);
            this.silverIntegerInput.TabIndex = 13;
            // 
            // copperLabelX
            // 
            // 
            // 
            // 
            this.copperLabelX.BackgroundStyle.Class = "";
            this.copperLabelX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.copperLabelX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copperLabelX.Location = new System.Drawing.Point(593, 464);
            this.copperLabelX.Name = "copperLabelX";
            this.copperLabelX.Size = new System.Drawing.Size(52, 23);
            this.copperLabelX.TabIndex = 16;
            this.copperLabelX.Text = "Copper";
            // 
            // copperIntegerInput
            // 
            this.copperIntegerInput.AllowEmptyState = false;
            // 
            // 
            // 
            this.copperIntegerInput.BackgroundStyle.Class = "DateTimeInputBackground";
            this.copperIntegerInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.copperIntegerInput.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.copperIntegerInput.Location = new System.Drawing.Point(520, 464);
            this.copperIntegerInput.MinValue = 0;
            this.copperIntegerInput.Name = "copperIntegerInput";
            this.copperIntegerInput.ShowUpDown = true;
            this.copperIntegerInput.Size = new System.Drawing.Size(67, 20);
            this.copperIntegerInput.TabIndex = 15;
            // 
            // clearButton
            // 
            this.clearButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.clearButton.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.clearButton.Location = new System.Drawing.Point(330, 525);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.clearButton.TabIndex = 17;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.sendButton.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.sendButton.Location = new System.Drawing.Point(427, 525);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sendButton.TabIndex = 18;
            this.sendButton.Text = "Send";
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // mysqlBackgroundWorker
            // 
            this.mysqlBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mysqlBackgroundWorker_DoWork);
            this.mysqlBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mysqlBackgroundWorker_RunWorkerCompleted);
            // 
            // SendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 560);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.copperLabelX);
            this.Controls.Add(this.copperIntegerInput);
            this.Controls.Add(this.silverLabelX);
            this.Controls.Add(this.silverIntegerInput);
            this.Controls.Add(this.goldLabelX);
            this.Controls.Add(this.goldIntegerInput);
            this.Controls.Add(this.moneyLabelX);
            this.Controls.Add(this.messageTextBoxX);
            this.Controls.Add(this.itemIDIntegerInput);
            this.Controls.Add(this.itemIDLabelX);
            this.Controls.Add(this.subjectTextBoxX);
            this.Controls.Add(this.subjectLabelX);
            this.Controls.Add(this.offlineUsersComboBoxEx);
            this.Controls.Add(this.offlineUsersLabelX);
            this.Controls.Add(this.onlineUsersComboBoxEx);
            this.Controls.Add(this.onlineUsersLabelX);
            this.Controls.Add(this.sendToLabelX);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendMail";
            this.Text = "Send Mail";
            this.Load += new System.EventHandler(this.SendMail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.itemIDIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goldIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.silverIntegerInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.copperIntegerInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX sendToLabelX;
        private DevComponents.DotNetBar.LabelX onlineUsersLabelX;
        private DevComponents.DotNetBar.Controls.ComboBoxEx onlineUsersComboBoxEx;
        private DevComponents.DotNetBar.Controls.ComboBoxEx offlineUsersComboBoxEx;
        private DevComponents.DotNetBar.LabelX offlineUsersLabelX;
        private DevComponents.DotNetBar.LabelX subjectLabelX;
        private DevComponents.DotNetBar.Controls.TextBoxX subjectTextBoxX;
        private DevComponents.DotNetBar.LabelX itemIDLabelX;
        private DevComponents.Editors.IntegerInput itemIDIntegerInput;
        private DevComponents.DotNetBar.Controls.TextBoxX messageTextBoxX;
        private DevComponents.DotNetBar.LabelX moneyLabelX;
        private DevComponents.Editors.IntegerInput goldIntegerInput;
        private DevComponents.DotNetBar.LabelX goldLabelX;
        private DevComponents.DotNetBar.LabelX silverLabelX;
        private DevComponents.Editors.IntegerInput silverIntegerInput;
        private DevComponents.DotNetBar.LabelX copperLabelX;
        private DevComponents.Editors.IntegerInput copperIntegerInput;
        private DevComponents.DotNetBar.ButtonX clearButton;
        private DevComponents.DotNetBar.ButtonX sendButton;
        private System.ComponentModel.BackgroundWorker mysqlBackgroundWorker;
    }
}