namespace UI.WinFormsViews
{
    partial class UserViewWinForm
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
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.GetUsersButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UsersDataGridView = new System.Windows.Forms.DataGridView();
            this.UserIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermissionsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetUserButton = new System.Windows.Forms.Button();
            this.UserIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PermissionsTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CardIDTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UsersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(271, 511);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(180, 42);
            this.DeleteButton.TabIndex = 48;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(25, 511);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(180, 42);
            this.AddButton.TabIndex = 47;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(25, 439);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(180, 42);
            this.UpdateButton.TabIndex = 46;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // GetUsersButton
            // 
            this.GetUsersButton.Location = new System.Drawing.Point(544, 485);
            this.GetUsersButton.Name = "GetUsersButton";
            this.GetUsersButton.Size = new System.Drawing.Size(408, 42);
            this.GetUsersButton.TabIndex = 45;
            this.GetUsersButton.Text = "Посмотреть всех пользователей";
            this.GetUsersButton.UseVisualStyleBackColor = true;
            this.GetUsersButton.Click += new System.EventHandler(this.GetUsersButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(250, 251);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(152, 35);
            this.PasswordTextBox.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 30);
            this.label3.TabIndex = 42;
            this.label3.Text = "Пароль";
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Location = new System.Drawing.Point(246, 188);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(152, 35);
            this.EmailTextBox.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 30);
            this.label2.TabIndex = 40;
            this.label2.Text = "Email";
            // 
            // UsersDataGridView
            // 
            this.UsersDataGridView.AllowUserToOrderColumns = true;
            this.UsersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UsersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserIDColumn,
            this.EmailColumn,
            this.PasswordColumn,
            this.PermissionsColumn,
            this.CardIDColumn});
            this.UsersDataGridView.Location = new System.Drawing.Point(544, 24);
            this.UsersDataGridView.Name = "UsersDataGridView";
            this.UsersDataGridView.RowHeadersWidth = 72;
            this.UsersDataGridView.RowTemplate.Height = 37;
            this.UsersDataGridView.Size = new System.Drawing.Size(1224, 438);
            this.UsersDataGridView.TabIndex = 35;
            // 
            // UserIDColumn
            // 
            this.UserIDColumn.HeaderText = "ID пользователя";
            this.UserIDColumn.MinimumWidth = 9;
            this.UserIDColumn.Name = "UserIDColumn";
            this.UserIDColumn.Width = 175;
            // 
            // EmailColumn
            // 
            this.EmailColumn.HeaderText = "Email";
            this.EmailColumn.MinimumWidth = 9;
            this.EmailColumn.Name = "EmailColumn";
            this.EmailColumn.Width = 220;
            // 
            // PasswordColumn
            // 
            this.PasswordColumn.HeaderText = "Пароль";
            this.PasswordColumn.MinimumWidth = 9;
            this.PasswordColumn.Name = "PasswordColumn";
            this.PasswordColumn.Width = 400;
            // 
            // PermissionsColumn
            // 
            this.PermissionsColumn.HeaderText = "Права доступа";
            this.PermissionsColumn.MinimumWidth = 9;
            this.PermissionsColumn.Name = "PermissionsColumn";
            this.PermissionsColumn.Width = 175;
            // 
            // CardIDColumn
            // 
            this.CardIDColumn.HeaderText = "ID карты";
            this.CardIDColumn.MinimumWidth = 9;
            this.CardIDColumn.Name = "CardIDColumn";
            this.CardIDColumn.Width = 175;
            // 
            // GetUserButton
            // 
            this.GetUserButton.Location = new System.Drawing.Point(25, 83);
            this.GetUserButton.Name = "GetUserButton";
            this.GetUserButton.Size = new System.Drawing.Size(408, 42);
            this.GetUserButton.TabIndex = 38;
            this.GetUserButton.Text = "Получить информацию";
            this.GetUserButton.UseVisualStyleBackColor = true;
            this.GetUserButton.Click += new System.EventHandler(this.GetUserButton_Click);
            // 
            // UserIDTextBox
            // 
            this.UserIDTextBox.Location = new System.Drawing.Point(246, 24);
            this.UserIDTextBox.Name = "UserIDTextBox";
            this.UserIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.UserIDTextBox.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 30);
            this.label1.TabIndex = 36;
            this.label1.Text = "ID пользователя";
            // 
            // PermissionsTextBox
            // 
            this.PermissionsTextBox.Location = new System.Drawing.Point(246, 312);
            this.PermissionsTextBox.Name = "PermissionsTextBox";
            this.PermissionsTextBox.Size = new System.Drawing.Size(152, 35);
            this.PermissionsTextBox.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 30);
            this.label4.TabIndex = 49;
            this.label4.Text = "Права доступа";
            // 
            // CardIDTextBox
            // 
            this.CardIDTextBox.Location = new System.Drawing.Point(246, 372);
            this.CardIDTextBox.Name = "CardIDTextBox";
            this.CardIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.CardIDTextBox.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 30);
            this.label5.TabIndex = 51;
            this.label5.Text = "ID карты";
            // 
            // UserViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1778, 562);
            this.Controls.Add(this.CardIDTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PermissionsTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.GetUsersButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EmailTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsersDataGridView);
            this.Controls.Add(this.GetUserButton);
            this.Controls.Add(this.UserIDTextBox);
            this.Controls.Add(this.label1);
            this.Name = "UserViewWinForm";
            this.Text = "Пользователи";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserViewWinForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.UsersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button DeleteButton;
        private Button AddButton;
        private Button UpdateButton;
        private Button GetUsersButton;
        private TextBox PasswordTextBox;
        private Label label3;
        private TextBox EmailTextBox;
        private Label label2;
        private DataGridView UsersDataGridView;
        private Button GetUserButton;
        private TextBox UserIDTextBox;
        private Label label1;
        private TextBox PermissionsTextBox;
        private Label label4;
        private TextBox CardIDTextBox;
        private Label label5;
        private DataGridViewTextBoxColumn UserIDColumn;
        private DataGridViewTextBoxColumn EmailColumn;
        private DataGridViewTextBoxColumn PasswordColumn;
        private DataGridViewTextBoxColumn PermissionsColumn;
        private DataGridViewTextBoxColumn CardIDColumn;
    }
}