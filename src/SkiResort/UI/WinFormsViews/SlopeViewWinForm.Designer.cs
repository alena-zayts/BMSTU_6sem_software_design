namespace UI.WinFormsViews
{
    partial class SlopeViewWinForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.SlopesDataGridView = new System.Windows.Forms.DataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOpenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DifficultyLevelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectedLiftsColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.GetInfoButton = new System.Windows.Forms.Button();
            this.IsOpenTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DifficultyLevelTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddConnectedLiftButton = new System.Windows.Forms.Button();
            this.LiftNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DeleteConnectedLiftButton = new System.Windows.Forms.Button();
            this.GetInfosButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SlopesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // SlopesDataGridView
            // 
            this.SlopesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SlopesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.NameColumn,
            this.IsOpenColumn,
            this.DifficultyLevelColumn,
            this.ConnectedLiftsColumns});
            this.SlopesDataGridView.Location = new System.Drawing.Point(494, 37);
            this.SlopesDataGridView.Name = "SlopesDataGridView";
            this.SlopesDataGridView.RowHeadersWidth = 72;
            this.SlopesDataGridView.RowTemplate.Height = 37;
            this.SlopesDataGridView.Size = new System.Drawing.Size(960, 438);
            this.SlopesDataGridView.TabIndex = 1;
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
            this.IsOpenColumn.HeaderText = "Открыта";
            this.IsOpenColumn.MinimumWidth = 9;
            this.IsOpenColumn.Name = "IsOpenColumn";
            this.IsOpenColumn.Width = 175;
            // 
            // DifficultyLevelColumn
            // 
            this.DifficultyLevelColumn.HeaderText = "Уровень сложности";
            this.DifficultyLevelColumn.MinimumWidth = 9;
            this.DifficultyLevelColumn.Name = "DifficultyLevelColumn";
            this.DifficultyLevelColumn.Width = 175;
            // 
            // ConnectedLiftsColumns
            // 
            this.ConnectedLiftsColumns.HeaderText = "На чем добраться";
            this.ConnectedLiftsColumns.MinimumWidth = 9;
            this.ConnectedLiftsColumns.Name = "ConnectedLiftsColumns";
            this.ConnectedLiftsColumns.Width = 175;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(258, 50);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(152, 35);
            this.NameTextBox.TabIndex = 2;
            // 
            // GetInfoButton
            // 
            this.GetInfoButton.Location = new System.Drawing.Point(37, 109);
            this.GetInfoButton.Name = "GetInfoButton";
            this.GetInfoButton.Size = new System.Drawing.Size(292, 42);
            this.GetInfoButton.TabIndex = 3;
            this.GetInfoButton.Text = "Получить информацию";
            this.GetInfoButton.UseVisualStyleBackColor = true;
            this.GetInfoButton.Click += new System.EventHandler(this.GetInfoButton_Click);
            // 
            // IsOpenTextBox
            // 
            this.IsOpenTextBox.Location = new System.Drawing.Point(258, 230);
            this.IsOpenTextBox.Name = "IsOpenTextBox";
            this.IsOpenTextBox.Size = new System.Drawing.Size(152, 35);
            this.IsOpenTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "Открыта";
            // 
            // DifficultyLevelTextBox
            // 
            this.DifficultyLevelTextBox.Location = new System.Drawing.Point(258, 291);
            this.DifficultyLevelTextBox.Name = "DifficultyLevelTextBox";
            this.DifficultyLevelTextBox.Size = new System.Drawing.Size(152, 35);
            this.DifficultyLevelTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "Уровень сложности";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(37, 358);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(171, 42);
            this.UpdateButton.TabIndex = 8;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(37, 417);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(171, 42);
            this.AddButton.TabIndex = 9;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(239, 417);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(171, 42);
            this.DeleteButton.TabIndex = 10;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddConnectedLiftButton
            // 
            this.AddConnectedLiftButton.Location = new System.Drawing.Point(33, 584);
            this.AddConnectedLiftButton.Name = "AddConnectedLiftButton";
            this.AddConnectedLiftButton.Size = new System.Drawing.Size(204, 42);
            this.AddConnectedLiftButton.TabIndex = 13;
            this.AddConnectedLiftButton.Text = "Добавить связь";
            this.AddConnectedLiftButton.UseVisualStyleBackColor = true;
            this.AddConnectedLiftButton.Click += new System.EventHandler(this.AddConnectedLiftButton_Click);
            // 
            // LiftNameTextBox
            // 
            this.LiftNameTextBox.Location = new System.Drawing.Point(310, 530);
            this.LiftNameTextBox.Name = "LiftNameTextBox";
            this.LiftNameTextBox.Size = new System.Drawing.Size(152, 35);
            this.LiftNameTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 530);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "Название подъемника";
            // 
            // DeleteConnectedLiftButton
            // 
            this.DeleteConnectedLiftButton.Location = new System.Drawing.Point(258, 584);
            this.DeleteConnectedLiftButton.Name = "DeleteConnectedLiftButton";
            this.DeleteConnectedLiftButton.Size = new System.Drawing.Size(213, 42);
            this.DeleteConnectedLiftButton.TabIndex = 14;
            this.DeleteConnectedLiftButton.Text = "Удалить связь";
            this.DeleteConnectedLiftButton.UseVisualStyleBackColor = true;
            this.DeleteConnectedLiftButton.Click += new System.EventHandler(this.DeleteConnectedLiftButton_Click);
            // 
            // GetInfosButton
            // 
            this.GetInfosButton.Location = new System.Drawing.Point(494, 498);
            this.GetInfosButton.Name = "GetInfosButton";
            this.GetInfosButton.Size = new System.Drawing.Size(292, 42);
            this.GetInfosButton.TabIndex = 15;
            this.GetInfosButton.Text = "Посмотреть все спуски";
            this.GetInfosButton.UseVisualStyleBackColor = true;
            this.GetInfosButton.Click += new System.EventHandler(this.GetSlopesInfoButton_Click);
            // 
            // SlopeViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 638);
            this.Controls.Add(this.GetInfosButton);
            this.Controls.Add(this.DeleteConnectedLiftButton);
            this.Controls.Add(this.AddConnectedLiftButton);
            this.Controls.Add(this.LiftNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.DifficultyLevelTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IsOpenTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GetInfoButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.SlopesDataGridView);
            this.Controls.Add(this.label1);
            this.Name = "SlopeViewWinForm";
            this.Text = "Спуски";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SlopeView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.SlopesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private DataGridView SlopesDataGridView;
        private TextBox NameTextBox;
        private Button GetInfoButton;
        private TextBox IsOpenTextBox;
        private Label label2;
        private TextBox DifficultyLevelTextBox;
        private Label label3;
        private Button UpdateButton;
        private Button AddButton;
        private Button DeleteButton;
        private Button AddConnectedLiftButton;
        private TextBox LiftNameTextBox;
        private Label label4;
        private Button DeleteConnectedLiftButton;
        private Button GetInfosButton;
        private DataGridViewTextBoxColumn IDColumn;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn IsOpenColumn;
        private DataGridViewTextBoxColumn DifficultyLevelColumn;
        private DataGridViewTextBoxColumn ConnectedLiftsColumns;
    }
}