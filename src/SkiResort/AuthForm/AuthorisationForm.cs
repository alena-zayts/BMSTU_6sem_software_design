using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentAccessToDB;
using DB_CP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ComponentBuisinessLogic;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AuthForm
{
    public partial class AuthorisationForm : Form
    {
        IConfiguration _config;
        public AuthorisationForm()
        {
            _config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
            InitializeComponent();
        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            if (loginBox.Text == "")
            {
                MessageBox.Show("Не введен логин");
                return;
            }
            if (passwordBox.Text == "")
            {
                MessageBox.Show("Не введен пароль");
                return;
            }
            IUserInfoRepository rep = new UserInfoRepository(new transfersystemContext(Connection.GetConnection(0, _config)));
            Userinfo user = rep.FindUserByLogin(loginBox.Text);
            if (user != null)
            {
                if (user.Hash == passwordBox.Text)
                {                     
                    OpenNewForm(Connection.GetConnection(user.Permission, _config), user);
                }
                else
                {
                    MessageBox.Show("Пароль не верный");
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        public void OpenNewForm(string connectionString, Userinfo user) 
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Form1>();
                    services.AddSingleton(provider => { return user; });
                    DiExtensions.AddRepositoryExtensions(services);
                    DiExtensions.AddControllerExtensions(services);
                    services.AddDbContext<transfersystemContext>(option => option.UseNpgsql(connectionString));

                    var serilogLogger = new LoggerConfiguration()
                        .WriteTo.File(_config["Logger"])
                        .CreateLogger();
                    services.AddLogging(x =>
                    {
                        x.AddSerilog(logger: serilogLogger, dispose: true);
                    });
                });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    var form1 = services.GetRequiredService<Form1>();
                    form1.Show();

                    Console.WriteLine("Successfully opened");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }
        }
    }
    public static class DiExtensions
    {
        public static void AddRepositoryExtensions(IServiceCollection services)
        {
            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.AddSingleton<ITeamRepository, TeamRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoRepository>();
            services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
            services.AddSingleton<IAvailableDealsRepository, AvailableDealsRepository>();
            services.AddSingleton<IDesiredPlayersRepository, DesiredPlayersRepository>();
            services.AddSingleton<IManagementRepository, ManagementRepository>();
            services.AddSingleton<IFunctionsRepository, FunctionRepository>();
        }
        public static void AddControllerExtensions(IServiceCollection services)
        {
            services.AddScoped<AnalyticController>();
            services.AddScoped<ManagerController>();
            services.AddScoped<ModeratorController>();
            services.AddScoped<UserController>();
        }
    }
}



