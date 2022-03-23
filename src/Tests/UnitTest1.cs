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


namespace Tests
{
	public class LiftsDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public LiftsDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

			Lift added_lift = new Lift(100000, "A1", true, 100, 60, 360);
			rep.Add(added_lift);


			Lift got_lift = rep.GetById(added_lift.lift_id);
			

			Assert.Equal(added_lift.lift_id, got_lift.lift_id);
			Assert.Equal(added_lift.lift_name, got_lift.lift_name);
			Assert.Equal(added_lift.is_open, got_lift.is_open);
			Assert.Equal(added_lift.seats_amount, got_lift.seats_amount);
			Assert.Equal(added_lift.queue_time, got_lift.queue_time);
			Assert.Equal(added_lift.lifting_time, got_lift.lifting_time);

			rep.Delete(added_lift);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_lift.lift_id));
		}

		[Fact]
		public void Test_Add_GetByName_Delete()
		{

			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

			Lift added_lift = new Lift(200000, "A2", false, 20, 10, 30);
			rep.Add(added_lift);


			Lift got_lift = rep.GetByName(added_lift.lift_name);


			Assert.Equal(added_lift.lift_id, got_lift.lift_id);
			Assert.Equal(added_lift.lift_name, got_lift.lift_name);
			Assert.Equal(added_lift.is_open, got_lift.is_open);
			Assert.Equal(added_lift.seats_amount, got_lift.seats_amount);
			Assert.Equal(added_lift.queue_time, got_lift.queue_time);
			Assert.Equal(added_lift.lifting_time, got_lift.lifting_time);

			rep.Delete(added_lift);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetByName(added_lift.lift_name));
		}


		[Fact]
		public void Test_Update_GetList()
		{

			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

			Lift added_lift1 = new Lift(100000, "A1", true, 100, 60, 360);
			rep.Add(added_lift1);

			Lift added_lift2 = new Lift(200000, "A2", false, 20, 10, 30);
			rep.Add(added_lift2);

			added_lift2.is_open = true;
			added_lift2.queue_time = 50;
			rep.Update(added_lift2);


			Assert.Equal(2, rep.GetList().Count());

			Lift got_lift1 = rep.GetList()[0];
			Lift got_lift2 = rep.GetList()[1];


			Assert.Equal(added_lift2.lift_id, got_lift2.lift_id);
			Assert.Equal(added_lift2.lift_name, got_lift2.lift_name);
			Assert.Equal(added_lift2.is_open, got_lift2.is_open);
			Assert.Equal(added_lift2.seats_amount, got_lift2.seats_amount);
			Assert.Equal(added_lift2.queue_time, got_lift2.queue_time);
			Assert.Equal(added_lift2.lifting_time, got_lift2.lifting_time);

			rep.Delete(added_lift1);
			rep.Delete(added_lift2);
		}
	}
}
