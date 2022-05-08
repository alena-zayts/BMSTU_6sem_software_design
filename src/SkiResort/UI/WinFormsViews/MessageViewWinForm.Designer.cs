namespace UI.WinFormsViews
{
    partial class MessageViewWinForm
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
            this.MessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.MessageIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenderIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckedByIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetMessagesButton = new System.Windows.Forms.Button();
            this.MessageIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MarkCheckedButton = new System.Windows.Forms.Button();
            this.GetMessageButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.TextTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SenderIDTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CheckedByIDTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MessagesDataGridView
            // 
            this.MessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MessagesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MessageIDColumn,
            this.SenderIDColumn,
            this.CheckedByIDColumn,
            this.TextColumn});
            this.MessagesDataGridView.Location = new System.Drawing.Point(23, 30);
            this.MessagesDataGridView.Name = "MessagesDataGridView";
            this.MessagesDataGridView.RowHeadersWidth = 72;
            this.MessagesDataGridView.RowTemplate.Height = 37;
            this.MessagesDataGridView.Size = new System.Drawing.Size(1106, 282);
            this.MessagesDataGridView.TabIndex = 17;
            // 
            // MessageIDColumn
            // 
            this.MessageIDColumn.HeaderText = "ID";
            this.MessageIDColumn.MinimumWidth = 9;
            this.MessageIDColumn.Name = "MessageIDColumn";
            this.MessageIDColumn.Width = 175;
            // 
            // SenderIDColumn
            // 
            this.SenderIDColumn.HeaderText = "Отправитель";
            this.SenderIDColumn.MinimumWidth = 9;
            this.SenderIDColumn.Name = "SenderIDColumn";
            this.SenderIDColumn.Width = 175;
            // 
            // CheckedByIDColumn
            // 
            this.CheckedByIDColumn.HeaderText = "Кем проверено";
            this.CheckedByIDColumn.MinimumWidth = 9;
            this.CheckedByIDColumn.Name = "CheckedByIDColumn";
            this.CheckedByIDColumn.Width = 175;
            // 
            // TextColumn
            // 
            this.TextColumn.HeaderText = "Текст";
            this.TextColumn.MinimumWidth = 9;
            this.TextColumn.Name = "TextColumn";
            this.TextColumn.Width = 500;
            // 
            // GetMessagesButton
            // 
            this.GetMessagesButton.Location = new System.Drawing.Point(23, 336);
            this.GetMessagesButton.Name = "GetMessagesButton";
            this.GetMessagesButton.Size = new System.Drawing.Size(292, 42);
            this.GetMessagesButton.TabIndex = 19;
            this.GetMessagesButton.Text = "Посмотреть все сообщения";
            this.GetMessagesButton.UseVisualStyleBackColor = true;
            this.GetMessagesButton.Click += new System.EventHandler(this.GetMessagesButton_Click);
            // 
            // MessageIDTextBox
            // 
            this.MessageIDTextBox.Location = new System.Drawing.Point(248, 463);
            this.MessageIDTextBox.Name = "MessageIDTextBox";
            this.MessageIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.MessageIDTextBox.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 463);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 30);
            this.label2.TabIndex = 21;
            this.label2.Text = "ID";
            // 
            // MarkCheckedButton
            // 
            this.MarkCheckedButton.Location = new System.Drawing.Point(23, 530);
            this.MarkCheckedButton.Name = "MarkCheckedButton";
            this.MarkCheckedButton.Size = new System.Drawing.Size(266, 42);
            this.MarkCheckedButton.TabIndex = 23;
            this.MarkCheckedButton.Text = "Отметить прочитанным";
            this.MarkCheckedButton.UseVisualStyleBackColor = true;
            this.MarkCheckedButton.Click += new System.EventHandler(this.MarkCheckedButton_Click);
            // 
            // GetMessageButton
            // 
            this.GetMessageButton.Location = new System.Drawing.Point(334, 530);
            this.GetMessageButton.Name = "GetMessageButton";
            this.GetMessageButton.Size = new System.Drawing.Size(266, 42);
            this.GetMessageButton.TabIndex = 24;
            this.GetMessageButton.Text = "Посмореть сообщение";
            this.GetMessageButton.UseVisualStyleBackColor = true;
            this.GetMessageButton.Click += new System.EventHandler(this.GetMessageButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(23, 701);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(266, 42);
            this.SendButton.TabIndex = 27;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // TextTextBox
            // 
            this.TextTextBox.Location = new System.Drawing.Point(248, 634);
            this.TextTextBox.Name = "TextTextBox";
            this.TextTextBox.Size = new System.Drawing.Size(823, 35);
            this.TextTextBox.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 634);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 30);
            this.label1.TabIndex = 25;
            this.label1.Text = "Текст";
            // 
            // SenderIDTextBox
            // 
            this.SenderIDTextBox.Location = new System.Drawing.Point(248, 804);
            this.SenderIDTextBox.Name = "SenderIDTextBox";
            this.SenderIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.SenderIDTextBox.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 804);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 30);
            this.label3.TabIndex = 28;
            this.label3.Text = "ID отправителя";
            // 
            // CheckedByIDTextBox
            // 
            this.CheckedByIDTextBox.Location = new System.Drawing.Point(248, 866);
            this.CheckedByIDTextBox.Name = "CheckedByIDTextBox";
            this.CheckedByIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.CheckedByIDTextBox.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 866);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 30);
            this.label4.TabIndex = 30;
            this.label4.Text = "ID прочитавшего";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(23, 932);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(266, 42);
            this.UpdateButton.TabIndex = 32;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(334, 932);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(266, 42);
            this.DeleteButton.TabIndex = 33;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // MessageViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 1012);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.CheckedByIDTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SenderIDTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.TextTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetMessageButton);
            this.Controls.Add(this.MarkCheckedButton);
            this.Controls.Add(this.MessageIDTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GetMessagesButton);
            this.Controls.Add(this.MessagesDataGridView);
            this.Name = "MessageViewWinForm";
            this.Text = "Сообщеня";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageViewWinForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView MessagesDataGridView;
        private DataGridViewTextBoxColumn MessageIDColumn;
        private DataGridViewTextBoxColumn SenderIDColumn;
        private DataGridViewTextBoxColumn CheckedByIDColumn;
        private DataGridViewTextBoxColumn TextColumn;
        private Button GetMessagesButton;
        private TextBox MessageIDTextBox;
        private Label label2;
        private Button MarkCheckedButton;
        private Button GetMessageButton;
        private Button SendButton;
        private TextBox TextTextBox;
        private Label label1;
        private TextBox SenderIDTextBox;
        private Label label3;
        private TextBox CheckedByIDTextBox;
        private Label label4;
        private Button UpdateButton;
        private Button DeleteButton;
    }
}