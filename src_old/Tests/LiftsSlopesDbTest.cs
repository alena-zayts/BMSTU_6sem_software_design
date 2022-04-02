using System;
using System.Linq;
using Xunit;
using SkiResortApp.ComponentAccessToDB.DBModels;
using SkiResortApp.ComponentAccessToDB.RepositoryInterfaces;
using SkiResortApp.ComponentAccessToDB.RepositoriesTarantool;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using System.IO;
using Xunit.Abstractions;
using System.Collections.Generic;


namespace Tests
{
	public class LiftsSlopesDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public LiftsSlopesDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_schema);

			LiftSlopeDB added_lift_slope = new LiftSlopeDB(100000, 1, 1);
			rep.Add(added_lift_slope);


			LiftSlopeDB got_lift_slope = rep.GetById(added_lift_slope.record_id);
			
			Assert.Equal(added_lift_slope, got_lift_slope);

			rep.Delete(added_lift_slope);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_lift_slope.record_id));
			Assert.Empty(rep.GetList());
		}

		//[Fact]
		public void Test_Add_GetByOther_Delete()
		{
			ILiftsRepository lift_rep = new TarantoolLiftsRepository(_schema);
			if (lift_rep.GetList().Count() != 0)
			{
				foreach (var item in lift_rep.GetList())
					lift_rep.Delete(item);
			}
			Assert.Equal(0, lift_rep.GetList().Count());
			LiftDB added_lift1 = new LiftDB(100000, "A1", true, 100, 60, 360);
			lift_rep.Add(added_lift1);
			LiftDB added_lift2 = new LiftDB(200000, "A2", false, 20, 10, 30);
			lift_rep.Add(added_lift2);

			ISlopesRepository slope_rep = new TarantoolSlopesRepository(_schema);
			if (slope_rep.GetList().Count() != 0)
				slope_rep.Delete(slope_rep.GetList()[0]);
			Assert.Empty(slope_rep.GetList());
			SlopeDB added_slope1 = new SlopeDB(1, "A1", true, 1);
			slope_rep.Add(added_slope1);
			SlopeDB added_slope2 = new SlopeDB(2, "A2", false, 20);
			slope_rep.Add(added_slope2);
			SlopeDB added_slope3 = new SlopeDB(3, "A3", true, 5);
			slope_rep.Add(added_slope3);


			ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_schema);
			Assert.Empty(rep.GetList());
			LiftSlopeDB added_lift_slope1 = new LiftSlopeDB(1, added_lift1.lift_id, added_slope1.slope_id);
			LiftSlopeDB added_lift_slope2 = new LiftSlopeDB(2, added_lift1.lift_id, added_slope2.slope_id);
			LiftSlopeDB added_lift_slope4 = new LiftSlopeDB(4, added_lift2.lift_id, added_slope2.slope_id);


			rep.Add(added_lift_slope1);
			rep.Add(added_lift_slope2);
			rep.Add(added_lift_slope4);
			var tmp1 = rep.GetList();
			var tmp2 = lift_rep.GetList();
			var tmp3 = slope_rep.GetList();

			List<LiftDB> from_slope1 = rep.GetLiftsBySlopeId(added_slope1.slope_id);
			Assert.Equal(1, from_slope1.Count());
			Assert.Equal(added_lift1, from_slope1[0]);

			List<LiftDB> from_slope2 = rep.GetLiftsBySlopeId(added_slope2.slope_id);
			Assert.Equal(2, from_slope2.Count());
			Assert.Equal(added_lift1, from_slope2[0]);
			Assert.Equal(added_lift2, from_slope2[1]);

			List<LiftDB> from_slope3 = rep.GetLiftsBySlopeId(added_slope3.slope_id);
			Assert.Equal(0, from_slope3.Count());



			List<SlopeDB> from_lift1 = rep.GetSlopesByLiftId(added_lift1.lift_id);
			Assert.Equal(2, from_lift1.Count());
			Assert.Equal(added_slope1, from_lift1[0]);
			Assert.Equal(added_slope2, from_lift1[1]);

			List<SlopeDB> from_lift2 = rep.GetSlopesByLiftId(added_lift2.lift_id);
			Assert.Equal(1, from_lift2.Count());
			Assert.Equal(added_slope2, from_lift2[0]);


			lift_rep.Delete(added_lift1);
			lift_rep.Delete(added_lift2);
			slope_rep.Delete(added_slope1);
			slope_rep.Delete(added_slope2);
			slope_rep.Delete(added_slope3);
			rep.Delete(added_lift_slope1);
			rep.Delete(added_lift_slope2);
			rep.Delete(added_lift_slope4);

			Assert.Empty(lift_rep.GetList());
			Assert.Empty(slope_rep.GetList());
			Assert.Empty(rep.GetList());

		}


		[Fact]
		public void Test_Update_GetList()
		{

			ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_schema);

			LiftSlopeDB added_lift_slope1 = new LiftSlopeDB(100000, 1, 1);
			rep.Add(added_lift_slope1);

			LiftSlopeDB added_lift_slope2 = new LiftSlopeDB(200000, 1, 2);
			rep.Add(added_lift_slope2);

			added_lift_slope2.lift_id = 5;
			added_lift_slope2.slope_id = 9;
			rep.Update(added_lift_slope2);


			Assert.Equal(2, rep.GetList().Count());

			LiftSlopeDB got_lift_slope1 = rep.GetList()[0];
			LiftSlopeDB got_lift_slope2 = rep.GetList()[1];


			Assert.Equal(added_lift_slope1, got_lift_slope1);
			Assert.Equal(added_lift_slope2, got_lift_slope2);

			rep.Delete(added_lift_slope1);
			rep.Delete(added_lift_slope2);
		}
	}
}
