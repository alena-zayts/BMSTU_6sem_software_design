using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using SkiResort.ComponentBL.ModelsBL;
using SkiResort.ComponentAccessToDB.RepositoriesInterfaces;
using SkiResort.ComponentAccessToDB.RepositoriesTarantool;


namespace Tests
{
	public class UsersDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public UsersDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			IUsersRepository rep = new TarantoolUsersRepository(_schema);

			UserBL added_user = new UserBL(100000, 1, "qwe", "rty", 1);
			rep.Add(added_user);


			UserBL got_user = rep.GetById(added_user.user_id);
			
			Assert.Equal(added_user, got_user);

			rep.Delete(added_user);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_user.user_id));
		}


		[Fact]
		public void Test_Update_GetList()
		{

			IUsersRepository rep = new TarantoolUsersRepository(_schema);

			UserBL added_user1 = new UserBL(100000, 1, "qwe", "rty", 1);
			rep.Add(added_user1);

			UserBL added_user2 = new UserBL(200000, 9, "rt", "dfd", 5);
			rep.Add(added_user2);

			added_user2.password = "dfd";
			added_user1.card_id = 50;
			rep.Update(added_user1);
			rep.Update(added_user2);


			Assert.Equal(2, rep.GetList().Count());

			UserBL got_user1 = rep.GetList()[0];
			UserBL got_user2 = rep.GetList()[1];


			Assert.Equal(added_user2, got_user2);

			rep.Delete(added_user1);
			rep.Delete(added_user2);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_user1.user_id));
			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_user2.user_id));
			Assert.Empty(rep.GetList());
		}
	}
}
