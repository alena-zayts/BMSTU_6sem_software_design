using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;


using ComponentAccessToDB.RepositoriesTarantool;
using ComponentAccessToDB;



namespace Tests
{
    public class LiftsDbTest
    {
        ContextTarantool _context;
        private readonly ITestOutputHelper output;

        public LiftsDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ILiftsRepository rep = new TarantoolLiftsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            // add correct
            LiftBL added_lift = new LiftBL(1, "A1", true , 10, 100, 1000);
            await rep.Add(added_lift);
            // add already existing
            await Assert.ThrowsAsync<LiftDBException>(() => rep.Add(added_lift));

            // get_by_id correct
            LiftBL got_lift = await rep.GetById(added_lift.lift_id);
            Assert.Equal(added_lift, got_lift);
            // get_by_name correct
            got_lift = await rep.GetByName(added_lift.lift_name);
            Assert.Equal(added_lift, got_lift);

            // delete correct
            await rep.Delete(added_lift);

            // get_by_id not existing
            await Assert.ThrowsAsync<LiftDBException>(() => rep.GetById(added_lift.lift_id));
            // get_by_id incorrect
            await Assert.ThrowsAsync<LiftDBException>(() => rep.GetByName(added_lift.lift_name));

            // delete not existing
            await Assert.ThrowsAsync<LiftDBException>(() => rep.Delete(added_lift));

            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ILiftsRepository rep = new TarantoolLiftsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            LiftBL added_lift1 = new LiftBL(1, "A1", true, 10, 100, 1000);
            await rep.Add(added_lift1);

            LiftBL added_lift2 = new LiftBL(2, "B2", false, 20, 200, 2000);
            await rep.Add(added_lift2);

            added_lift2.lift_name = "dfd";
            added_lift1.is_open = !added_lift1.is_open;

            // updates correct
            await rep.Update(added_lift1);
            await rep.Update(added_lift2);

            var list = await rep.GetList();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_lift1, list[0]);
            Assert.Equal(added_lift2, list[1]);

            await rep.Delete(added_lift1);
            await rep.Delete(added_lift2);


            // updates not existing
            await Assert.ThrowsAsync<LiftDBException>(() => rep.Update(added_lift1));
            await Assert.ThrowsAsync<LiftDBException>(() => rep.Update(added_lift2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }
    }
}

