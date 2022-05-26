using AccessToDB;
using BL;
using UI;

namespace TechUI
{
    internal static class Program
    {

        static void Main()
        {
            IViewsFactory viewsFactory = new TechViewsFactory();
            Facade facade = new(new TarantoolRepositoriesFactory());
            Presenter presenter = new(viewsFactory, facade);
            presenter.RunAsync();
        }
    }
}