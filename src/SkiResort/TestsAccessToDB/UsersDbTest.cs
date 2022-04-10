using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL;
using BL.Models;
using BL.IRepositories;


using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions;
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
            await rep.AddUserAsync(added_user);
            // add already existing
            await Assert.ThrowsAsync<UserException>(() => rep.AddUserAsync(added_user));

			// get_by_id correct
			User got_user = await rep.GetUserByIdAsync(added_user.UserID);
            Assert.Equal(added_user, got_user);

			// delete correct
			await rep.DeleteUserAsync(added_user);

			// get_by_id not existing
			await Assert.ThrowsAsync<UserException>(() => rep.GetUserByIdAsync(added_user.UserID));

			// delete not existing
			await Assert.ThrowsAsync<UserException>(() => rep.DeleteUserAsync(added_user));

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
            await rep.AddUserAsync(added_user1);

            User added_user2 = new User(2, 9, "rt", "dfd", (PermissionsEnum)2);
            await rep.AddUserAsync(added_user2);

            added_user2 = new User(added_user2.UserID, added_user2.CardID, "dfd", "pop", (PermissionsEnum)1);

            // updates correct
            await rep.UpdateUserAsync(added_user1);
            await rep.UpdateUserAsync(added_user2);

            var list = await rep.GetUsersAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_user1, list[0]);
            Assert.Equal(added_user2, list[1]);

            await rep.DeleteUserAsync(added_user1);
            await rep.DeleteUserAsync(added_user2);


            // updates not existing
            await Assert.ThrowsAsync<UserException>(() => rep.UpdateUserAsync(added_user1));
            await Assert.ThrowsAsync<UserException>(() => rep.UpdateUserAsync(added_user2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetUsersAsync());



            User tmp2 = await rep.AddUserAutoIncrementAsync(added_user1);
            Assert.True(1 == tmp2.UserID);
            User tmp3 = await rep.AddUserAutoIncrementAsync(added_user2);
            Assert.True(2 == tmp3.UserID);
            await rep.DeleteUserAsync(tmp2);
            await rep.DeleteUserAsync(tmp3);
            Assert.Empty(await rep.GetUsersAsync());
        }
    }
}
