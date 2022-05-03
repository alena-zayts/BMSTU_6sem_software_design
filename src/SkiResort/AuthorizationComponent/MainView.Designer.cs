namespace AuthorizationComponent
{
    partial class MainView
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
            this.profileButton = new System.Windows.Forms.Button();
            this.slopeButton = new System.Windows.Forms.Button();
            this.liftButton = new System.Windows.Forms.Button();
            this.messageButton = new System.Windows.Forms.Button();
            this.turnstileButton = new System.Windows.Forms.Button();
            this.userButton = new System.Windows.Forms.Button();
            this.cardReadingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // profileButton
            // 
            this.profileButton.Location = new System.Drawing.Point(52, 58);
            this.profileButton.Name = "profileButton";
            this.profileButton.Size = new System.Drawing.Size(189, 63);
            this.profileButton.TabIndex = 0;
            this.profileButton.Text = "Профиль";
            this.profileButton.UseVisualStyleBackColor = true;
            this.profileButton.Click += new System.EventHandler(this.profileButton_Click);
            // 
            // slopeButton
            // 
            this.slopeButton.Location = new System.Drawing.Point(52, 140);
            this.slopeButton.Name = "slopeButton";
            this.slopeButton.Size = new System.Drawing.Size(189, 63);
            this.slopeButton.TabIndex = 1;
            this.slopeButton.Text = "Спуски";
            this.slopeButton.UseVisualStyleBackColor = true;
            this.slopeButton.Click += new System.EventHandler(this.slopeButton_Click);
            // 
            // liftButton
            // 
            this.liftButton.Location = new System.Drawing.Point(52, 227);
            this.liftButton.Name = "liftButton";
            this.liftButton.Size = new System.Drawing.Size(189, 63);
            this.liftButton.TabIndex = 2;
            this.liftButton.Text = "Подъемники";
            this.liftButton.UseVisualStyleBackColor = true;
            this.liftButton.Click += new System.EventHandler(this.liftButton_Click);
            // 
            // messageButton
            // 
            this.messageButton.Location = new System.Drawing.Point(52, 312);
            this.messageButton.Name = "messageButton";
            this.messageButton.Size = new System.Drawing.Size(189, 63);
            this.messageButton.TabIndex = 3;
            this.messageButton.Text = "Сообщения";
            this.messageButton.UseVisualStyleBackColor = true;
            this.messageButton.Click += new System.EventHandler(this.messageButton_Click);
            // 
            // turnstileButton
            // 
            this.turnstileButton.Location = new System.Drawing.Point(386, 58);
            this.turnstileButton.Name = "turnstileButton";
            this.turnstileButton.Size = new System.Drawing.Size(189, 63);
            this.turnstileButton.TabIndex = 4;
            this.turnstileButton.Text = "Турникеты";
            this.turnstileButton.UseVisualStyleBackColor = true;
            this.turnstileButton.Click += new System.EventHandler(this.turnstileButton_Click);
            // 
            // userButton
            // 
            this.userButton.Location = new System.Drawing.Point(386, 140);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(189, 63);
            this.userButton.TabIndex = 5;
            this.userButton.Text = "Пользователи";
            this.userButton.UseVisualStyleBackColor = true;
            this.userButton.Click += new System.EventHandler(this.userButton_Click);
            // 
            // ccardReadingButton
            // 
            this.cardReadingButton.Location = new System.Drawing.Point(386, 227);
            this.cardReadingButton.Name = "ccardReadingButton";
            this.cardReadingButton.Size = new System.Drawing.Size(189, 63);
            this.cardReadingButton.TabIndex = 6;
            this.cardReadingButton.Text = "Чтения карт";
            this.cardReadingButton.UseVisualStyleBackColor = true;
            this.cardReadingButton.Click += new System.EventHandler(this.ccardReadingButton_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cardReadingButton);
            this.Controls.Add(this.userButton);
            this.Controls.Add(this.turnstileButton);
            this.Controls.Add(this.messageButton);
            this.Controls.Add(this.liftButton);
            this.Controls.Add(this.slopeButton);
            this.Controls.Add(this.profileButton);
            this.Name = "MainView";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Button profileButton;
        private Button slopeButton;
        private Button liftButton;
        private Button messageButton;
        private Button turnstileButton;
        private Button userButton;
        private Button cardReadingButton;
    }
}