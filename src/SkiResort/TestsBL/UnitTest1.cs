using Xunit;
using ComponentBL;
using Ninject.Modules;
using Ninject;
using ComponentBL.ModelsBL;
using System.Threading.Tasks;


namespace TestsBL
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<FakeRepositoriesFactory>();
            IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();

            FacadeBL facade = new FacadeBL(repositoriesFactory);

            var tmp_user_rep = repositoriesFactory.CreateUsersRepository();
            

            UserBL admin_user = await tmp_user_rep.AddAutoIncrement(new UserBL(0, 0, "admin_email", "admin_password", (uint)Permissions.ADMIN));
            UserBL ski_patrol_user = await tmp_user_rep.AddAutoIncrement(new UserBL(0, 0, "ski_patrol_email", "ski_patrol_password", (uint)Permissions.SKI_PATROL));

            uint unauthorized_user_id = 3;
            await facade.LogInAsUnauthorized(unauthorized_user_id);

            UserBL unauthorized_user_from_db = faca
            Assert.Equal(new UserBL(unauthorized_user_id, ))

            UserBL registered_user = await facade.Register(new UserBL(unauthorized_user_id, 0, "registration_email", "registration_password", (uint)Permissions.UNAUTHORIZED));
            Assert.Equal((uint)Permissions.AUTHORIZED, registered_user.permissions);

            UserBL loged_out_user = await facade.LogOut(registered_user);
            
            facade.Register(registered_user);

            var tmp = facade.AdminAddAutoIncrementTurnstile((uint) Permissions.ADMIN, new TurnstileBL(0, 1, true));
        }
    }
}