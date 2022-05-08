namespace UI.WinFormsViews
{
    partial class CardReadingViewWinForm
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
            this.СardReadingsDataGridView = new System.Windows.Forms.DataGridView();
            this.recordIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TurnstileIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadingTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetCardReadingButton = new System.Windows.Forms.Button();
            this.RecordIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReadingTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.TurnstileIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CardIDTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GetCardReadingsButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.СardReadingsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // СardReadingsDataGridView
            // 
            this.СardReadingsDataGridView.AllowUserToOrderColumns = true;
            this.СardReadingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.СardReadingsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recordIDColumn,
            this.TurnstileIDColumn,
            this.CardIDColumn,
            this.ReadingTimeColumn});
            this.СardReadingsDataGridView.Location = new System.Drawing.Point(538, 24);
            this.СardReadingsDataGridView.Name = "СardReadingsDataGridView";
            this.СardReadingsDataGridView.RowHeadersWidth = 72;
            this.СardReadingsDataGridView.RowTemplate.Height = 37;
            this.СardReadingsDataGridView.Size = new System.Drawing.Size(1326, 438);
            this.СardReadingsDataGridView.TabIndex = 19;
            // 
            // recordIDColumn
            // 
            this.recordIDColumn.HeaderText = "ID записи";
            this.recordIDColumn.MinimumWidth = 9;
            this.recordIDColumn.Name = "recordIDColumn";
            this.recordIDColumn.Width = 175;
            // 
            // TurnstileIDColumn
            // 
            this.TurnstileIDColumn.HeaderText = "ID подъемника";
            this.TurnstileIDColumn.MinimumWidth = 9;
            this.TurnstileIDColumn.Name = "TurnstileIDColumn";
            this.TurnstileIDColumn.Width = 220;
            // 
            // CardIDColumn
            // 
            this.CardIDColumn.HeaderText = "ID карты";
            this.CardIDColumn.MinimumWidth = 9;
            this.CardIDColumn.Name = "CardIDColumn";
            this.CardIDColumn.Width = 175;
            // 
            // ReadingTimeColumn
            // 
            this.ReadingTimeColumn.HeaderText = "Время считывания";
            this.ReadingTimeColumn.MinimumWidth = 9;
            this.ReadingTimeColumn.Name = "ReadingTimeColumn";
            this.ReadingTimeColumn.Width = 400;
            // 
            // GetCardReadingButton
            // 
            this.GetCardReadingButton.Location = new System.Drawing.Point(19, 83);
            this.GetCardReadingButton.Name = "GetCardReadingButton";
            this.GetCardReadingButton.Size = new System.Drawing.Size(408, 42);
            this.GetCardReadingButton.TabIndex = 22;
            this.GetCardReadingButton.Text = "Получить информацию";
            this.GetCardReadingButton.UseVisualStyleBackColor = true;
            this.GetCardReadingButton.Click += new System.EventHandler(this.GetCardReadingButton_Click);
            // 
            // RecordIDTextBox
            // 
            this.RecordIDTextBox.Location = new System.Drawing.Point(240, 24);
            this.RecordIDTextBox.Name = "RecordIDTextBox";
            this.RecordIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.RecordIDTextBox.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 30);
            this.label1.TabIndex = 20;
            this.label1.Text = "ID записи";
            // 
            // ReadingTimeDateTimePicker
            // 
            this.ReadingTimeDateTimePicker.Location = new System.Drawing.Point(244, 315);
            this.ReadingTimeDateTimePicker.Name = "ReadingTimeDateTimePicker";
            this.ReadingTimeDateTimePicker.Size = new System.Drawing.Size(265, 35);
            this.ReadingTimeDateTimePicker.TabIndex = 23;
            // 
            // TurnstileIDTextBox
            // 
            this.TurnstileIDTextBox.Location = new System.Drawing.Point(240, 188);
            this.TurnstileIDTextBox.Name = "TurnstileIDTextBox";
            this.TurnstileIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.TurnstileIDTextBox.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 30);
            this.label2.TabIndex = 24;
            this.label2.Text = "ID подъемника";
            // 
            // CardIDTextBox
            // 
            this.CardIDTextBox.Location = new System.Drawing.Point(244, 251);
            this.CardIDTextBox.Name = "CardIDTextBox";
            this.CardIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.CardIDTextBox.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 30);
            this.label3.TabIndex = 26;
            this.label3.Text = "ID карты";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 30);
            this.label4.TabIndex = 28;
            this.label4.Text = "Время считывания";
            // 
            // GetCardReadingsButton
            // 
            this.GetCardReadingsButton.Location = new System.Drawing.Point(566, 485);
            this.GetCardReadingsButton.Name = "GetCardReadingsButton";
            this.GetCardReadingsButton.Size = new System.Drawing.Size(408, 42);
            this.GetCardReadingsButton.TabIndex = 29;
            this.GetCardReadingsButton.Text = "Посмотреть все считывания";
            this.GetCardReadingsButton.UseVisualStyleBackColor = true;
            this.GetCardReadingsButton.Click += new System.EventHandler(this.GetCardReadingsButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(19, 394);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(180, 42);
            this.UpdateButton.TabIndex = 30;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(19, 466);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(180, 42);
            this.AddButton.TabIndex = 31;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(265, 466);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(180, 42);
            this.DeleteButton.TabIndex = 32;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CardReadingViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1422, 751);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.GetCardReadingsButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CardIDTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TurnstileIDTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReadingTimeDateTimePicker);
            this.Controls.Add(this.СardReadingsDataGridView);
            this.Controls.Add(this.GetCardReadingButton);
            this.Controls.Add(this.RecordIDTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CardReadingViewWinForm";
            this.Text = "Считывания карт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardReadingViewWinForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.СardReadingsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView СardReadingsDataGridView;
        private Button GetCardReadingButton;
        private TextBox RecordIDTextBox;
        private Label label1;
        private DataGridViewTextBoxColumn recordIDColumn;
        private DataGridViewTextBoxColumn TurnstileIDColumn;
        private DataGridViewTextBoxColumn CardIDColumn;
        private DataGridViewTextBoxColumn ReadingTimeColumn;
        private DateTimePicker ReadingTimeDateTimePicker;
        private TextBox TurnstileIDTextBox;
        private Label label2;
        private TextBox CardIDTextBox;
        private Label label3;
        private Label label4;
        private Button GetCardReadingsButton;
        private Button UpdateButton;
        private Button AddButton;
        private Button DeleteButton;
    }
}