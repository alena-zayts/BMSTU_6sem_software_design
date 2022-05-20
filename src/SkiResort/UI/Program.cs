using AccessToDB;
using BL;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            IViewsFactory viewsFactory = new ViewsFactory();
            IViewsFactory viewsFactory2 = new TechViewsFactory();
            Facade  facade = new(new TarantoolRepositoriesFactory());
            Presenter presenter = new(viewsFactory2, facade);
            presenter.RunAsync();

            //Application.Run(mainView);
        }
    }
}