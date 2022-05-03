
using System;
using System.Windows.Forms;

namespace DB_CP
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GetAllPlayers = new System.Windows.Forms.Button();
            this.UserBox = new System.Windows.Forms.GroupBox();
            this.StatisticsBox = new System.Windows.Forms.GroupBox();
            this.GetStatisticsByID = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.StatisticsID = new System.Windows.Forms.TextBox();
            this.Players = new System.Windows.Forms.GroupBox();
            this.GetPlayerTeamStat = new System.Windows.Forms.Button();
            this.GetPlayersByName = new System.Windows.Forms.Button();
            this.PlayerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GetPlayerByID = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PlayerIDBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GetTeamByName = new System.Windows.Forms.Button();
            this.GetTeamByID = new System.Windows.Forms.Button();
            this.GetAllTeams = new System.Windows.Forms.Button();
            this.TeamName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TeamID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TeamBoxForPlayers = new System.Windows.Forms.GroupBox();
            this.GetPlayersByTeamID = new System.Windows.Forms.Button();
            this.TeamIDBoxForPlayer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AnalyticGroup = new System.Windows.Forms.GroupBox();
            this.DeleteDesiredPlayer = new System.Windows.Forms.Button();
            this.DesiredPlayerID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AddDesiredPlayer = new System.Windows.Forms.Button();
            this.GetAllDesiredPlayers = new System.Windows.Forms.Button();
            this.PlayerIDForDesired = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ManagerGroup = new System.Windows.Forms.GroupBox();
            this.DeleteDesPlayerManager = new System.Windows.Forms.Button();
            this.DesiredPlayerForManager = new System.Windows.Forms.TextBox();
            this.GetDesiredPlayersForManager = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.GetOutcoming = new System.Windows.Forms.Button();
            this.GetIncoming = new System.Windows.Forms.Button();
            this.RejectDeal = new System.Windows.Forms.Button();
            this.ConfirmDeal = new System.Windows.Forms.Button();
            this.DealID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.RequestPlayer = new System.Windows.Forms.Button();
            this.CostForManager = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.PlayerIDForManager = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ModeratorGroup = new System.Windows.Forms.GroupBox();
            this.DeleteUser = new System.Windows.Forms.Button();
            this.UserID = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.AddNewUser = new System.Windows.Forms.Button();
            this.Perm = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.Hash = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.GetAllUsers = new System.Windows.Forms.Button();
            this.GetAllDeals = new System.Windows.Forms.Button();
            this.DeleteDeal = new System.Windows.Forms.Button();
            this.MakeDeal = new System.Windows.Forms.Button();
            this.DealIDForModer = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.UserLogin = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Permission = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.UserBox.SuspendLayout();
            this.StatisticsBox.SuspendLayout();
            this.Players.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TeamBoxForPlayers.SuspendLayout();
            this.AnalyticGroup.SuspendLayout();
            this.ManagerGroup.SuspendLayout();
            this.ModeratorGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(788, 654);
            this.dataGridView1.TabIndex = 1;
            // 
            // GetAllPlayers
            // 
            this.GetAllPlayers.Location = new System.Drawing.Point(6, 22);
            this.GetAllPlayers.Name = "GetAllPlayers";
            this.GetAllPlayers.Size = new System.Drawing.Size(176, 24);
            this.GetAllPlayers.TabIndex = 2;
            this.GetAllPlayers.Text = "Просмотреть всех игроков";
            this.GetAllPlayers.UseVisualStyleBackColor = true;
            this.GetAllPlayers.Click += new System.EventHandler(this.GetAllPlayers_Click);
            // 
            // UserBox
            // 
            this.UserBox.Controls.Add(this.StatisticsBox);
            this.UserBox.Controls.Add(this.Players);
            this.UserBox.Controls.Add(this.groupBox1);
            this.UserBox.Controls.Add(this.TeamBoxForPlayers);
            this.UserBox.Location = new System.Drawing.Point(820, 12);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(209, 654);
            this.UserBox.TabIndex = 3;
            this.UserBox.TabStop = false;
            this.UserBox.Text = "User";
            // 
            // StatisticsBox
            // 
            this.StatisticsBox.Controls.Add(this.GetStatisticsByID);
            this.StatisticsBox.Controls.Add(this.label6);
            this.StatisticsBox.Controls.Add(this.StatisticsID);
            this.StatisticsBox.Location = new System.Drawing.Point(6, 553);
            this.StatisticsBox.Name = "StatisticsBox";
            this.StatisticsBox.Size = new System.Drawing.Size(195, 89);
            this.StatisticsBox.TabIndex = 14;
            this.StatisticsBox.TabStop = false;
            this.StatisticsBox.Text = "Statistics";
            // 
            // GetStatisticsByID
            // 
            this.GetStatisticsByID.Location = new System.Drawing.Point(8, 51);
            this.GetStatisticsByID.Name = "GetStatisticsByID";
            this.GetStatisticsByID.Size = new System.Drawing.Size(175, 27);
            this.GetStatisticsByID.TabIndex = 11;
            this.GetStatisticsByID.Text = "Получить статистику ";
            this.GetStatisticsByID.UseVisualStyleBackColor = true;
            this.GetStatisticsByID.Click += new System.EventHandler(this.GetStatisticsByID_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Statistics ID";
            // 
            // StatisticsID
            // 
            this.StatisticsID.Location = new System.Drawing.Point(83, 22);
            this.StatisticsID.Name = "StatisticsID";
            this.StatisticsID.Size = new System.Drawing.Size(100, 23);
            this.StatisticsID.TabIndex = 11;
            // 
            // Players
            // 
            this.Players.Controls.Add(this.GetPlayerTeamStat);
            this.Players.Controls.Add(this.GetPlayersByName);
            this.Players.Controls.Add(this.PlayerName);
            this.Players.Controls.Add(this.label2);
            this.Players.Controls.Add(this.GetPlayerByID);
            this.Players.Controls.Add(this.label3);
            this.Players.Controls.Add(this.PlayerIDBox);
            this.Players.Controls.Add(this.GetAllPlayers);
            this.Players.Location = new System.Drawing.Point(7, 22);
            this.Players.Name = "Players";
            this.Players.Size = new System.Drawing.Size(194, 241);
            this.Players.TabIndex = 13;
            this.Players.TabStop = false;
            this.Players.Text = "Player";
            // 
            // GetPlayerTeamStat
            // 
            this.GetPlayerTeamStat.Location = new System.Drawing.Point(6, 178);
            this.GetPlayerTeamStat.Name = "GetPlayerTeamStat";
            this.GetPlayerTeamStat.Size = new System.Drawing.Size(176, 47);
            this.GetPlayerTeamStat.TabIndex = 12;
            this.GetPlayerTeamStat.Text = "Получить игроков, их команды и статистики";
            this.GetPlayerTeamStat.UseVisualStyleBackColor = true;
            this.GetPlayerTeamStat.Click += new System.EventHandler(this.GetPlayerTeamStat_Click);
            // 
            // GetPlayersByName
            // 
            this.GetPlayersByName.Location = new System.Drawing.Point(6, 146);
            this.GetPlayersByName.Name = "GetPlayersByName";
            this.GetPlayersByName.Size = new System.Drawing.Size(176, 27);
            this.GetPlayersByName.TabIndex = 10;
            this.GetPlayersByName.Text = "Получить игроков по Name";
            this.GetPlayersByName.UseVisualStyleBackColor = true;
            this.GetPlayersByName.Click += new System.EventHandler(this.GetPlayersByName_Click);
            // 
            // PlayerName
            // 
            this.PlayerName.Location = new System.Drawing.Point(82, 117);
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.Size = new System.Drawing.Size(100, 23);
            this.PlayerName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Player Name";
            // 
            // GetPlayerByID
            // 
            this.GetPlayerByID.Location = new System.Drawing.Point(6, 84);
            this.GetPlayerByID.Name = "GetPlayerByID";
            this.GetPlayerByID.Size = new System.Drawing.Size(176, 27);
            this.GetPlayerByID.TabIndex = 11;
            this.GetPlayerByID.Text = "Получить игрока по ID";
            this.GetPlayerByID.UseVisualStyleBackColor = true;
            this.GetPlayerByID.Click += new System.EventHandler(this.GetPlayerByID_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Player ID";
            // 
            // PlayerIDBox
            // 
            this.PlayerIDBox.Location = new System.Drawing.Point(82, 55);
            this.PlayerIDBox.Name = "PlayerIDBox";
            this.PlayerIDBox.Size = new System.Drawing.Size(100, 23);
            this.PlayerIDBox.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GetTeamByName);
            this.groupBox1.Controls.Add(this.GetTeamByID);
            this.groupBox1.Controls.Add(this.GetAllTeams);
            this.groupBox1.Controls.Add(this.TeamName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TeamID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(7, 361);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 188);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Team";
            // 
            // GetTeamByName
            // 
            this.GetTeamByName.Location = new System.Drawing.Point(6, 146);
            this.GetTeamByName.Name = "GetTeamByName";
            this.GetTeamByName.Size = new System.Drawing.Size(176, 27);
            this.GetTeamByName.TabIndex = 10;
            this.GetTeamByName.Text = "Получить команду по Name";
            this.GetTeamByName.UseVisualStyleBackColor = true;
            this.GetTeamByName.Click += new System.EventHandler(this.GetTeamByName_Click);
            // 
            // GetTeamByID
            // 
            this.GetTeamByID.Location = new System.Drawing.Point(6, 84);
            this.GetTeamByID.Name = "GetTeamByID";
            this.GetTeamByID.Size = new System.Drawing.Size(176, 27);
            this.GetTeamByID.TabIndex = 9;
            this.GetTeamByID.Text = "Получить команду по ID";
            this.GetTeamByID.UseVisualStyleBackColor = true;
            this.GetTeamByID.Click += new System.EventHandler(this.GetTeamByID_Click);
            // 
            // GetAllTeams
            // 
            this.GetAllTeams.Location = new System.Drawing.Point(7, 22);
            this.GetAllTeams.Name = "GetAllTeams";
            this.GetAllTeams.Size = new System.Drawing.Size(176, 24);
            this.GetAllTeams.TabIndex = 12;
            this.GetAllTeams.Text = "Просмотреть все команды";
            this.GetAllTeams.UseVisualStyleBackColor = true;
            this.GetAllTeams.Click += new System.EventHandler(this.GetAllTeams_Click);
            // 
            // TeamName
            // 
            this.TeamName.Location = new System.Drawing.Point(82, 117);
            this.TeamName.Name = "TeamName";
            this.TeamName.Size = new System.Drawing.Size(100, 23);
            this.TeamName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Team Name";
            // 
            // TeamID
            // 
            this.TeamID.Location = new System.Drawing.Point(82, 55);
            this.TeamID.Name = "TeamID";
            this.TeamID.Size = new System.Drawing.Size(100, 23);
            this.TeamID.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Team ID";
            // 
            // TeamBoxForPlayers
            // 
            this.TeamBoxForPlayers.Controls.Add(this.GetPlayersByTeamID);
            this.TeamBoxForPlayers.Controls.Add(this.TeamIDBoxForPlayer);
            this.TeamBoxForPlayers.Controls.Add(this.label1);
            this.TeamBoxForPlayers.Location = new System.Drawing.Point(7, 269);
            this.TeamBoxForPlayers.Name = "TeamBoxForPlayers";
            this.TeamBoxForPlayers.Size = new System.Drawing.Size(194, 86);
            this.TeamBoxForPlayers.TabIndex = 5;
            this.TeamBoxForPlayers.TabStop = false;
            this.TeamBoxForPlayers.Text = "Find players by team";
            // 
            // GetPlayersByTeamID
            // 
            this.GetPlayersByTeamID.Location = new System.Drawing.Point(7, 49);
            this.GetPlayersByTeamID.Name = "GetPlayersByTeamID";
            this.GetPlayersByTeamID.Size = new System.Drawing.Size(176, 27);
            this.GetPlayersByTeamID.TabIndex = 9;
            this.GetPlayersByTeamID.Text = "Получить игроков по ID";
            this.GetPlayersByTeamID.UseVisualStyleBackColor = true;
            this.GetPlayersByTeamID.Click += new System.EventHandler(this.GetTeamByIDForPlayers_Click);
            // 
            // TeamIDBoxForPlayer
            // 
            this.TeamIDBoxForPlayer.Location = new System.Drawing.Point(83, 20);
            this.TeamIDBoxForPlayer.Name = "TeamIDBoxForPlayer";
            this.TeamIDBoxForPlayer.Size = new System.Drawing.Size(100, 23);
            this.TeamIDBoxForPlayer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Team ID";
            // 
            // AnalyticGroup
            // 
            this.AnalyticGroup.Controls.Add(this.DeleteDesiredPlayer);
            this.AnalyticGroup.Controls.Add(this.DesiredPlayerID);
            this.AnalyticGroup.Controls.Add(this.label7);
            this.AnalyticGroup.Controls.Add(this.AddDesiredPlayer);
            this.AnalyticGroup.Controls.Add(this.GetAllDesiredPlayers);
            this.AnalyticGroup.Controls.Add(this.PlayerIDForDesired);
            this.AnalyticGroup.Controls.Add(this.label8);
            this.AnalyticGroup.Location = new System.Drawing.Point(1035, 12);
            this.AnalyticGroup.Name = "AnalyticGroup";
            this.AnalyticGroup.Size = new System.Drawing.Size(221, 179);
            this.AnalyticGroup.TabIndex = 13;
            this.AnalyticGroup.TabStop = false;
            this.AnalyticGroup.Text = "Analytic";
            // 
            // DeleteDesiredPlayer
            // 
            this.DeleteDesiredPlayer.Location = new System.Drawing.Point(7, 146);
            this.DeleteDesiredPlayer.Name = "DeleteDesiredPlayer";
            this.DeleteDesiredPlayer.Size = new System.Drawing.Size(197, 27);
            this.DeleteDesiredPlayer.TabIndex = 10;
            this.DeleteDesiredPlayer.Text = "Удалить желаемого игрока";
            this.DeleteDesiredPlayer.UseVisualStyleBackColor = true;
            this.DeleteDesiredPlayer.Click += new System.EventHandler(this.DeleteDesiredPlayer_Click);
            // 
            // DesiredPlayerID
            // 
            this.DesiredPlayerID.Location = new System.Drawing.Point(104, 117);
            this.DesiredPlayerID.Name = "DesiredPlayerID";
            this.DesiredPlayerID.Size = new System.Drawing.Size(100, 23);
            this.DesiredPlayerID.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "DesiredPlayerID";
            // 
            // AddDesiredPlayer
            // 
            this.AddDesiredPlayer.Location = new System.Drawing.Point(8, 84);
            this.AddDesiredPlayer.Name = "AddDesiredPlayer";
            this.AddDesiredPlayer.Size = new System.Drawing.Size(196, 27);
            this.AddDesiredPlayer.TabIndex = 9;
            this.AddDesiredPlayer.Text = "Добавить желаемого игрока";
            this.AddDesiredPlayer.UseVisualStyleBackColor = true;
            this.AddDesiredPlayer.Click += new System.EventHandler(this.AddDesiredPlayer_Click);
            // 
            // GetAllDesiredPlayers
            // 
            this.GetAllDesiredPlayers.Location = new System.Drawing.Point(7, 22);
            this.GetAllDesiredPlayers.Name = "GetAllDesiredPlayers";
            this.GetAllDesiredPlayers.Size = new System.Drawing.Size(197, 24);
            this.GetAllDesiredPlayers.TabIndex = 12;
            this.GetAllDesiredPlayers.Text = "Получить желаемых игроков";
            this.GetAllDesiredPlayers.UseVisualStyleBackColor = true;
            this.GetAllDesiredPlayers.Click += new System.EventHandler(this.GetAllDesiredPlayers_Click);
            // 
            // PlayerIDForDesired
            // 
            this.PlayerIDForDesired.Location = new System.Drawing.Point(104, 55);
            this.PlayerIDForDesired.Name = "PlayerIDForDesired";
            this.PlayerIDForDesired.Size = new System.Drawing.Size(100, 23);
            this.PlayerIDForDesired.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Player ID";
            // 
            // ManagerGroup
            // 
            this.ManagerGroup.Controls.Add(this.DeleteDesPlayerManager);
            this.ManagerGroup.Controls.Add(this.DesiredPlayerForManager);
            this.ManagerGroup.Controls.Add(this.GetDesiredPlayersForManager);
            this.ManagerGroup.Controls.Add(this.label17);
            this.ManagerGroup.Controls.Add(this.GetOutcoming);
            this.ManagerGroup.Controls.Add(this.GetIncoming);
            this.ManagerGroup.Controls.Add(this.RejectDeal);
            this.ManagerGroup.Controls.Add(this.ConfirmDeal);
            this.ManagerGroup.Controls.Add(this.DealID);
            this.ManagerGroup.Controls.Add(this.label11);
            this.ManagerGroup.Controls.Add(this.RequestPlayer);
            this.ManagerGroup.Controls.Add(this.CostForManager);
            this.ManagerGroup.Controls.Add(this.label10);
            this.ManagerGroup.Controls.Add(this.PlayerIDForManager);
            this.ManagerGroup.Controls.Add(this.label9);
            this.ManagerGroup.Location = new System.Drawing.Point(1035, 191);
            this.ManagerGroup.Name = "ManagerGroup";
            this.ManagerGroup.Size = new System.Drawing.Size(220, 475);
            this.ManagerGroup.TabIndex = 15;
            this.ManagerGroup.TabStop = false;
            this.ManagerGroup.Text = "Manager";
            // 
            // DeleteDesPlayerManager
            // 
            this.DeleteDesPlayerManager.Location = new System.Drawing.Point(8, 374);
            this.DeleteDesPlayerManager.Name = "DeleteDesPlayerManager";
            this.DeleteDesPlayerManager.Size = new System.Drawing.Size(197, 27);
            this.DeleteDesPlayerManager.TabIndex = 15;
            this.DeleteDesPlayerManager.Text = "Удалить желаемого игрока";
            this.DeleteDesPlayerManager.UseVisualStyleBackColor = true;
            this.DeleteDesPlayerManager.Click += new System.EventHandler(this.DeleteDesPlayerManager_Click);
            // 
            // DesiredPlayerForManager
            // 
            this.DesiredPlayerForManager.Location = new System.Drawing.Point(105, 345);
            this.DesiredPlayerForManager.Name = "DesiredPlayerForManager";
            this.DesiredPlayerForManager.Size = new System.Drawing.Size(100, 23);
            this.DesiredPlayerForManager.TabIndex = 17;
            // 
            // GetDesiredPlayersForManager
            // 
            this.GetDesiredPlayersForManager.Cursor = System.Windows.Forms.Cursors.Default;
            this.GetDesiredPlayersForManager.Location = new System.Drawing.Point(6, 19);
            this.GetDesiredPlayersForManager.Name = "GetDesiredPlayersForManager";
            this.GetDesiredPlayersForManager.Size = new System.Drawing.Size(197, 24);
            this.GetDesiredPlayersForManager.TabIndex = 15;
            this.GetDesiredPlayersForManager.Text = "Получить желаемых игроков";
            this.GetDesiredPlayersForManager.UseVisualStyleBackColor = true;
            this.GetDesiredPlayersForManager.Click += new System.EventHandler(this.GetDesiredPlayersForManager_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 348);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 15);
            this.label17.TabIndex = 16;
            this.label17.Text = "DesiredPlayerID";
            // 
            // GetOutcoming
            // 
            this.GetOutcoming.Location = new System.Drawing.Point(7, 292);
            this.GetOutcoming.Name = "GetOutcoming";
            this.GetOutcoming.Size = new System.Drawing.Size(197, 47);
            this.GetOutcoming.TabIndex = 24;
            this.GetOutcoming.Text = "Получить исходящие предложения";
            this.GetOutcoming.UseVisualStyleBackColor = true;
            this.GetOutcoming.Click += new System.EventHandler(this.GetOutcoming_Click);
            // 
            // GetIncoming
            // 
            this.GetIncoming.Location = new System.Drawing.Point(6, 235);
            this.GetIncoming.Name = "GetIncoming";
            this.GetIncoming.Size = new System.Drawing.Size(197, 51);
            this.GetIncoming.TabIndex = 23;
            this.GetIncoming.Text = "Получить входящие предложения";
            this.GetIncoming.UseVisualStyleBackColor = true;
            this.GetIncoming.Click += new System.EventHandler(this.GetIncoming_Click);
            // 
            // RejectDeal
            // 
            this.RejectDeal.Location = new System.Drawing.Point(6, 202);
            this.RejectDeal.Name = "RejectDeal";
            this.RejectDeal.Size = new System.Drawing.Size(197, 27);
            this.RejectDeal.TabIndex = 22;
            this.RejectDeal.Text = "Отклонить сделку";
            this.RejectDeal.UseVisualStyleBackColor = true;
            this.RejectDeal.Click += new System.EventHandler(this.RejectDeal_Click);
            // 
            // ConfirmDeal
            // 
            this.ConfirmDeal.Location = new System.Drawing.Point(6, 169);
            this.ConfirmDeal.Name = "ConfirmDeal";
            this.ConfirmDeal.Size = new System.Drawing.Size(197, 27);
            this.ConfirmDeal.TabIndex = 21;
            this.ConfirmDeal.Text = "Подтвердить сделку";
            this.ConfirmDeal.UseVisualStyleBackColor = true;
            this.ConfirmDeal.Click += new System.EventHandler(this.ConfirmDeal_Click);
            // 
            // DealID
            // 
            this.DealID.Location = new System.Drawing.Point(103, 140);
            this.DealID.Name = "DealID";
            this.DealID.Size = new System.Drawing.Size(100, 23);
            this.DealID.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "Deal ID";
            // 
            // RequestPlayer
            // 
            this.RequestPlayer.Location = new System.Drawing.Point(6, 107);
            this.RequestPlayer.Name = "RequestPlayer";
            this.RequestPlayer.Size = new System.Drawing.Size(197, 27);
            this.RequestPlayer.TabIndex = 15;
            this.RequestPlayer.Text = "Запросить игрока";
            this.RequestPlayer.UseVisualStyleBackColor = true;
            this.RequestPlayer.Click += new System.EventHandler(this.RequestPlayer_Click);
            // 
            // CostForManager
            // 
            this.CostForManager.Location = new System.Drawing.Point(103, 78);
            this.CostForManager.Name = "CostForManager";
            this.CostForManager.Size = new System.Drawing.Size(100, 23);
            this.CostForManager.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 15);
            this.label10.TabIndex = 17;
            this.label10.Text = "Cost";
            // 
            // PlayerIDForManager
            // 
            this.PlayerIDForManager.Location = new System.Drawing.Point(103, 49);
            this.PlayerIDForManager.Name = "PlayerIDForManager";
            this.PlayerIDForManager.Size = new System.Drawing.Size(100, 23);
            this.PlayerIDForManager.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "Player ID";
            // 
            // ModeratorGroup
            // 
            this.ModeratorGroup.Controls.Add(this.DeleteUser);
            this.ModeratorGroup.Controls.Add(this.UserID);
            this.ModeratorGroup.Controls.Add(this.label23);
            this.ModeratorGroup.Controls.Add(this.AddNewUser);
            this.ModeratorGroup.Controls.Add(this.Perm);
            this.ModeratorGroup.Controls.Add(this.label22);
            this.ModeratorGroup.Controls.Add(this.Hash);
            this.ModeratorGroup.Controls.Add(this.label21);
            this.ModeratorGroup.Controls.Add(this.Username);
            this.ModeratorGroup.Controls.Add(this.label20);
            this.ModeratorGroup.Controls.Add(this.GetAllUsers);
            this.ModeratorGroup.Controls.Add(this.GetAllDeals);
            this.ModeratorGroup.Controls.Add(this.DeleteDeal);
            this.ModeratorGroup.Controls.Add(this.MakeDeal);
            this.ModeratorGroup.Controls.Add(this.DealIDForModer);
            this.ModeratorGroup.Controls.Add(this.label14);
            this.ModeratorGroup.Location = new System.Drawing.Point(1262, 12);
            this.ModeratorGroup.Name = "ModeratorGroup";
            this.ModeratorGroup.Size = new System.Drawing.Size(221, 389);
            this.ModeratorGroup.TabIndex = 25;
            this.ModeratorGroup.TabStop = false;
            this.ModeratorGroup.Text = "Moderator";
            // 
            // DeleteUser
            // 
            this.DeleteUser.Location = new System.Drawing.Point(6, 348);
            this.DeleteUser.Name = "DeleteUser";
            this.DeleteUser.Size = new System.Drawing.Size(197, 27);
            this.DeleteUser.TabIndex = 32;
            this.DeleteUser.Text = "Удалить пользователя";
            this.DeleteUser.UseVisualStyleBackColor = true;
            this.DeleteUser.Click += new System.EventHandler(this.DeleteUser_Click);
            // 
            // UserID
            // 
            this.UserID.Location = new System.Drawing.Point(103, 319);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(100, 23);
            this.UserID.TabIndex = 31;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 322);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(44, 15);
            this.label23.TabIndex = 30;
            this.label23.Text = "User ID";
            // 
            // AddNewUser
            // 
            this.AddNewUser.Location = new System.Drawing.Point(7, 289);
            this.AddNewUser.Name = "AddNewUser";
            this.AddNewUser.Size = new System.Drawing.Size(196, 24);
            this.AddNewUser.TabIndex = 29;
            this.AddNewUser.Text = "Добавить нового пользователя";
            this.AddNewUser.UseVisualStyleBackColor = true;
            this.AddNewUser.Click += new System.EventHandler(this.AddNewUser_Click);
            // 
            // Perm
            // 
            this.Perm.Location = new System.Drawing.Point(103, 257);
            this.Perm.Name = "Perm";
            this.Perm.Size = new System.Drawing.Size(100, 23);
            this.Perm.TabIndex = 28;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 260);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 15);
            this.label22.TabIndex = 27;
            this.label22.Text = "Permission";
            // 
            // Hash
            // 
            this.Hash.Location = new System.Drawing.Point(103, 228);
            this.Hash.Name = "Hash";
            this.Hash.Size = new System.Drawing.Size(100, 23);
            this.Hash.TabIndex = 26;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(5, 231);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 15);
            this.label21.TabIndex = 25;
            this.label21.Text = "Password";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(103, 199);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 23);
            this.Username.TabIndex = 24;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 202);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 15);
            this.label20.TabIndex = 23;
            this.label20.Text = "Username";
            // 
            // GetAllUsers
            // 
            this.GetAllUsers.Location = new System.Drawing.Point(7, 149);
            this.GetAllUsers.Name = "GetAllUsers";
            this.GetAllUsers.Size = new System.Drawing.Size(196, 46);
            this.GetAllUsers.TabIndex = 13;
            this.GetAllUsers.Text = "Просмотреть всех пользователей";
            this.GetAllUsers.UseVisualStyleBackColor = true;
            this.GetAllUsers.Click += new System.EventHandler(this.GetAllUsers_Click);
            // 
            // GetAllDeals
            // 
            this.GetAllDeals.Location = new System.Drawing.Point(6, 116);
            this.GetAllDeals.Name = "GetAllDeals";
            this.GetAllDeals.Size = new System.Drawing.Size(197, 27);
            this.GetAllDeals.TabIndex = 22;
            this.GetAllDeals.Text = "Получить все сделки";
            this.GetAllDeals.UseVisualStyleBackColor = true;
            this.GetAllDeals.Click += new System.EventHandler(this.GetAllDeals_Click);
            // 
            // DeleteDeal
            // 
            this.DeleteDeal.Location = new System.Drawing.Point(6, 83);
            this.DeleteDeal.Name = "DeleteDeal";
            this.DeleteDeal.Size = new System.Drawing.Size(197, 27);
            this.DeleteDeal.TabIndex = 21;
            this.DeleteDeal.Text = "Удалить сделку";
            this.DeleteDeal.UseVisualStyleBackColor = true;
            this.DeleteDeal.Click += new System.EventHandler(this.DeleteDeal_Click);
            // 
            // MakeDeal
            // 
            this.MakeDeal.Location = new System.Drawing.Point(6, 50);
            this.MakeDeal.Name = "MakeDeal";
            this.MakeDeal.Size = new System.Drawing.Size(197, 27);
            this.MakeDeal.TabIndex = 15;
            this.MakeDeal.Text = "Провести сделку";
            this.MakeDeal.UseVisualStyleBackColor = true;
            this.MakeDeal.Click += new System.EventHandler(this.MakeDeal_Click);
            // 
            // DealIDForModer
            // 
            this.DealIDForModer.Location = new System.Drawing.Point(103, 21);
            this.DealIDForModer.Name = "DealIDForModer";
            this.DealIDForModer.Size = new System.Drawing.Size(100, 23);
            this.DealIDForModer.TabIndex = 16;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "DealID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1262, 405);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(172, 15);
            this.label12.TabIndex = 26;
            this.label12.Text = "Информация о пользователе:";
            // 
            // UserLogin
            // 
            this.UserLogin.AutoSize = true;
            this.UserLogin.Location = new System.Drawing.Point(1327, 426);
            this.UserLogin.Name = "UserLogin";
            this.UserLogin.Size = new System.Drawing.Size(37, 15);
            this.UserLogin.TabIndex = 27;
            this.UserLogin.Text = "Login";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1262, 426);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 15);
            this.label13.TabIndex = 28;
            this.label13.Text = "Login:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1262, 449);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 15);
            this.label15.TabIndex = 29;
            this.label15.Text = "Password:";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(1328, 449);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(39, 15);
            this.Password.TabIndex = 30;
            this.Password.Text = "Passw";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1263, 473);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 15);
            this.label16.TabIndex = 31;
            this.label16.Text = "Permission:";
            // 
            // Permission
            // 
            this.Permission.AutoSize = true;
            this.Permission.Location = new System.Drawing.Point(1328, 470);
            this.Permission.Name = "Permission";
            this.Permission.Size = new System.Drawing.Size(65, 15);
            this.Permission.TabIndex = 32;
            this.Permission.Text = "Permission";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1263, 494);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(18, 15);
            this.label18.TabIndex = 33;
            this.label18.Text = "ID";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.BackColor = System.Drawing.SystemColors.Control;
            this.Status.Location = new System.Drawing.Point(1328, 494);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(39, 15);
            this.Status.TabIndex = 34;
            this.Status.Text = "Status";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1262, 470);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 15);
            this.label19.TabIndex = 31;
            this.label19.Text = "Permission:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1488, 673);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.Permission);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.UserLogin);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ModeratorGroup);
            this.Controls.Add(this.AnalyticGroup);
            this.Controls.Add(this.ManagerGroup);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.UserBox.ResumeLayout(false);
            this.StatisticsBox.ResumeLayout(false);
            this.StatisticsBox.PerformLayout();
            this.Players.ResumeLayout(false);
            this.Players.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TeamBoxForPlayers.ResumeLayout(false);
            this.TeamBoxForPlayers.PerformLayout();
            this.AnalyticGroup.ResumeLayout(false);
            this.AnalyticGroup.PerformLayout();
            this.ManagerGroup.ResumeLayout(false);
            this.ManagerGroup.PerformLayout();
            this.ModeratorGroup.ResumeLayout(false);
            this.ModeratorGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void CloseForm(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button GetAllPlayers;
        private System.Windows.Forms.GroupBox UserBox;
        private System.Windows.Forms.GroupBox StatisticsBox;
        private System.Windows.Forms.Button GetStatistic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox StatisticsID;
        private System.Windows.Forms.GroupBox Players;
        private System.Windows.Forms.Button GetPlayerByID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PlayerIDBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button GetTeamByName;
        private System.Windows.Forms.Button GetTeamByID;
        private System.Windows.Forms.Button GetAllTeams;
        private System.Windows.Forms.TextBox TeamName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TeamID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox TeamBoxForPlayers;
        private System.Windows.Forms.Button GetPlayersByName;
        private System.Windows.Forms.Button GetPlayersByTeamID;
        private System.Windows.Forms.TextBox PlayerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TeamIDBoxForPlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox AnalyticGroup;
        private System.Windows.Forms.Button DeleteDesiredPlayer;
        private System.Windows.Forms.Button AddDesiredPlayer;
        private System.Windows.Forms.Button GetAllDesiredPlayers;
        private System.Windows.Forms.Button GetStatisticsByID;
        private System.Windows.Forms.TextBox PlayerIDForDesired;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DesiredPlayerID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox ManagerGroup;
        private System.Windows.Forms.TextBox PlayerIDForManager;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DealID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button RequestPlayer;
        private System.Windows.Forms.TextBox CostForManager;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button ConfirmDeal;
        private System.Windows.Forms.Button RejectDeal;
        private System.Windows.Forms.Button GetOutcoming;
        private System.Windows.Forms.Button GetIncoming;
        private System.Windows.Forms.GroupBox ModeratorGroup;
        private System.Windows.Forms.Button DeleteDeal;
        private System.Windows.Forms.Button MakeDeal;
        private System.Windows.Forms.TextBox DealIDForModer;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button GetAllDeals;
        private Button GetPlayerTeamStat;
        private Button GetDesiredPlayersForManager;
        private Label label12;
        private Label UserLogin;
        private Label label13;
        private Label label15;
        private Label Password;
        private Label label16;
        private Label Permission;
        private Label label18;
        private Label Status;
        private Button DeleteDesPlayerManager;
        private TextBox DesiredPlayerForManager;
        private Label label17;
        private Label label19;
        private Button DeleteUser;
        private TextBox UserID;
        private Label label23;
        private Button AddNewUser;
        private TextBox Perm;
        private Label label22;
        private TextBox Hash;
        private Label label21;
        private TextBox Username;
        private Label label20;
        private Button GetAllUsers;
    }
}

