using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL;
using BL.Models;
using BL.IRepositories;


using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions.UserExceptions;
using AccessToDB;



namespace Tests
{
	public class UsersDbTest
	{
		TarantoolContext _context;
		private readonly ITestOutputHelper output;

		public UsersDbTest(ITestOutputHelper output)
		{
			this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            IUsersRepository rep = new TarantoolUsersRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetUsersAsync());

            // add correct
            User added_user = new User(1, 1, "qwe", "rty", (PermissionsEnum) 1);
            await rep.AddUserAsync(added_user.UserID, added_user.CardID, added_user.UserEmail, added_user.Password, added_user.Permissions);
            // add already existing
            await Assert.ThrowsAsync<UserAddException>(() => rep.AddUserAsync(added_user.UserID, added_user.CardID, added_user.UserEmail, added_user.Password, added_user.Permissions));

			// get_by_id correct
			User got_user = await rep.GetUserByIdAsync(added_user.UserID);
            Assert.Equal(added_user, got_user);

			// delete correct
			await rep.DeleteUserByIDAsync(added_user.UserID);

			// get_by_id not existing
			await Assert.ThrowsAsync<UserNotFoundException>(() => rep.GetUserByIdAsync(added_user.UserID));

			// delete not existing
			await Assert.ThrowsAsync<UserDeleteException>(() => rep.DeleteUserByIDAsync(added_user.UserID));

            // end tests - empty getlist
            Assert.Empty(await rep.GetUsersAsync());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            IUsersRepository rep = new TarantoolUsersRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetUsersAsync());

            User added_user1 = new User(1, 1, "qwe", "rty", (PermissionsEnum)1);
            await rep.AddUserAsync(added_user1.UserID, added_user1.CardID, added_user1.UserEmail, added_user1.Password, added_user1.Permissions);

            User added_user2 = new User(2, 9, "rt", "dfd", (PermissionsEnum)2);
            await rep.AddUserAsync(added_user2.UserID, added_user2.CardID, added_user2.UserEmail, added_user2.Password, added_user2.Permissions);

            added_user2 = new User(added_user2.UserID, added_user2.CardID, "dfd", "pop", (PermissionsEnum)1);

            // updates correct
            await rep.UpdateUserByIDAsync(added_user1.UserID, added_user1.CardID, added_user1.UserEmail, added_user1.Password, added_user1.Permissions);
            await rep.UpdateUserByIDAsync(added_user2.UserID, added_user2.CardID, added_user2.UserEmail, added_user2.Password, added_user2.Permissions);

            var list = await rep.GetUsersAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_user1, list[0]);
            Assert.Equal(added_user2, list[1]);

            await rep.DeleteUserByIDAsync(added_user1.UserID);
            await rep.DeleteUserByIDAsync(added_user2.UserID);


            // updates not existing
            await Assert.ThrowsAsync<UserUpdateException>(() => rep.UpdateUserByIDAsync(added_user1.UserID, added_user1.CardID, added_user1.UserEmail, added_user1.Password, added_user1.Permissions));
            await Assert.ThrowsAsync<UserUpdateException>(() => rep.UpdateUserByIDAsync(added_user2.UserID, added_user2.CardID, added_user2.UserEmail, added_user2.Password, added_user2.Permissions));


            // end tests - empty getlist
            Assert.Empty(await rep.GetUsersAsync());



            uint tmpUserID2 = await rep.AddUserAutoIncrementAsync(added_user1.CardID, added_user1.UserEmail, added_user1.Password, added_user1.Permissions);
            Assert.True(1 == tmpUserID2);
            uint tmpUserID3 = await rep.AddUserAutoIncrementAsync(added_user2.CardID, added_user2.UserEmail, added_user2.Password, added_user2.Permissions);
            Assert.True(2 == tmpUserID3);
            await rep.DeleteUserByIDAsync(tmpUserID2);
            await rep.DeleteUserByIDAsync(tmpUserID3);
            Assert.Empty(await rep.GetUsersAsync());
        }
    }
}
