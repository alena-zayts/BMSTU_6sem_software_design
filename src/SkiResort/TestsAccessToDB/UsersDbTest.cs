using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL.Models;
using BL.IRepositories;


using ComponentAccessToDB.RepositoriesTarantool;
using ComponentAccessToDB;



namespace Tests
{
	public class UsersDbTest
	{
		ContextTarantool _context;
		private readonly ITestOutputHelper output;

		public UsersDbTest(ITestOutputHelper output)
		{
			this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            IUsersRepository rep = new TarantoolUsersRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetUsersAsync());

            // add correct
            User added_user = new User(1, 1, "qwe", "rty", 1);
            await rep.Add(added_user);
            // add already existing
            await Assert.ThrowsAsync<UserDBException>(() => rep.Add(added_user));

			// get_by_id correct
			User got_user = await rep.GetById(added_user.UserID);
            Assert.Equal(added_user, got_user);

			// delete correct
			await rep.Delete(added_user);

			// get_by_id not existing
			await Assert.ThrowsAsync<UserDBException>(() => rep.GetById(added_user.UserID));

			// delete not existing
			await Assert.ThrowsAsync<UserDBException>(() => rep.Delete(added_user));

            // end tests - empty getlist
            Assert.Empty(await rep.GetUsersAsync());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            IUsersRepository rep = new TarantoolUsersRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetUsersAsync());

            User added_user1 = new User(1, 1, "qwe", "rty", 1);
            await rep.Add(added_user1);

            User added_user2 = new User(2, 9, "rt", "dfd", 2);
            await rep.Add(added_user2);

            added_user2.Password = "dfd";
            added_user1.UserEmail = "wow";

            // updates correct
            await rep.Update(added_user1);
            await rep.Update(added_user2);

            var list = await rep.GetUsersAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_user1, list[0]);
            Assert.Equal(added_user2, list[1]);

            await rep.Delete(added_user1);
            await rep.Delete(added_user2);


            // updates not existing
            await Assert.ThrowsAsync<UserDBException>(() => rep.Update(added_user1));
            await Assert.ThrowsAsync<UserDBException>(() => rep.Update(added_user2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetUsersAsync());



            User tmp2 = await rep.AddAutoIncrement(added_user1);
            Assert.True(1 == tmp2.UserID);
            User tmp3 = await rep.AddAutoIncrement(added_user2);
            Assert.True(2 == tmp3.UserID);
            await rep.Delete(tmp2);
            await rep.Delete(tmp3);
            Assert.Empty(await rep.GetUsersAsync());
        }
    }
}
