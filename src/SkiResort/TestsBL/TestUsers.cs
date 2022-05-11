using Xunit;
using BL;
using Ninject;
using BL.Models;
using System.Threading.Tasks;
using BL.Exceptions.UserExceptions;
using System.Collections.Generic;


namespace TestsBL
{
    public class TestUsers
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();

            // ��� �������� ������� ������������-��������������
            IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();
            var tmpUsersRepository = repositoriesFactory.CreateUsersRepository();
            uint adminUserID = await tmpUsersRepository.AddUserAutoIncrementAsync(User.UniversalCardID, "admin_email", "admin_password", PermissionsEnum.ADMIN);
            User adminUser = new(adminUserID, User.UniversalCardID, "admin_email", "admin_password", PermissionsEnum.ADMIN);
            tmpUsersRepository = null;
            //

            Facade facade = new Facade(repositoriesFactory);

            uint skiPatrolUserID = 2;
            User skiPatrolUser = new(skiPatrolUserID, User.UniversalCardID, "ski_patrol_email", "ski_patrol_password", PermissionsEnum.SKI_PATROL);
            await facade.AdminAddUserAsync(adminUser.UserID, skiPatrolUser);
            User skiPatrolUserFromDB = await facade.AdminGetUserByIDAsync(adminUser.UserID, skiPatrolUserID);
            Assert.Equal(skiPatrolUser, skiPatrolUserFromDB);

            User updatedSkiPatrolUser = new(skiPatrolUserID, User.UniversalCardID, "ski_patrol_email_updated", "ski_patrol_password", PermissionsEnum.SKI_PATROL);
            await facade.AdminUpdateUserAsync(adminUser.UserID, updatedSkiPatrolUser.UserID, updatedSkiPatrolUser.CardID, updatedSkiPatrolUser.UserEmail, updatedSkiPatrolUser.Password, updatedSkiPatrolUser.Permissions);
            User updatedSkiPatrolUserFromDB = await facade.AdminGetUserByIDAsync(adminUser.UserID, skiPatrolUserID);
            Assert.Equal(updatedSkiPatrolUser, updatedSkiPatrolUserFromDB);  


            uint unauthorizedUser1ID = 3;
            await facade.LogInAsUnauthorizedAsync(unauthorizedUser1ID);
            await Assert.ThrowsAsync<UserDuplicateException>(() => facade.LogInAsUnauthorizedAsync(unauthorizedUser1ID));
    
            User unauthorizedUser1FromDB = await facade.AdminGetUserByIDAsync(adminUser.UserID, unauthorizedUser1ID);
            Assert.Equal(unauthorizedUser1ID, unauthorizedUser1FromDB.UserID);
            Assert.Equal(PermissionsEnum.UNAUTHORIZED, unauthorizedUser1FromDB.Permissions);

            uint unauthorizedUser2ID = 4;
            await facade.LogInAsUnauthorizedAsync(unauthorizedUser2ID);
            User unauthorizedUser2FromDB = await facade.AdminGetUserByIDAsync(adminUser.UserID, unauthorizedUser2ID);

            User userToRegister1 = new(unauthorizedUser1FromDB.UserID, unauthorizedUser1FromDB.CardID, "registration_email", "registration_password", unauthorizedUser1FromDB.Permissions);
            User registeredUser = await facade.RegisterAsync(userToRegister1.UserID, userToRegister1.CardID, userToRegister1.UserEmail, userToRegister1.Password);
            Assert.Equal(PermissionsEnum.AUTHORIZED, registeredUser.Permissions);

            User userToRegister2 = new(unauthorizedUser2FromDB.UserID, unauthorizedUser2FromDB.CardID, "registration_email", "registration_password2", unauthorizedUser2FromDB.Permissions);
            await Assert.ThrowsAsync<UserRegistrationException>(() => facade.RegisterAsync(userToRegister2.UserID, userToRegister2.CardID, userToRegister2.UserEmail, userToRegister2.Password));

            User loggedOutUser = await facade.LogOutAsync(registeredUser.UserID);
            Assert.Equal(PermissionsEnum.UNAUTHORIZED, loggedOutUser.Permissions);

            User loggedInUser = await facade.LogInAsync(registeredUser.UserID, registeredUser.UserEmail, registeredUser.Password);
            Assert.Equal(PermissionsEnum.AUTHORIZED, loggedInUser.Permissions);

            User addedByAdminUser = new(User.UniversalUserID, User.UniversalCardID, "qwe", "rty", PermissionsEnum.ADMIN);
            uint newID = await facade.AdminAddAutoIncrementUserAsync(adminUser.UserID, addedByAdminUser);
            User addedByAdminUserWithNewId = new(newID, addedByAdminUser.CardID, addedByAdminUser.UserEmail, addedByAdminUser.Password, addedByAdminUser.Permissions);
            Assert.Equal(addedByAdminUser.Password, addedByAdminUserWithNewId.Password);
            Assert.NotEqual(addedByAdminUser.UserID, addedByAdminUserWithNewId.UserID);

            List<User> usersFromDB = await facade.AdminGetUsersAsync(adminUser.UserID, 0u, 0u);
            Assert.Equal(5, usersFromDB.Count);
            Assert.Contains(adminUser, usersFromDB);
            Assert.Contains(updatedSkiPatrolUser, usersFromDB);
            Assert.Contains(loggedInUser, usersFromDB);
            Assert.Contains(addedByAdminUserWithNewId, usersFromDB);
            Assert.Contains(unauthorizedUser2FromDB, usersFromDB);

            foreach (var user in usersFromDB)
            {
                if (user != adminUser)
                    await facade.AdminDeleteUserAsync(adminUser.UserID, user.UserID);
            }
            
            usersFromDB = await facade.AdminGetUsersAsync(adminUser.UserID, 0u, Facade.UNLIMITED);


            uint tmpUserID = await facade.AddUnauthorizedUserAsync();
            await facade.AdminDeleteUserAsync(adminUser.UserID, tmpUserID);


            Assert.Single(usersFromDB);

            await facade.AdminDeleteUserAsync(adminUser.UserID, adminUser.UserID);



        }
    }
}