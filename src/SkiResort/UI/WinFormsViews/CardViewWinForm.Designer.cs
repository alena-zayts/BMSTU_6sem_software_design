namespace UI.WinFormsViews
{
    partial class CardViewWinForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.TimePicker = new System.Windows.Forms.DateTimePicker();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.GetCardsButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TypeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.СardsDataGridView = new System.Windows.Forms.DataGridView();
            this.cardIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivationTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetCardButton = new System.Windows.Forms.Button();
            this.CardIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.СardsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 30);
            this.label5.TabIndex = 50;
            this.label5.Text = "Время активации";
            // 
            // TimePicker
            // 
            this.TimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TimePicker.Location = new System.Drawing.Point(249, 309);
            this.TimePicker.Name = "TimePicker";
            this.TimePicker.Size = new System.Drawing.Size(229, 35);
            this.TimePicker.TabIndex = 49;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(270, 445);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(180, 42);
            this.DeleteButton.TabIndex = 48;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(24, 445);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(180, 42);
            this.AddButton.TabIndex = 47;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(24, 373);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(180, 42);
            this.UpdateButton.TabIndex = 46;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // GetCardsButton
            // 
            this.GetCardsButton.Location = new System.Drawing.Point(547, 489);
            this.GetCardsButton.Name = "GetCardsButton";
            this.GetCardsButton.Size = new System.Drawing.Size(408, 42);
            this.GetCardsButton.TabIndex = 45;
            this.GetCardsButton.Text = "Посмотреть все карты";
            this.GetCardsButton.UseVisualStyleBackColor = true;
            this.GetCardsButton.Click += new System.EventHandler(this.GetCardsButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 30);
            this.label4.TabIndex = 44;
            this.label4.Text = "Дата активации";
            // 
            // TypeTextBox
            // 
            this.TypeTextBox.Location = new System.Drawing.Point(249, 192);
            this.TypeTextBox.Name = "TypeTextBox";
            this.TypeTextBox.Size = new System.Drawing.Size(152, 35);
            this.TypeTextBox.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 30);
            this.label2.TabIndex = 40;
            this.label2.Text = "Тип карты";
            // 
            // DatePicker
            // 
            this.DatePicker.Location = new System.Drawing.Point(249, 249);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(229, 35);
            this.DatePicker.TabIndex = 39;
            // 
            // СardsDataGridView
            // 
            this.СardsDataGridView.AllowUserToOrderColumns = true;
            this.СardsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.СardsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cardIDColumn,
            this.Type,
            this.ActivationTimeColumn});
            this.СardsDataGridView.Location = new System.Drawing.Point(547, 28);
            this.СardsDataGridView.Name = "СardsDataGridView";
            this.СardsDataGridView.RowHeadersWidth = 72;
            this.СardsDataGridView.RowTemplate.Height = 37;
            this.СardsDataGridView.Size = new System.Drawing.Size(874, 438);
            this.СardsDataGridView.TabIndex = 35;
            // 
            // cardIDColumn
            // 
            this.cardIDColumn.HeaderText = "ID карты";
            this.cardIDColumn.MinimumWidth = 9;
            this.cardIDColumn.Name = "cardIDColumn";
            this.cardIDColumn.Width = 175;
            // 
            // Type
            // 
            this.Type.HeaderText = "Тип карты";
            this.Type.MinimumWidth = 9;
            this.Type.Name = "Type";
            this.Type.Width = 220;
            // 
            // ActivationTimeColumn
            // 
            this.ActivationTimeColumn.HeaderText = "Время активации";
            this.ActivationTimeColumn.MinimumWidth = 9;
            this.ActivationTimeColumn.Name = "ActivationTimeColumn";
            this.ActivationTimeColumn.Width = 400;
            // 
            // GetCardButton
            // 
            this.GetCardButton.Location = new System.Drawing.Point(28, 87);
            this.GetCardButton.Name = "GetCardButton";
            this.GetCardButton.Size = new System.Drawing.Size(408, 42);
            this.GetCardButton.TabIndex = 38;
            this.GetCardButton.Text = "Получить информацию";
            this.GetCardButton.UseVisualStyleBackColor = true;
            this.GetCardButton.Click += new System.EventHandler(this.GetCardButton_Click);
            // 
            // CardIDTextBox
            // 
            this.CardIDTextBox.Location = new System.Drawing.Point(249, 28);
            this.CardIDTextBox.Name = "CardIDTextBox";
            this.CardIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.CardIDTextBox.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 30);
            this.label1.TabIndex = 36;
            this.label1.Text = "ID карты";
            // 
            // CardViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 548);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TimePicker);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.GetCardsButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TypeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.СardsDataGridView);
            this.Controls.Add(this.GetCardButton);
            this.Controls.Add(this.CardIDTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CardViewWinForm";
            this.Text = "Карты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardViewWinForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.СardsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label5;
        private DateTimePicker TimePicker;
        private Button DeleteButton;
        private Button AddButton;
        private Button UpdateButton;
        private Button GetCardsButton;
        private Label label4;
        private TextBox TypeTextBox;
        private Label label2;
        private DateTimePicker DatePicker;
        private DataGridView СardsDataGridView;
        private DataGridViewTextBoxColumn cardIDColumn;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn ActivationTimeColumn;
        private Button GetCardButton;
        private TextBox CardIDTextBox;
        private Label label1;
    }
}