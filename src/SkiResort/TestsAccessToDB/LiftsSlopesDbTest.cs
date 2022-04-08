

//		//[Fact]
//		public void Test_Add_GetByOther_Delete()
//		{
//			ILiftsRepository lift_rep = new TarantoolLiftsRepository(_schema);
//			if (lift_rep.GetList().Count() != 0)
//			{
//				foreach (var item in lift_rep.GetList())
//					lift_rep.Delete(item);
//			}
//			Assert.Equal(0, lift_rep.GetList().Count());
//			LiftBL added_lift1 = new LiftBL(100000, "A1", true, 100, 60, 360);
//			lift_rep.Add(added_lift1);
//			LiftBL added_lift2 = new LiftBL(200000, "A2", false, 20, 10, 30);
//			lift_rep.Add(added_lift2);

//			ISlopesRepository slope_rep = new TarantoolSlopesRepository(_schema);
//			if (slope_rep.GetList().Count() != 0)
//				slope_rep.Delete(slope_rep.GetList()[0]);
//			Assert.Empty(slope_rep.GetList());
//			SlopeBL added_slope1 = new SlopeBL(1, "A1", true, 1);
//			slope_rep.Add(added_slope1);
//			SlopeBL added_slope2 = new SlopeBL(2, "A2", false, 20);
//			slope_rep.Add(added_slope2);
//			SlopeBL added_slope3 = new SlopeBL(3, "A3", true, 5);
//			slope_rep.Add(added_slope3);


//			ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_schema);
//			Assert.Empty(rep.GetList());
//			LiftSlopeBL added_lift_slope1 = new LiftSlopeBL(1, added_lift1.LiftID, added_slope1.SlopeID);
//			LiftSlopeBL added_lift_slope2 = new LiftSlopeBL(2, added_lift1.LiftID, added_slope2.SlopeID);
//			LiftSlopeBL added_lift_slope4 = new LiftSlopeBL(4, added_lift2.LiftID, added_slope2.SlopeID);


//			rep.Add(added_lift_slope1);
//			rep.Add(added_lift_slope2);
//			rep.Add(added_lift_slope4);
//			var tmp1 = rep.GetList();
//			var tmp2 = lift_rep.GetList();
//			var tmp3 = slope_rep.GetList();

//			List<LiftBL> from_slope1 = rep.GetLiftsBySlopeId(added_slope1.SlopeID);
//			Assert.Equal(1, from_slope1.Count());
//			Assert.Equal(added_lift1, from_slope1[0]);

//			List<LiftBL> from_slope2 = rep.GetLiftsBySlopeId(added_slope2.SlopeID);
//			Assert.Equal(2, from_slope2.Count());
//			Assert.Equal(added_lift1, from_slope2[0]);
//			Assert.Equal(added_lift2, from_slope2[1]);

//			List<LiftBL> from_slope3 = rep.GetLiftsBySlopeId(added_slope3.SlopeID);
//			Assert.Equal(0, from_slope3.Count());



//			List<SlopeBL> from_lift1 = rep.GetSlopesByLiftId(added_lift1.LiftID);
//			Assert.Equal(2, from_lift1.Count());
//			Assert.Equal(added_slope1, from_lift1[0]);
//			Assert.Equal(added_slope2, from_lift1[1]);

//			List<SlopeBL> from_lift2 = rep.GetSlopesByLiftId(added_lift2.LiftID);
//			Assert.Equal(1, from_lift2.Count());
//			Assert.Equal(added_slope2, from_lift2[0]);


//			lift_rep.Delete(added_lift1);
//			lift_rep.Delete(added_lift2);
//			slope_rep.Delete(added_slope1);
//			slope_rep.Delete(added_slope2);
//			slope_rep.Delete(added_slope3);
//			rep.Delete(added_lift_slope1);
//			rep.Delete(added_lift_slope2);
//			rep.Delete(added_lift_slope4);

//			Assert.Empty(lift_rep.GetList());
//			Assert.Empty(slope_rep.GetList());
//			Assert.Empty(rep.GetList());

//		}


//		[Fact]
//		public void Test_Update_GetList()
//		{

//			ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_schema);

//			LiftSlopeBL added_lift_slope1 = new LiftSlopeBL(100000, 1, 1);
//			rep.Add(added_lift_slope1);

//			LiftSlopeBL added_lift_slope2 = new LiftSlopeBL(200000, 1, 2);
//			rep.Add(added_lift_slope2);

//			added_lift_slope2.LiftID = 5;
//			added_lift_slope2.SlopeID = 9;
//			rep.Update(added_lift_slope2);


//			Assert.Equal(2, rep.GetList().Count());

//			LiftSlopeBL got_lift_slope1 = rep.GetList()[0];
//			LiftSlopeBL got_lift_slope2 = rep.GetList()[1];


//			Assert.Equal(added_lift_slope1, got_lift_slope1);
//			Assert.Equal(added_lift_slope2, got_lift_slope2);

//			rep.Delete(added_lift_slope1);
//			rep.Delete(added_lift_slope2);
//		}
//	}
//}

using System.Collections.Generic;
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
    public class LiftsSlopesDbTest
    {
        ContextTarantool _context;
        private readonly ITestOutputHelper output;

        public LiftsSlopesDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            // add correct
            LiftSlopeBL added_lift_slope = new LiftSlopeBL(1, 1, 1);
            await rep.Add(added_lift_slope);
            // add already existing
            await Assert.ThrowsAsync<LiftSlopeDBException>(() => rep.Add(added_lift_slope));

            // get_by_id correct
            LiftSlopeBL got_lift_slope = await rep.GetById(added_lift_slope.RecordID);
            Assert.Equal(added_lift_slope, got_lift_slope);

            // delete correct
            await rep.Delete(added_lift_slope);

            // get_by_id not existing
            await Assert.ThrowsAsync<LiftSlopeDBException>(() => rep.GetById(added_lift_slope.RecordID));

            // delete not existing
            await Assert.ThrowsAsync<LiftSlopeDBException>(() => rep.Delete(added_lift_slope));

            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            LiftSlopeBL added_lift_slope1 = new LiftSlopeBL(1, 1, 1);
            await rep.Add(added_lift_slope1);

            LiftSlopeBL added_lift_slope2 = new LiftSlopeBL(2, 2, 1);
            await rep.Add(added_lift_slope2);

            added_lift_slope2.LiftID = 100;
            added_lift_slope1.SlopeID = 200;

            // updates correct
            await rep.Update(added_lift_slope1);
            await rep.Update(added_lift_slope2);

            var list = await rep.GetList();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_lift_slope1, list[0]);
            Assert.Equal(added_lift_slope2, list[1]);

            await rep.Delete(added_lift_slope1);
            await rep.Delete(added_lift_slope2);


            // updates not existing
            await Assert.ThrowsAsync<LiftSlopeDBException>(() => rep.Update(added_lift_slope1));
            await Assert.ThrowsAsync<LiftSlopeDBException>(() => rep.Update(added_lift_slope2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }

        //[Fact]
        public async Task Test_Add_GetByOther_Delete()
        {
            ILiftsRepository lift_rep = new TarantoolLiftsRepository(_context);
            Assert.Empty(await lift_rep.GetList());


            LiftBL added_lift1 = new LiftBL(100000, "A1", true, 100, 60, 360);
            await lift_rep.Add(added_lift1);
            LiftBL added_lift2 = new LiftBL(200000, "A2", false, 20, 10, 30);
            await lift_rep.Add(added_lift2);

            ISlopesRepository slope_rep = new TarantoolSlopesRepository(_context);
            Assert.Empty(await slope_rep.GetSlopes());

            SlopeBL added_slope1 = new SlopeBL(1, "A1", true, 1);
            await slope_rep.Add(added_slope1);
            SlopeBL added_slope2 = new SlopeBL(2, "A2", false, 20);
            await slope_rep.Add(added_slope2);
            SlopeBL added_slope3 = new SlopeBL(3, "A3", true, 5);
            await slope_rep.Add(added_slope3);


            ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_context);
            Assert.Empty(await rep.GetList());
            LiftSlopeBL added_lift_slope1 = new LiftSlopeBL(1, added_lift1.LiftID, added_slope1.SlopeID);
            LiftSlopeBL added_lift_slope2 = new LiftSlopeBL(2, added_lift1.LiftID, added_slope2.SlopeID);
            LiftSlopeBL added_lift_slope4 = new LiftSlopeBL(4, added_lift2.LiftID, added_slope2.SlopeID);


            await rep.Add(added_lift_slope1);
            await rep.Add(added_lift_slope2);
            await rep.Add(added_lift_slope4);

            var tmp1 = await rep.GetList();
            var tmp2 = await lift_rep.GetList();
            var tmp3 = await slope_rep.GetSlopes();

            List<LiftBL> from_slope1 = await rep.GetLiftsBySlopeId(added_slope1.SlopeID);
            Assert.Equal(1, from_slope1.Count);
            Assert.Equal(added_lift1, from_slope1[0]);

            List<LiftBL> from_slope2 = await rep.GetLiftsBySlopeId(added_slope2.SlopeID);
            Assert.Equal(2, from_slope2.Count);
            Assert.Equal(added_lift1, from_slope2[0]);
            Assert.Equal(added_lift2, from_slope2[1]);

            List<LiftBL> from_slope3 = await rep.GetLiftsBySlopeId(added_slope3.SlopeID);
            Assert.Equal(0, from_slope3.Count);



            List<SlopeBL> from_lift1 = await rep.GetSlopesByLiftId(added_lift1.LiftID);
            Assert.Equal(2, from_lift1.Count);
            Assert.Equal(added_slope1, from_lift1[0]);
            Assert.Equal(added_slope2, from_lift1[1]);

            List<SlopeBL> from_lift2 = await rep.GetSlopesByLiftId(added_lift2.LiftID);
            Assert.Equal(1, from_lift2.Count);
            Assert.Equal(added_slope2, from_lift2[0]);


            lift_rep.Delete(added_lift1);
            lift_rep.Delete(added_lift2);
            slope_rep.Delete(added_slope1);
            slope_rep.Delete(added_slope2);
            slope_rep.Delete(added_slope3);
            rep.Delete(added_lift_slope1);
            rep.Delete(added_lift_slope2);
            rep.Delete(added_lift_slope4);

            Assert.Empty(await lift_rep.GetList());
            Assert.Empty(await slope_rep.GetSlopes());
            Assert.Empty(await rep.GetList());

            LiftSlopeBL tmp5 = await rep.AddAutoIncrement(added_lift_slope1);
            Assert.True(1 == tmp5.RecordID);
            LiftSlopeBL tmp6 = await rep.AddAutoIncrement(added_lift_slope1);
            Assert.True(2 == tmp6.RecordID);
            await rep.Delete(tmp5);
            await rep.Delete(tmp6);
            Assert.Empty(await rep.GetList());

        }
    }
}
