using Xunit;
using ComponentBL;
using Ninject.Modules;
using Ninject;
using ComponentBL.ModelsBL;


namespace TestsBL
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<FakeRepositoriesFactory>();
            IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();

            FacadeBL facade = new FacadeBL(repositoriesFactory);

            var tmp = facade.AdminAddAutoIncrementTurnstile((uint) Permissions.ADMIN, new TurnstileBL(0, 1, true));
        }
    }
}