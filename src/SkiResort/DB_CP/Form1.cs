using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Serilog.Core;
using Microsoft.Extensions.Logging;

namespace DB_CP
{
    public partial class Form1 : Form
    {
        private readonly ILogger<Form1> _logger;
        private readonly AnalyticController _analytic;
        private readonly ManagerController _manager;
        private readonly ModeratorController _moderator;
        private readonly UserController _userController;
        private readonly Userinfo _user;
        public Form1(ILogger<Form1> logger, Userinfo currentUser, AnalyticController analytic, ManagerController manager, ModeratorController moderator, UserController userController)
        {
            _analytic = analytic;
            _manager = manager;
            _moderator = moderator;
            _userController = userController;
            _logger = logger;
            _user = currentUser;
            InitializeComponent();
            CheckPerms();
        }
        private void CheckPerms()
        {
            UserLogin.Text = _user.Login;
            Password.Text = _user.Hash;
            Status.Text = _user.Id.ToString();
            if (_user.Permission == (int)Permissions.Analytic)
            {
                ManagerGroup.Enabled = false;
                ModeratorGroup.Enabled = false;
                Permission.Text = "Аналитик";
            }
            else if (_user.Permission == (int)Permissions.Manager)
            {
                AnalyticGroup.Enabled = false;
                ModeratorGroup.Enabled = false;
                Permission.Text = "Менеджер";
            }
            else
            {
                AnalyticGroup.Enabled = false;
                ManagerGroup.Enabled = false;
                Permission.Text = "Модератор";
            }
        }
        private void AddColumnsTeam()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("TeamID", "ID команды");
            dataGridView1.Columns.Add("ManagamanetID", "ID менеджемнта");
            dataGridView1.Columns.Add("Name", "Имя команды");
            dataGridView1.Columns.Add("Headcoach", "Главный тренер");
            dataGridView1.Columns.Add("Country", "Страна");
            dataGridView1.Columns.Add("Stadium", "Стадион");
            dataGridView1.Columns.Add("Balance", "Баланс");
        }
        private void AddColumnsStatistic()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("StatisticsId", "ID статистики");
            dataGridView1.Columns.Add("Numberofwashers", "Количество шайб");
            dataGridView1.Columns.Add("Averagegametime", "Среднее игровое время");
        }
        private void AddColumnsPlayer()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("PlayerId", "ID игрока");
            dataGridView1.Columns.Add("TeamId", "ID команды");
            dataGridView1.Columns.Add("Statistics", "ID статистики");
            dataGridView1.Columns.Add("Name", "Имя игрока");
            dataGridView1.Columns.Add("Position", "Позиция");
            dataGridView1.Columns.Add("Weight", "Вес");
            dataGridView1.Columns.Add("Height", "Рост");
            dataGridView1.Columns.Add("Number", "Номер");
            dataGridView1.Columns.Add("Age", "Возраст");
            dataGridView1.Columns.Add("Country", "Страна");
            dataGridView1.Columns.Add("Cost", "Цена");
        }
        private void AddColumnsDesiredPlayer()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Id", "ID желаемого игрока");
            dataGridView1.Columns.Add("PlayerId", "ID игрока");
            dataGridView1.Columns.Add("TeamId", "ID команды");
            dataGridView1.Columns.Add("ManagementID", "ID менеджемнта");
        }
        private void AddColumnsAvailableDeal()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Id", "ID доступной сделки");
            dataGridView1.Columns.Add("PlayerID", "ID игрока");
            dataGridView1.Columns.Add("ToManagementId", "Какому менеджменту");
            dataGridView1.Columns.Add("FromManagementId", "От какого менеджмента");
            dataGridView1.Columns.Add("Cost", "Цена");
            dataGridView1.Columns.Add("Status", "Статус");
        }
        private void AddColumnsPlayerTeamStat()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("playerid", "ID Игрока");
            dataGridView1.Columns.Add("player", "Имя игрока");
            dataGridView1.Columns.Add("team", "Команда игрока");
            dataGridView1.Columns.Add("washers", "Количество шайб");
            dataGridView1.Columns.Add("gametime", "Игровое время");
        }
        private void AddColumnsUserInfo()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("id", "ID Пользователя");
            dataGridView1.Columns.Add("login", "Логин пользователя");
            dataGridView1.Columns.Add("hash", "Пароль пользователя");
            dataGridView1.Columns.Add("permission", "Разрешение");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Application.Exit();
        }
        private void GetAllPlayers_Click(object sender, EventArgs e)
        {
            AddColumnsPlayer();
            List<Player> players = _userController.GetAllPlayers();
            if (players != null)
            {
                foreach (Player player in players)
                {
                    dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                        player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                }
            }
            else
            {
                MessageBox.Show("Игроки не найдены");
            }
        }
        private void GetPlayerByID_Click(object sender, EventArgs e)
        {
            if (PlayerIDBox.Text != "")
            {
                AddColumnsPlayer();
                Player player = _userController.FindPlayerByID(Convert.ToInt32(PlayerIDBox.Text));
                if (player != null)
                {
                    dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                        player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                }
                else
                {
                    MessageBox.Show("Игрок не найден");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id игрока");
            }
        }
        private void GetTeamByIDForPlayers_Click(object sender, EventArgs e)
        {
            if (TeamIDBoxForPlayer.Text != "")
            {
                AddColumnsPlayer();
                List<Player> players = _userController.GetPlayersByTeam(Convert.ToInt32(TeamIDBoxForPlayer.Text));
                if (players != null)
                {
                    foreach (Player player in players)
                    {
                        dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                            player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                    }
                }
                else
                {
                    MessageBox.Show("Игроки не найдены");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id команды");
            }
        }
        private void GetPlayersByName_Click(object sender, EventArgs e)
        {
            if (PlayerName.Text != "")
            {
                AddColumnsPlayer();
                Player player = _userController.FindPlayerByName(PlayerName.Text);
                if (player != null)
                {
                    dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                        player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                }
                else
                {
                    MessageBox.Show("Игроки не найден");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Name игрока");
            }
        }
        private void GetAllTeams_Click(object sender, EventArgs e)
        {
            AddColumnsTeam();
            List<Team> teams = _userController.GetAllTeams();
            if (teams != null)
            {
                foreach (Team team in teams)
                {
                    dataGridView1.Rows.Add(team.Teamid, team.Managementid, team.Name, team.Headcoach, team.Country, team.Stadium, team.Balance);
                }
            }
            else
            {
                MessageBox.Show("Команды не найдены");
            }
        }
        private void GetTeamByID_Click(object sender, EventArgs e)
        {
            if (TeamID.Text != "")
            {
                AddColumnsTeam();
                Team team = _userController.FindTeamByID(Convert.ToInt32(TeamID.Text));
                if (team != null)
                {
                    dataGridView1.Rows.Add(team.Teamid, team.Managementid, team.Name, team.Headcoach, team.Country, team.Stadium, team.Balance);
                }
                else
                {
                    MessageBox.Show("Команда не найдена");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id команды");
            }
        }
        private void GetTeamByName_Click(object sender, EventArgs e)
        {
            if (TeamName.Text != "")
            {
                AddColumnsTeam();
                Team team = _userController.FindTeamByName(TeamName.Text);
                if (team != null)
                {
                    dataGridView1.Rows.Add(team.Teamid, team.Managementid, team.Name, team.Headcoach, team.Country, team.Stadium, team.Balance);
                }
                else
                {
                    MessageBox.Show("Команда не найдена");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Name команды");
            }
        }
        private void GetStatisticsByID_Click(object sender, EventArgs e)
        {
            if (StatisticsID.Text != "")
            {
                AddColumnsStatistic();
                Statistic stat = _userController.GetPlayerStatistic(Convert.ToInt32(StatisticsID.Text));
                if (stat != null)
                {
                    dataGridView1.Rows.Add(stat.Statisticsid, stat.Numberofwashers, stat.Averagegametime);
                }
                else
                {
                    MessageBox.Show("Статистика не найдена");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id статистики");
            }
        }
        private void GetAllDesiredPlayers_Click(object sender, EventArgs e)
        {
            AddColumnsDesiredPlayer();
            List<Desiredplayer> players = _analytic.GetAllDesiredPlayers();
            if (players != null)
            {
                foreach (Desiredplayer player in players)
                {
                    dataGridView1.Rows.Add(player.Id, player.Playerid, player.Teamid, player.Managementid);
                }
            }
            else
            {
                MessageBox.Show("Игроки не найдены");
            }
        }
        private void AddDesiredPlayer_Click(object sender, EventArgs e)
        {
            if (PlayerIDForDesired.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (_analytic.AddDesiredPlayer(Convert.ToInt32(PlayerIDForDesired.Text)) == false)
            {
                MessageBox.Show("Не удалось добавить игрока");
                return;
            }
            GetAllDesiredPlayers_Click(sender, e);
        }
        private void DeleteDesiredPlayer_Click(object sender, EventArgs e)
        {
            if (DesiredPlayerID.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (_analytic.DeleteDesiredPlayer(Convert.ToInt32(DesiredPlayerID.Text)) == false)
            {
                MessageBox.Show("Не удалось удалить игрока");
                return;
            }
            GetAllDesiredPlayers_Click(sender, e);
        }
        private void RequestPlayer_Click(object sender, EventArgs e)
        {
            if (PlayerIDForManager.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (CostForManager.Text == "")
            {
                MessageBox.Show("Не указана цена");
                return;
            }
            if (_manager.RequestPlayer(Convert.ToInt32(PlayerIDForManager.Text), Convert.ToInt32(CostForManager.Text)) == false) 
            {
                MessageBox.Show("Не удалось предложить игрока");
                return;
            }
        }
        private void ConfirmDeal_Click(object sender, EventArgs e)
        {
            if (DealID.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
                return;
            }
            if (_manager.ConfirmDeal(Convert.ToInt32(DealID.Text)) == false)
            {
                MessageBox.Show("Не удалось подтвердить сделку");
                return;
            }
            else
            {
                MessageBox.Show("Сделка подтверждена");
            }
        }
        private void RejectDeal_Click(object sender, EventArgs e)
        {
            if (DealID.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
                return;
            }
            if (_manager.RejectDeal(Convert.ToInt32(DealID.Text)) == false)
            {
                MessageBox.Show("Не удалось отклонить сделку");
                return;
            }
        }
        private void GetIncoming_Click(object sender, EventArgs e)
        {
            AddColumnsAvailableDeal();
            List<Availabledeal> deals = _manager.GetIncomingDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    dataGridView1.Rows.Add(deal.Id, deal.Playerid, deal.Tomanagementid, deal.Frommanagementid, deal.Cost, deal.Status);
                }
            }   
            else
            {
                MessageBox.Show("Сделки не найдены");
            }
        }
        private void GetOutcoming_Click(object sender, EventArgs e)
        {
            AddColumnsAvailableDeal();
            List<Availabledeal> deals = _manager.GetOutgoaingDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    dataGridView1.Rows.Add(deal.Id, deal.Playerid, deal.Tomanagementid, deal.Frommanagementid, deal.Cost, deal.Status);
                }
            }
            else
            {
                MessageBox.Show("Сделки не найдены");
            }
        }
        private void MakeDeal_Click(object sender, EventArgs e)
        {
            if (DealIDForModer.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
            }
            else
            {
                if (!_moderator.MakeDeal(Convert.ToInt32(DealIDForModer.Text)))
                {
                    MessageBox.Show("Не удалось провести сделку");
                }
                else
                {
                    MessageBox.Show("Сделка проведена");
                    _moderator.DeleteDeal(Convert.ToInt32(DealIDForModer.Text));
                }
            }
        }
        private void DeleteDeal_Click(object sender, EventArgs e)
        {
            if (DealIDForModer.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
            }
            else
            {
                if (!_moderator.DeleteDeal(Convert.ToInt32(DealIDForModer.Text)))
                {
                    MessageBox.Show("Не удалось удалить сделку");
                }
                else
                {
                    MessageBox.Show("Сделка удалена");
                }
            }
        }
        private void GetAllDeals_Click(object sender, EventArgs e)
        {
            AddColumnsAvailableDeal();
            List<Availabledeal> deals = _moderator.GetAllDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    dataGridView1.Rows.Add(deal.Id, deal.Playerid, deal.Tomanagementid, deal.Frommanagementid, deal.Cost, deal.Status);
                }
            }
            else
            {
                MessageBox.Show("Сделки не найдены");
            }
        }

        private void GetPlayerTeamStat_Click(object sender, EventArgs e)
        {
            AddColumnsPlayerTeamStat();
            List<PlayersTeamStat> players = _userController.GetPlayerTeamStat();
            if (players != null)
            {
                foreach (PlayersTeamStat player in players)
                {
                    dataGridView1.Rows.Add(player.playerid, player.player, player.team, player.washers, player.gametime);
                }
            }
            else
            {
                MessageBox.Show("Игроки не найдены");
            }
        }

        private void GetDesiredPlayersForManager_Click(object sender, EventArgs e)
        {
            AddColumnsDesiredPlayer();
            List<Desiredplayer> players = _manager.GetAllDesiredPlayers();
            if (players != null)
            {
                foreach (Desiredplayer player in players)
                {
                    dataGridView1.Rows.Add(player.Id, player.Playerid, player.Teamid, player.Managementid);
                }
            }
            else
            {
                MessageBox.Show("Игроки не найдены");
            }
        }

        private void DeleteDesPlayerManager_Click(object sender, EventArgs e)
        {
            if (DesiredPlayerForManager.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (_manager.DeleteDesiredPlayer(Convert.ToInt32(DesiredPlayerForManager.Text)) == false)
            {
                MessageBox.Show("Не удалось удалить игрока");
                return;
            }
            GetDesiredPlayersForManager_Click(sender, e);
        }

        private void GetAllUsers_Click(object sender, EventArgs e)
        {
            AddColumnsUserInfo();
            List<Userinfo> users = _moderator.GetAllUsers();
            if (users != null)
            {
                foreach (Userinfo user in users)
                {
                    dataGridView1.Rows.Add(user.Id, user.Login, user.Hash, user.Permission);
                }
            }
            else
            {
                MessageBox.Show("Пользователи не найдены");
            }
        }

        private void AddNewUser_Click(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                MessageBox.Show("Вы не ввели логин");
                return;
            }
            if (Hash.Text == "")
            {
                MessageBox.Show("Вы не ввели пароль");
                return;
            }
            if (Perm.Text == "")
            {
                MessageBox.Show("Вы не ввели permissions");
                return;
            }
            if (! _moderator.AddNewUser(Username.Text, Hash.Text, Convert.ToInt32(Perm.Text)))
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }
            else
            {
                MessageBox.Show("Пользователь добавлен");
            }
            GetAllUsers_Click(sender, e);
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            if (UserID.Text == "")
            {
                MessageBox.Show("Вы не ввели ID");
                return;
            }
            if (_moderator.DeleteUser(Convert.ToInt32(UserID.Text)))
            {
                MessageBox.Show("Пользователь удален");
                GetAllUsers_Click(sender, e);
                return;
            }
            MessageBox.Show("Пользователь не найден");
        }
    }
}
