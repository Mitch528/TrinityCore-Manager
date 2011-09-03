namespace TrinityCore_Manager
{
    partial class Splash
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
            this.splashPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splashPictureBox
            // 
            this.splashPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splashPictureBox.Image = global::TrinityCore_Manager.Properties.Resources.TrinityCore_Manager;
            this.splashPictureBox.Location = new System.Drawing.Point(0, 0);
            this.splashPictureBox.Name = "splashPictureBox";
            this.splashPictureBox.Size = new System.Drawing.Size(493, 353);
            this.splashPictureBox.TabIndex = 0;
            this.splashPictureBox.TabStop = false;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 353);
            this.Controls.Add(this.splashPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.Load += new System.EventHandler(this.Splash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox splashPictureBox;
    }
}