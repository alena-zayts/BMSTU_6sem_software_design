using AccessToDB;
using BL;
using System.Configuration;
using System.ComponentModel.Design;
using Workers;
using Microsoft.Extensions.Hosting;

namespace UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["TarantoolConnectionString"].ConnectionString;
            //ServiceContainer container = new ServiceContainer();

            //CardReadingReceivingService cardReadingReceivingService = new();
            /*            container.Add<VKGroupHelperWorker>(vk);
                        container.RegisterInstance<Settings>(Globals.Settings);
                        container.RegisterInstance<ApplicationContext>(Context);
                        container.Register<IMainFormView, MainForm>();
                        container.Register<MainFormPresenter>();
            */

            IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
    {
        services.AddHostedService<QueueTimeCountingService>();
        services.AddHostedService<CardReadingReceivingService>();
    })
    .Build();

            await host.RunAsync();

            ApplicationConfiguration.Initialize();


            IViewsFactory viewsFactory = new WinFormViewsFactory();
            Facade  facade = new(new TarantoolRepositoriesFactory());
            Presenter presenter = new(viewsFactory, facade);
            presenter.RunAsync();

            //Application.Run(mainView);
        }
    }
}