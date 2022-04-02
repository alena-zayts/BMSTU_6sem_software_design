using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using SkiResort.ComponentBL.ModelsBL;
using SkiResort.ComponentAccessToDB.RepositoriesInterfaces;
using SkiResort.ComponentAccessToDB.RepositoriesTarantool;
using SkiResort.ComponentAccessToDB.DBContexts;


namespace Tests
{
	public class UsersDbTest
	{
		ISchema _schema;
		TarantoolContext _context;
		private readonly ITestOutputHelper output;

		public UsersDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
			_context = new TarantoolContext(_schema);
		}
		//[Fact]
		//public void Test_Add_GetById_Delete()
		//{
		//	IUsersRepository rep = new TarantoolUsersRepository(_schema);

		//	UserBL added_user = new UserBL(100000, 1, "qwe", "rty", 1);
		//	rep.Add(added_user);


		//	UserBL got_user = rep.GetById(added_user.user_id);
			
		//	Assert.Equal(added_user, got_user);

		//	rep.Delete(added_user);

		//	Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_user.user_id));
		//}


		[Fact]
		public async Task Test_Update_GetList()
		{

			IUsersRepository rep = new TarantoolUsersRepository(_context);

			UserBL added_user1 = new UserBL(100000, 1, "qwe", "rty", 1);
			await rep.Add(added_user1);

			UserBL added_user2 = new UserBL(200000, 9, "rt", "dfd", 5);
			await rep.Add(added_user2);

			added_user2.password = "dfd";
			added_user1.card_id = 50;
			await rep.Update(added_user1);
			await rep.Update(added_user2);

			var list = await rep.GetList();


			Assert.Equal(2, list.Count);

			UserBL got_user1 = list[0];
			UserBL got_user2 = list[1];


			Assert.Equal(added_user2, got_user2);

			await rep.Delete(added_user1);
			await rep.Delete(added_user2);

            await Assert.ThrowsAsync<IndexOutOfRangeException>(() => rep.GetById(added_user1.user_id));
            await Assert.ThrowsAsync<IndexOutOfRangeException>(() => rep.GetById(added_user2.user_id));
			Assert.Empty(await rep.GetList());

			await rep.GetById(88);
		}
	}
}
