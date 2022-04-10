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
    public class LiftsDbTest
    {
        TarantoolContext _context;
        private readonly ITestOutputHelper output;

        public LiftsDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ILiftsRepository rep = new TarantoolLiftsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetLiftsAsync());

            // add correct
            Lift added_lift = new Lift(1, "A1", true , 10, 100, 1000);
            await rep.AddLiftAsync(added_lift);
            // add already existing
            await Assert.ThrowsAsync<LiftException>(() => rep.AddLiftAsync(added_lift));

            // get_by_id correct
            Lift got_lift = await rep.GetLiftByIdAsync(added_lift.LiftID);
            Assert.Equal(added_lift, got_lift);
            // get_by_name correct
            got_lift = await rep.GetLiftByNameAsync(added_lift.LiftName);
            Assert.Equal(added_lift, got_lift);

            // delete correct
            await rep.DeleteLiftAsync(added_lift);

            // get_by_id not existing
            await Assert.ThrowsAsync<LiftException>(() => rep.GetLiftByIdAsync(added_lift.LiftID));
            // get_by_id incorrect
            await Assert.ThrowsAsync<LiftException>(() => rep.GetLiftByNameAsync(added_lift.LiftName));

            // delete not existing
            await Assert.ThrowsAsync<LiftException>(() => rep.DeleteLiftAsync(added_lift));

            // end tests - empty getlist
            Assert.Empty(await rep.GetLiftsAsync());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ILiftsRepository rep = new TarantoolLiftsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetLiftsAsync());

            Lift added_lift1 = new Lift(1, "A1", true, 10, 100, 1000);
            await rep.AddLiftAsync(added_lift1);

            Lift added_lift2 = new Lift(2, "B2", false, 20, 200, 2000);
            await rep.AddLiftAsync(added_lift2);

            added_lift2 = new Lift(added_lift2.LiftID, added_lift2.LiftName, added_lift2.IsOpen, 821, added_lift2.LiftingTime, added_lift2.QueueTime);

            // updates correct
            await rep.UpdateLiftAsync(added_lift1);
            await rep.UpdateLiftAsync(added_lift2);

            var list = await rep.GetLiftsAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_lift1, list[0]);
            Assert.Equal(added_lift2, list[1]);

            await rep.DeleteLiftAsync(added_lift1);
            await rep.DeleteLiftAsync(added_lift2);


            // updates not existing
            await Assert.ThrowsAsync<LiftException>(() => rep.UpdateLiftAsync(added_lift1));
            await Assert.ThrowsAsync<LiftException>(() => rep.UpdateLiftAsync(added_lift2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetLiftsAsync());


            Lift tmp2 = await rep.AddLiftAutoIncrementAsync(added_lift1);
            Assert.True(1 == tmp2.LiftID);
            Lift tmp3 = await rep.AddLiftAutoIncrementAsync(added_lift2);
            Assert.True(2 == tmp3.LiftID);
            await rep.DeleteLiftAsync(tmp2);
            await rep.DeleteLiftAsync(tmp3);
            Assert.Empty(await rep.GetLiftsAsync());
        }
    }
}

