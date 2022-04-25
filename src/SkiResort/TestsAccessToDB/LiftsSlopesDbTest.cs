using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL.Models;
using BL.IRepositories;


using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions;
using AccessToDB;



namespace Tests
{
    public class LiftsSlopesDbTest
    {
        TarantoolContext _context;
        private readonly ITestOutputHelper output;

        public LiftsSlopesDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetLiftSlopeByIdAsync_DeleteLiftSlopeAsync()
        {
            ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetLiftsSlopesAsync());

            // add correct
            LiftSlope added_lift_slope = new LiftSlope(1, 1, 1);
            await rep.AddLiftSlopeAsync(added_lift_slope);
            // add already existing
            await Assert.ThrowsAsync<LiftSlopeException>(() => rep.AddLiftSlopeAsync(added_lift_slope));

            // get_by_id correct
            LiftSlope got_lift_slope = await rep.GetLiftSlopeByIdAsync(added_lift_slope.RecordID);
            Assert.Equal(added_lift_slope, got_lift_slope);

            // DeleteLiftSlopeAsync correct
            await rep.DeleteLiftSlopeAsync(added_lift_slope);

            // get_by_id not existing
            await Assert.ThrowsAsync<LiftSlopeException>(() => rep.GetLiftSlopeByIdAsync(added_lift_slope.RecordID));

            // DeleteLiftSlopeAsync not existing
            await Assert.ThrowsAsync<LiftSlopeException>(() => rep.DeleteLiftSlopeAsync(added_lift_slope));

            // end tests - empty getlist
            Assert.Empty(await rep.GetLiftsSlopesAsync());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetLiftsSlopesAsync());

            LiftSlope added_lift_slope1 = new LiftSlope(1, 1, 1);
            await rep.AddLiftSlopeAsync(added_lift_slope1);

            LiftSlope added_lift_slope2 = new LiftSlope(2, 2, 1);
            await rep.AddLiftSlopeAsync(added_lift_slope2);

            added_lift_slope2 = new LiftSlope(added_lift_slope2.RecordID, 100, 200);
            await rep.UpdateLiftSlopeAsync(added_lift_slope2);

            // updates correct
            Assert.Contains(added_lift_slope1, await rep.GetLiftsSlopesAsync());
            Assert.Contains(added_lift_slope2, await rep.GetLiftsSlopesAsync());

            var list = await rep.GetLiftsSlopesAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_lift_slope1, list[0]);
            Assert.Equal(added_lift_slope2, list[1]);

            await rep.DeleteLiftSlopeAsync(added_lift_slope1);
            await rep.DeleteLiftSlopeAsync(added_lift_slope2);


            // updates not existing
            await Assert.ThrowsAsync<LiftSlopeException>(() => rep.UpdateLiftSlopeAsync(added_lift_slope1));
            await Assert.ThrowsAsync<LiftSlopeException>(() => rep.UpdateLiftSlopeAsync(added_lift_slope2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetLiftsSlopesAsync());
        }

        //[Fact]
        public async Task Test_Add_GetByOther_DeleteLiftSlopeAsync()
        {
            ILiftsRepository lift_rep = new TarantoolLiftsRepository(_context);
            Assert.Empty(await lift_rep.GetLiftsAsync());


            Lift added_lift1 = new Lift(100000, "A1", true, 100, 60, 360);
            await lift_rep.AddLiftAsync(added_lift1.LiftID, added_lift1.LiftName, added_lift1.IsOpen, added_lift1.SeatsAmount, added_lift1.LiftingTime, added_lift1.QueueTime);
            Lift added_lift2 = new Lift(200000, "A2", false, 20, 10, 30);
            await lift_rep.AddLiftAsync(added_lift2.LiftID, added_lift2.LiftName, added_lift2.IsOpen, added_lift2.SeatsAmount, added_lift2.LiftingTime, added_lift2.QueueTime);

            ISlopesRepository slope_rep = new TarantoolSlopesRepository(_context);
            Assert.Empty(await slope_rep.GetSlopesAsync());

            Slope added_slope1 = new Slope(1, "A1", true, 1);
            await slope_rep.AddSlopeAsync(added_slope1);
            Slope added_slope2 = new Slope(2, "A2", false, 20);
            await slope_rep.AddSlopeAsync(added_slope2);
            Slope added_slope3 = new Slope(3, "A3", true, 5);
            await slope_rep.AddSlopeAsync(added_slope3);


            ILiftsSlopesRepository rep = new TarantoolLiftsSlopesRepository(_context);
            Assert.Empty(await rep.GetLiftsSlopesAsync());
            LiftSlope added_lift_slope1 = new LiftSlope(1, added_lift1.LiftID, added_slope1.SlopeID);
            LiftSlope added_lift_slope2 = new LiftSlope(2, added_lift1.LiftID, added_slope2.SlopeID);
            LiftSlope added_lift_slope4 = new LiftSlope(4, added_lift2.LiftID, added_slope2.SlopeID);


            await rep.AddLiftSlopeAsync(added_lift_slope1);
            await rep.AddLiftSlopeAsync(added_lift_slope2);
            await rep.AddLiftSlopeAsync(added_lift_slope4);

            var tmp1 = await rep.GetLiftsSlopesAsync();
            var tmp2 = await lift_rep.GetLiftsAsync();
            var tmp3 = await slope_rep.GetSlopesAsync();

            List<Lift> from_slope1 = await rep.GetLiftsBySlopeIdAsync(added_slope1.SlopeID);
            Assert.Equal(1, from_slope1.Count);
            Assert.Equal(added_lift1, from_slope1[0]);

            List<Lift> from_slope2 = await rep.GetLiftsBySlopeIdAsync(added_slope2.SlopeID);
            Assert.Equal(2, from_slope2.Count);
            Assert.Equal(added_lift1, from_slope2[0]);
            Assert.Equal(added_lift2, from_slope2[1]);

            List<Lift> from_slope3 = await rep.GetLiftsBySlopeIdAsync(added_slope3.SlopeID);
            Assert.Equal(0, from_slope3.Count);



            List<Slope> from_lift1 = await rep.GetSlopesByLiftIdAsync(added_lift1.LiftID);
            Assert.Equal(2, from_lift1.Count);
            Assert.Equal(added_slope1, from_lift1[0]);
            Assert.Equal(added_slope2, from_lift1[1]);

            List<Slope> from_lift2 = await rep.GetSlopesByLiftIdAsync(added_lift2.LiftID);
            Assert.Equal(1, from_lift2.Count);
            Assert.Equal(added_slope2, from_lift2[0]);


            lift_rep.DeleteLiftByIDAsync(added_lift1.LiftID);
            lift_rep.DeleteLiftByIDAsync(added_lift2.LiftID);
            slope_rep.DeleteSlopeAsync(added_slope1);
            slope_rep.DeleteSlopeAsync(added_slope2);
            slope_rep.DeleteSlopeAsync(added_slope3);
            rep.DeleteLiftSlopeAsync(added_lift_slope1);
            rep.DeleteLiftSlopeAsync(added_lift_slope2);
            rep.DeleteLiftSlopeAsync(added_lift_slope4);

            Assert.Empty(await lift_rep.GetLiftsAsync());
            Assert.Empty(await slope_rep.GetSlopesAsync());
            Assert.Empty(await rep.GetLiftsSlopesAsync());

            LiftSlope tmp5 = await rep.AddLiftSlopeAutoIncrementAsync(added_lift_slope1);
            Assert.True(1 == tmp5.RecordID);
            LiftSlope tmp6 = await rep.AddLiftSlopeAutoIncrementAsync(added_lift_slope1);
            Assert.True(2 == tmp6.RecordID);
            await rep.DeleteLiftSlopeAsync(tmp5);
            await rep.DeleteLiftSlopeAsync(tmp6);
            Assert.Empty(await rep.GetLiftsSlopesAsync());

        }
    }
}
