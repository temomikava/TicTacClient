namespace TicTacClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginToLobbyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginToLobbyButton
            // 
            this.loginToLobbyButton.Location = new System.Drawing.Point(345, 191);
            this.loginToLobbyButton.Name = "loginToLobbyButton";
            this.loginToLobbyButton.Size = new System.Drawing.Size(321, 62);
            this.loginToLobbyButton.TabIndex = 0;
            this.loginToLobbyButton.Text = "Login to lobby";
            this.loginToLobbyButton.UseVisualStyleBackColor = true;
            this.loginToLobbyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 597);
            this.Controls.Add(this.loginToLobbyButton);
            this.Name = "Form1";
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private Button loginToLobbyButton;
    }
}