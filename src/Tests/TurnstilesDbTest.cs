using System;
using System.Linq;
using Xunit;
using SkiResortApp.DbModels;
using SkiResortApp.IRepositories;
using SkiResortApp.TarantoolRepositories;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using System.IO;
using Xunit.Abstractions;
using System.Collections.Generic;


namespace Tests
{
	public class TurnstilesDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public TurnstilesDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_schema);

			Turnstile added_turnstile = new Turnstile(100000, 1, true);
			rep.Add(added_turnstile);


			Turnstile got_turnstile = rep.GetById(added_turnstile.turnstile_id);
			

			Assert.Equal(added_turnstile.turnstile_id, got_turnstile.turnstile_id);
			Assert.Equal(added_turnstile.lift_id, got_turnstile.lift_id);
			Assert.Equal(added_turnstile.is_open, got_turnstile.is_open);


			rep.Delete(added_turnstile);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_turnstile.turnstile_id));
		}

		[Fact]
		public void Test_Add_GetByLiftId_Delete()
		{

			ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_schema);

			Turnstile added_turnstile1 = new Turnstile(100000, 1, true);
			rep.Add(added_turnstile1);
			Turnstile added_turnstile2 = new Turnstile(200000, 1, false);
			rep.Add(added_turnstile2);


			List<Turnstile> got_turnstiles = rep.GetByLiftId(1);
			//got_turnstiles = rep.GetList();
			Assert.Equal(2, got_turnstiles.Count());

			Turnstile got_turnstile1 = got_turnstiles[0];
			Turnstile got_turnstile2 = got_turnstiles[1];

			
			Assert.Equal(added_turnstile1.turnstile_id, got_turnstile1.turnstile_id);
			Assert.Equal(added_turnstile1.lift_id, got_turnstile1.lift_id);
			Assert.Equal(added_turnstile1.is_open, got_turnstile1.is_open);
			
			Assert.Equal(added_turnstile2.turnstile_id, got_turnstile2.turnstile_id);
			Assert.Equal(added_turnstile2.lift_id, got_turnstile2.lift_id);
			Assert.Equal(added_turnstile2.is_open, got_turnstile2.is_open);
			

			rep.Delete(added_turnstile1);
			rep.Delete(added_turnstile2);

			got_turnstiles = rep.GetByLiftId(1);
			Assert.Empty(got_turnstiles);

		}


		[Fact]
		public void Test_Update_GetList()
		{

			ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_schema);

			Turnstile added_turnstile1 = new Turnstile(100000, 1, true);
			rep.Add(added_turnstile1);
			Turnstile added_turnstile2 = new Turnstile(200000, 2, false);
			rep.Add(added_turnstile2);

			added_turnstile2.is_open = true;
			added_turnstile2.lift_id = 1;
			rep.Update(added_turnstile2);


			Assert.Equal(2, rep.GetList().Count());

			Turnstile got_turnstile1 = rep.GetList()[0];
			Turnstile got_turnstile2 = rep.GetList()[1];


			Assert.Equal(added_turnstile2.turnstile_id, got_turnstile2.turnstile_id);
			Assert.Equal(added_turnstile2.lift_id, got_turnstile2.lift_id);
			Assert.Equal(added_turnstile2.is_open, got_turnstile2.is_open);

			rep.Delete(added_turnstile1);
			rep.Delete(added_turnstile2);
		}
	}
}
