using AccessToDB;
using BL;
using System.Configuration;
using System.ComponentModel.Design;
using Workers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;
using AccessToDB2;
using AccessToDB2.Models;

using Microsoft.EntityFrameworkCore;
namespace UI
{

    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();


            //IRepositoriesFactory repositoryFactory = new TarantoolRepositoriesFactory();
            IRepositoriesFactory repositoryFactory = new PostgresRepositoriesFactrory(Connection.GetConnection());


            IViewsFactory viewsFactory = new WinFormViewsFactory();
            Facade facade = new(repositoryFactory);
            Presenter presenter = new(viewsFactory, facade);
            presenter.RunAsync();

            //Task.Run(() => presenter.RunAsync());


            //IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            //{
            //    services.AddHostedService<QueueTimeCountingService>();
            //    services.AddHostedService<CardReadingReceivingService>();
            //}).Build();
            //host.RunAsync();
        }
    }
    //public static class DiExtensions
    //{
    //    public static void AddRepositoryExtensions(IServiceCollection services)
    //    {
    //        services.AddSingleton<IRepositoriesFactory, TarantoolRepositoriesFactory>();
    //        services.AddSingleton<IViewsFactory, WinFormViewsFactory>();
    //    }
    //}
}