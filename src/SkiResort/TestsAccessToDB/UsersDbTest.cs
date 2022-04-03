using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;


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

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();
			_context = new ContextTarantool(box);
		}

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            IUsersRepository rep = new TarantoolUsersRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            // add correct
            UserBL added_user = new UserBL(1, 1, "qwe", "rty", 1);
            await rep.Add(added_user);
            // add already existing
            await Assert.ThrowsAsync<UserDBException>(() => rep.Add(added_user));

			// get_by_id correct
			UserBL got_user = await rep.GetById(added_user.user_id);
            Assert.Equal(added_user, got_user);

			// delete correct
			await rep.Delete(added_user);

			// get_by_id not existing
			await Assert.ThrowsAsync<UserDBException>(() => rep.GetById(added_user.user_id));

			// delete not existing
			await Assert.ThrowsAsync<UserDBException>(() => rep.Delete(added_user));

            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            IUsersRepository rep = new TarantoolUsersRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            UserBL added_user1 = new UserBL(1, 1, "qwe", "rty", 1);
            await rep.Add(added_user1);

            UserBL added_user2 = new UserBL(2, 9, "rt", "dfd", 2);
            await rep.Add(added_user2);

            added_user2.password = "dfd";
            added_user1.user_email = "wow";

            // updates correct
            await rep.Update(added_user1);
            await rep.Update(added_user2);

            var list = await rep.GetList();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_user1, list[0]);
            Assert.Equal(added_user2, list[1]);

            await rep.Delete(added_user1);
            await rep.Delete(added_user2);


            // updates not existing
            await Assert.ThrowsAsync<UserDBException>(() => rep.Update(added_user1));
            await Assert.ThrowsAsync<UserDBException>(() => rep.Update(added_user2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }
    }
}
