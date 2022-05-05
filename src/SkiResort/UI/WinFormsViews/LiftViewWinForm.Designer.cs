namespace UI.WinFormsViews
{
    partial class LiftViewWinForm
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
            this.GetInfosButton = new System.Windows.Forms.Button();
            this.DeleteConnectedSlopeButton = new System.Windows.Forms.Button();
            this.AddConnectedSlopeButton = new System.Windows.Forms.Button();
            this.SlopeNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SeatsAmountTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IsOpenTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GetInfoButton = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QueueTimeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LiftingTimeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LiftsDataGridView = new System.Windows.Forms.DataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOpenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeatsAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiftingTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QueueTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectedSlopesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.LiftsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GetInfosButton
            // 
            this.GetInfosButton.Location = new System.Drawing.Point(817, 713);
            this.GetInfosButton.Name = "GetInfosButton";
            this.GetInfosButton.Size = new System.Drawing.Size(382, 42);
            this.GetInfosButton.TabIndex = 30;
            this.GetInfosButton.Text = "Посмотреть все подъемники";
            this.GetInfosButton.UseVisualStyleBackColor = true;
            this.GetInfosButton.Click += new System.EventHandler(this.GetInfosButton_Click);
            // 
            // DeleteConnectedSlopeButton
            // 
            this.DeleteConnectedSlopeButton.Location = new System.Drawing.Point(253, 713);
            this.DeleteConnectedSlopeButton.Name = "DeleteConnectedSlopeButton";
            this.DeleteConnectedSlopeButton.Size = new System.Drawing.Size(213, 42);
            this.DeleteConnectedSlopeButton.TabIndex = 29;
            this.DeleteConnectedSlopeButton.Text = "Удалить связь";
            this.DeleteConnectedSlopeButton.UseVisualStyleBackColor = true;
            this.DeleteConnectedSlopeButton.Click += new System.EventHandler(this.DeleteConnectedSlopeButton_Click);
            // 
            // AddConnectedSlopeButton
            // 
            this.AddConnectedSlopeButton.Location = new System.Drawing.Point(28, 713);
            this.AddConnectedSlopeButton.Name = "AddConnectedSlopeButton";
            this.AddConnectedSlopeButton.Size = new System.Drawing.Size(204, 42);
            this.AddConnectedSlopeButton.TabIndex = 28;
            this.AddConnectedSlopeButton.Text = "Добавить связь";
            this.AddConnectedSlopeButton.UseVisualStyleBackColor = true;
            this.AddConnectedSlopeButton.Click += new System.EventHandler(this.AddConnectedSlopeButton_Click);
            // 
            // SlopeNameTextBox
            // 
            this.SlopeNameTextBox.Location = new System.Drawing.Point(305, 659);
            this.SlopeNameTextBox.Name = "SlopeNameTextBox";
            this.SlopeNameTextBox.Size = new System.Drawing.Size(152, 35);
            this.SlopeNameTextBox.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 659);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 30);
            this.label4.TabIndex = 26;
            this.label4.Text = "Название спуска";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(234, 510);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(171, 42);
            this.DeleteButton.TabIndex = 25;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(32, 510);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(171, 42);
            this.AddButton.TabIndex = 24;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(32, 450);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(171, 42);
            this.UpdateButton.TabIndex = 23;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // SeatsAmountTextBox
            // 
            this.SeatsAmountTextBox.Location = new System.Drawing.Point(253, 264);
            this.SeatsAmountTextBox.Name = "SeatsAmountTextBox";
            this.SeatsAmountTextBox.Size = new System.Drawing.Size(152, 35);
            this.SeatsAmountTextBox.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 30);
            this.label3.TabIndex = 21;
            this.label3.Text = "Количество мест";
            // 
            // IsOpenTextBox
            // 
            this.IsOpenTextBox.Location = new System.Drawing.Point(253, 203);
            this.IsOpenTextBox.Name = "IsOpenTextBox";
            this.IsOpenTextBox.Size = new System.Drawing.Size(152, 35);
            this.IsOpenTextBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 30);
            this.label2.TabIndex = 19;
            this.label2.Text = "Открыт";
            // 
            // GetInfoButton
            // 
            this.GetInfoButton.Location = new System.Drawing.Point(32, 82);
            this.GetInfoButton.Name = "GetInfoButton";
            this.GetInfoButton.Size = new System.Drawing.Size(292, 42);
            this.GetInfoButton.TabIndex = 18;
            this.GetInfoButton.Text = "Получить информацию";
            this.GetInfoButton.UseVisualStyleBackColor = true;
            this.GetInfoButton.Click += new System.EventHandler(this.GetInfoButton_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(253, 23);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(152, 35);
            this.NameTextBox.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 30);
            this.label1.TabIndex = 16;
            this.label1.Text = "Название";
            // 
            // QueueTimeTextBox
            // 
            this.QueueTimeTextBox.Location = new System.Drawing.Point(257, 390);
            this.QueueTimeTextBox.Name = "QueueTimeTextBox";
            this.QueueTimeTextBox.ReadOnly = true;
            this.QueueTimeTextBox.Size = new System.Drawing.Size(152, 35);
            this.QueueTimeTextBox.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 30);
            this.label5.TabIndex = 33;
            this.label5.Text = "Время в очереди";
            // 
            // LiftingTimeTextBox
            // 
            this.LiftingTimeTextBox.Location = new System.Drawing.Point(257, 329);
            this.LiftingTimeTextBox.Name = "LiftingTimeTextBox";
            this.LiftingTimeTextBox.Size = new System.Drawing.Size(152, 35);
            this.LiftingTimeTextBox.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 30);
            this.label6.TabIndex = 31;
            this.label6.Text = "Время подъема";
            // 
            // LiftsDataGridView
            // 
            this.LiftsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LiftsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.NameColumn,
            this.IsOpenColumn,
            this.SeatsAmountColumn,
            this.LiftingTimeColumn,
            this.QueueTimeColumn,
            this.ConnectedSlopesColumn});
            this.LiftsDataGridView.Location = new System.Drawing.Point(551, 23);
            this.LiftsDataGridView.Name = "LiftsDataGridView";
            this.LiftsDataGridView.RowHeadersWidth = 72;
            this.LiftsDataGridView.RowTemplate.Height = 37;
            this.LiftsDataGridView.Size = new System.Drawing.Size(1326, 438);
            this.LiftsDataGridView.TabIndex = 16;
            // 
            // IDColumn
            // 
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.MinimumWidth = 9;
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.Width = 175;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Название";
            this.NameColumn.MinimumWidth = 9;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.Width = 175;
            // 
            // IsOpenColumn
            // 
            this.IsOpenColumn.HeaderText = "Открыт";
            this.IsOpenColumn.MinimumWidth = 9;
            this.IsOpenColumn.Name = "IsOpenColumn";
            this.IsOpenColumn.Width = 175;
            // 
            // SeatsAmountColumn
            // 
            this.SeatsAmountColumn.HeaderText = "Количество мест";
            this.SeatsAmountColumn.MinimumWidth = 9;
            this.SeatsAmountColumn.Name = "SeatsAmountColumn";
            this.SeatsAmountColumn.Width = 175;
            // 
            // LiftingTimeColumn
            // 
            this.LiftingTimeColumn.HeaderText = "Время подъема";
            this.LiftingTimeColumn.MinimumWidth = 9;
            this.LiftingTimeColumn.Name = "LiftingTimeColumn";
            this.LiftingTimeColumn.Width = 175;
            // 
            // QueueTimeColumn
            // 
            this.QueueTimeColumn.HeaderText = "Время в очереди";
            this.QueueTimeColumn.MinimumWidth = 9;
            this.QueueTimeColumn.Name = "QueueTimeColumn";
            this.QueueTimeColumn.Width = 175;
            // 
            // ConnectedSlopesColumn
            // 
            this.ConnectedSlopesColumn.HeaderText = "Куда поехать";
            this.ConnectedSlopesColumn.MinimumWidth = 9;
            this.ConnectedSlopesColumn.Name = "ConnectedSlopesColumn";
            this.ConnectedSlopesColumn.Width = 175;
            // 
            // LiftViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1911, 794);
            this.Controls.Add(this.LiftsDataGridView);
            this.Controls.Add(this.QueueTimeTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LiftingTimeTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.GetInfosButton);
            this.Controls.Add(this.DeleteConnectedSlopeButton);
            this.Controls.Add(this.AddConnectedSlopeButton);
            this.Controls.Add(this.SlopeNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SeatsAmountTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IsOpenTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GetInfoButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "LiftViewWinForm";
            this.Text = "Подъемники";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LiftViewWinForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.LiftsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button GetInfosButton;
        private Button DeleteConnectedSlopeButton;
        private Button AddConnectedSlopeButton;
        private TextBox SlopeNameTextBox;
        private Label label4;
        private Button DeleteButton;
        private Button AddButton;
        private Button UpdateButton;
        private TextBox SeatsAmountTextBox;
        private Label label3;
        private TextBox IsOpenTextBox;
        private Label label2;
        private Button GetInfoButton;
        private TextBox NameTextBox;
        private Label label1;
        private TextBox QueueTimeTextBox;
        private Label label5;
        private TextBox LiftingTimeTextBox;
        private Label label6;
        private DataGridView LiftsDataGridView;
        private DataGridViewTextBoxColumn IDColumn;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn IsOpenColumn;
        private DataGridViewTextBoxColumn SeatsAmountColumn;
        private DataGridViewTextBoxColumn LiftingTimeColumn;
        private DataGridViewTextBoxColumn QueueTimeColumn;
        private DataGridViewTextBoxColumn ConnectedSlopesColumn;
    }
}