using Xunit;
using BL;
using Ninject.Modules;
using Ninject;
using BL.Models;
using System.Threading.Tasks;


namespace TestsBL
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();
            IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();

            Facade facade = new Facade(repositoriesFactory);

            var tmpUsersRepository = repositoriesFactory.CreateUsersRepository();
            

            User adminUser = await tmpUsersRepository.AddUserAutoIncrementAsync(new User(0, User.UniversalCardID, "admin_email", "admin_password", PermissionsEnum.ADMIN));
            User skiPatrolUser = await tmpUsersRepository.AddUserAutoIncrementAsync(new User(0, User.UniversalCardID, "ski_patrol_email", "ski_patrol_password", PermissionsEnum.SKI_PATROL));

            uint unauthorizedUserID = 3;
            await facade.LogInAsUnauthorizedAsync(unauthorizedUserID);

            User unauthorizedUserFromDB = facade.AdminUsersGetByIDAsync(adminUser.UserID, unauthorizedUserID);
            Assert.Equal()

            User registered_user = await facade.Register(new User(unauthorizedUserID, 0, "registration_email", "registration_password", Permissions.UNAUTHORIZED));
            Assert.Equal(Permissions.AUTHORIZED, registered_user.Permissions);

            User loged_out_user = await facade.LogOut(registered_user);
            
            facade.Register(registered_user);

            var tmp = facade.AdminAddAutoIncrementTurnstile((uint) Permissions.ADMIN, new TurnstileBL(0, 1, true));
        }
    }
}