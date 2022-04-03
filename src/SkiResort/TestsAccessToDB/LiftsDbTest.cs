//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Xunit;
//using Xunit.Abstractions;

//using ProGaudi.Tarantool.Client;

//using ComponentBL.ModelsBL;
//using ComponentAccessToDB.RepositoriesInterfaces;
//using ComponentAccessToDB.RepositoriesTarantool;

//namespace Tests
//{
//	public class LiftsDbTest
//	{
//		ISchema _schema;
//		private readonly ITestOutputHelper output;
//		public LiftsDbTest(ITestOutputHelper output)
//		{
//			this.output = output;

//			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

//			_schema = box.GetSchema();
//		}
//		[Fact]
//		public void Test_Add_GetById_Delete()
//		{
//			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

//			LiftBL added_lift = new LiftBL(100000, "A1", true, 100, 60, 360);
//			rep.Add(added_lift);


//			LiftBL got_lift = rep.GetById(added_lift.lift_id);
			
//			Assert.Equal(added_lift, got_lift);

//			rep.Delete(added_lift);

//			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_lift.lift_id));
//		}

//		[Fact]
//		public void Test_Add_GetByName_Delete()
//		{

//			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

//			LiftBL added_lift = new LiftBL(200000, "A2", false, 20, 10, 30);
//			rep.Add(added_lift);


//			LiftBL got_lift = rep.GetByName(added_lift.lift_name);


//			Assert.Equal(added_lift, got_lift);

//			rep.Delete(added_lift);

//			Assert.Throws<IndexOutOfRangeException>(() => rep.GetByName(added_lift.lift_name));
//		}


//		[Fact]
//		public void Test_Update_GetList()
//		{

//			ILiftsRepository rep = new TarantoolLiftsRepository(_schema);

//			LiftBL added_lift1 = new LiftBL(100000, "A1", true, 100, 60, 360);
//			rep.Add(added_lift1);

//			LiftBL added_lift2 = new LiftBL(200000, "A2", false, 20, 10, 30);
//			rep.Add(added_lift2);

//			added_lift2.is_open = true;
//			added_lift2.queue_time = 50;
//			rep.Update(added_lift2);


//			Assert.Equal(2, rep.GetList().Count());

//			LiftBL got_lift1 = rep.GetList()[0];
//			LiftBL got_lift2 = rep.GetList()[1];


//			Assert.Equal(added_lift2, got_lift2);

//			rep.Delete(added_lift1);
//			rep.Delete(added_lift2);

//			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_lift1.lift_id));
//			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_lift2.lift_id));
//			Assert.Empty(rep.GetList());
//		}
//	}
//}
