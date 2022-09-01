namespace TicTacClient
{
    partial class Lobby
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
            this.availableGames = new System.Windows.Forms.ListBox();
            this.creaeGameButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.joinToGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // availableGames
            // 
            this.availableGames.FormattingEnabled = true;
            this.availableGames.ItemHeight = 25;
            this.availableGames.Location = new System.Drawing.Point(12, 12);
            this.availableGames.Name = "availableGames";
            this.availableGames.Size = new System.Drawing.Size(669, 529);
            this.availableGames.TabIndex = 0;
            // 
            // creaeGameButton
            // 
            this.creaeGameButton.Location = new System.Drawing.Point(738, 27);
            this.creaeGameButton.Name = "creaeGameButton";
            this.creaeGameButton.Size = new System.Drawing.Size(192, 62);
            this.creaeGameButton.TabIndex = 1;
            this.creaeGameButton.Text = "Create Game";
            this.creaeGameButton.UseVisualStyleBackColor = true;
            this.creaeGameButton.Click += new System.EventHandler(this.creaeGameButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1082, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // joinToGameButton
            // 
            this.joinToGameButton.Location = new System.Drawing.Point(738, 117);
            this.joinToGameButton.Name = "joinToGameButton";
            this.joinToGameButton.Size = new System.Drawing.Size(192, 65);
            this.joinToGameButton.TabIndex = 3;
            this.joinToGameButton.Text = "Join To Game";
            this.joinToGameButton.UseVisualStyleBackColor = true;
            this.joinToGameButton.Click += new System.EventHandler(this.joinToGameButton_Click);
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 966);
            this.Controls.Add(this.joinToGameButton);
            this.Controls.Add(this.creaeGameButton);
            this.Controls.Add(this.availableGames);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox availableGames;
        private Button creaeGameButton;
        private MenuStrip menuStrip1;
        private Button joinToGameButton;
    }
}