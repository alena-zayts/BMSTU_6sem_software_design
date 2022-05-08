namespace UI.WinFormsViews
{
    partial class TurnstileViewWinForm
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
            this.GetTurnstilesButton = new System.Windows.Forms.Button();
            this.IsOpenTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LiftIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TurnstilesDataGridView = new System.Windows.Forms.DataGridView();
            this.TurnstileIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiftIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOpenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetTurnstileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TurnstileIDTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TurnstilesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(265, 385);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(180, 42);
            this.DeleteButton.TabIndex = 48;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(19, 385);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(180, 42);
            this.AddButton.TabIndex = 47;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(19, 313);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(180, 42);
            this.UpdateButton.TabIndex = 46;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // GetTurnstilesButton
            // 
            this.GetTurnstilesButton.Location = new System.Drawing.Point(482, 454);
            this.GetTurnstilesButton.Name = "GetTurnstilesButton";
            this.GetTurnstilesButton.Size = new System.Drawing.Size(408, 42);
            this.GetTurnstilesButton.TabIndex = 45;
            this.GetTurnstilesButton.Text = "Посмотреть все турникеты";
            this.GetTurnstilesButton.UseVisualStyleBackColor = true;
            this.GetTurnstilesButton.Click += new System.EventHandler(this.GetTurnstilesButton_Click);
            // 
            // IsOpenTextBox
            // 
            this.IsOpenTextBox.Location = new System.Drawing.Point(248, 253);
            this.IsOpenTextBox.Name = "IsOpenTextBox";
            this.IsOpenTextBox.Size = new System.Drawing.Size(152, 35);
            this.IsOpenTextBox.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 30);
            this.label3.TabIndex = 42;
            this.label3.Text = "Открыт";
            // 
            // LiftIDTextBox
            // 
            this.LiftIDTextBox.Location = new System.Drawing.Point(244, 190);
            this.LiftIDTextBox.Name = "LiftIDTextBox";
            this.LiftIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.LiftIDTextBox.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 30);
            this.label2.TabIndex = 40;
            this.label2.Text = "ID подъемника";
            // 
            // TurnstilesDataGridView
            // 
            this.TurnstilesDataGridView.AllowUserToOrderColumns = true;
            this.TurnstilesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TurnstilesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TurnstileIDColumn,
            this.LiftIDColumn,
            this.IsOpenColumn});
            this.TurnstilesDataGridView.Location = new System.Drawing.Point(482, 26);
            this.TurnstilesDataGridView.Name = "TurnstilesDataGridView";
            this.TurnstilesDataGridView.RowHeadersWidth = 72;
            this.TurnstilesDataGridView.RowTemplate.Height = 37;
            this.TurnstilesDataGridView.Size = new System.Drawing.Size(601, 412);
            this.TurnstilesDataGridView.TabIndex = 35;
            // 
            // TurnstileIDColumn
            // 
            this.TurnstileIDColumn.HeaderText = "ID турникета";
            this.TurnstileIDColumn.MinimumWidth = 9;
            this.TurnstileIDColumn.Name = "TurnstileIDColumn";
            this.TurnstileIDColumn.Width = 175;
            // 
            // LiftIDColumn
            // 
            this.LiftIDColumn.HeaderText = "ID подъемника";
            this.LiftIDColumn.MinimumWidth = 9;
            this.LiftIDColumn.Name = "LiftIDColumn";
            this.LiftIDColumn.Width = 175;
            // 
            // IsOpenColumn
            // 
            this.IsOpenColumn.HeaderText = "Открыт";
            this.IsOpenColumn.MinimumWidth = 9;
            this.IsOpenColumn.Name = "IsOpenColumn";
            this.IsOpenColumn.Width = 175;
            // 
            // GetTurnstileButton
            // 
            this.GetTurnstileButton.Location = new System.Drawing.Point(23, 85);
            this.GetTurnstileButton.Name = "GetTurnstileButton";
            this.GetTurnstileButton.Size = new System.Drawing.Size(408, 42);
            this.GetTurnstileButton.TabIndex = 38;
            this.GetTurnstileButton.Text = "Получить информацию";
            this.GetTurnstileButton.UseVisualStyleBackColor = true;
            this.GetTurnstileButton.Click += new System.EventHandler(this.GetTurnstileButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 30);
            this.label1.TabIndex = 36;
            this.label1.Text = "ID турникета";
            // 
            // TurnstileIDTextBox
            // 
            this.TurnstileIDTextBox.Location = new System.Drawing.Point(244, 26);
            this.TurnstileIDTextBox.Name = "TurnstileIDTextBox";
            this.TurnstileIDTextBox.Size = new System.Drawing.Size(152, 35);
            this.TurnstileIDTextBox.TabIndex = 37;
            // 
            // TurnstileViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 508);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.GetTurnstilesButton);
            this.Controls.Add(this.IsOpenTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LiftIDTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TurnstilesDataGridView);
            this.Controls.Add(this.GetTurnstileButton);
            this.Controls.Add(this.TurnstileIDTextBox);
            this.Controls.Add(this.label1);
            this.Name = "TurnstileViewWinForm";
            this.Text = "Турникеты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TurnstileViewWinForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.TurnstilesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button DeleteButton;
        private Button AddButton;
        private Button UpdateButton;
        private Button GetTurnstilesButton;
        private TextBox IsOpenTextBox;
        private Label label3;
        private TextBox LiftIDTextBox;
        private Label label2;
        private DataGridView TurnstilesDataGridView;
        private Button GetTurnstileButton;
        private Label label1;
        private TextBox TurnstileIDTextBox;
        private DataGridViewTextBoxColumn TurnstileIDColumn;
        private DataGridViewTextBoxColumn LiftIDColumn;
        private DataGridViewTextBoxColumn IsOpenColumn;
    }
}