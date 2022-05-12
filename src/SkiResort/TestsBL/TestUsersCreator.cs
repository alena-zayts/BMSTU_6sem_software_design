using Ninject;
using System.Threading.Tasks;

using BL;
using BL.Models;
using BL.Exceptions;

using System.Collections.Generic;

namespace TestsBL
{
    public class TestUsersCreator
    {
        public const uint adminID = 1;
        public const uint skiPatrolID = 2;
        public const uint authorizedID = 3;
        public const uint unauthorizedID = 4;
        public static async Task Create()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();

            
            IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();
            var tmpUsersRepository = repositoriesFactory.CreateUsersRepository();
            User adminUser = new (adminID, User.UniversalCardID, "admin_email", "admin_password", PermissionsEnum.ADMIN);
            await tmpUsersRepository.AddUserAsync(adminUser.UserID, adminUser.CardID, adminUser.UserEmail, adminUser.Password, adminUser.Permissions);
            tmpUsersRepository = null;

            Facade facade = new(repositoriesFactory);

            User skiPatrolUser = new(skiPatrolID, User.UniversalCardID, "ski_patrol_email", "ski_patrol_password", PermissionsEnum.SKI_PATROL);
            await facade.AdminAddUserAsync(adminUser.UserID, skiPatrolUser.UserID, skiPatrolUser.CardID, skiPatrolUser.UserEmail, skiPatrolUser.Password, skiPatrolUser.Permissions);

            
            await facade.LogInAsUnauthorizedAsync(unauthorizedID);
            await facade.LogInAsUnauthorizedAsync(authorizedID);
            
            User userToRegister1 = new(authorizedID, User.UniversalCardID, "registration_email", "registration_password", PermissionsEnum.UNAUTHORIZED);
            User registeredUser = await facade.RegisterAsync(userToRegister1.UserID, userToRegister1.CardID, userToRegister1.UserEmail, userToRegister1.Password);
        }
    }
}
