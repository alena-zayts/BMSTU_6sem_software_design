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

// https://xunit.net/docs/getting-started/netcore/cmdline

namespace DbTests
{
	public class LiftsDbTest
	{
		ISchema _schema;
		public LiftsDbTest()
		{
			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById()
		{
			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

			Lift added_lift = new Lift(1, "A1", true, 100, 60, 360);
			rep.Add(added_lift);

			Lift got_lift = rep.GetById(added_lift.lift_id);


			Assert.Equal(added_lift.lift_id, got_lift.lift_id);
			Assert.Equal(added_lift.lift_name, got_lift.lift_name);
			Assert.Equal(added_lift.is_open, got_lift.is_open);
			Assert.Equal(added_lift.seats_amount, got_lift.seats_amount);
			Assert.Equal(added_lift.queue_time, got_lift.queue_time);
			Assert.Equal(added_lift.lifting_time, got_lift.lifting_time);
		}

	}
}