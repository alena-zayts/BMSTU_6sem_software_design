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
    public class SlopesDbTest
    {
        ContextTarantool _context;
        private readonly ITestOutputHelper output;

        public SlopesDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ISlopesRepository rep = new TarantoolSlopesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetSlopes());

            // add correct
            SlopeBL added_slope = new SlopeBL(1, "A1", true, 10);
            await rep.Add(added_slope);
            // add already existing
            await Assert.ThrowsAsync<SlopeDBException>(() => rep.Add(added_slope));

            // get_by_id correct
            SlopeBL got_slope = await rep.GetById(added_slope.SlopeID);
            Assert.Equal(added_slope, got_slope);
            // get_by_name correct
            got_slope = await rep.GetByName(added_slope.SlopeName);
            Assert.Equal(added_slope, got_slope);

            // delete correct
            await rep.Delete(added_slope);

            // get_by_id not existing
            await Assert.ThrowsAsync<SlopeDBException>(() => rep.GetById(added_slope.SlopeID));
            // get_by_id incorrect
            await Assert.ThrowsAsync<SlopeDBException>(() => rep.GetByName(added_slope.SlopeName));

            // delete not existing
            await Assert.ThrowsAsync<SlopeDBException>(() => rep.Delete(added_slope));

            // end tests - empty getlist
            Assert.Empty(await rep.GetSlopes());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ISlopesRepository rep = new TarantoolSlopesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetSlopes());

            SlopeBL added_slope1 = new SlopeBL(1, "A1", true, 10);
            await rep.Add(added_slope1);

            SlopeBL added_slope2 = new SlopeBL(2, "B2", false, 20);
            await rep.Add(added_slope2);

            added_slope2.SlopeName = "dfd";
            added_slope1.IsOpen = !added_slope1.IsOpen;

            // updates correct
            await rep.Update(added_slope1);
            await rep.Update(added_slope2);

            var list = await rep.GetSlopes();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_slope1, list[0]);
            Assert.Equal(added_slope2, list[1]);

            await rep.Delete(added_slope1);
            await rep.Delete(added_slope2);


            // updates not existing
            await Assert.ThrowsAsync<SlopeDBException>(() => rep.Update(added_slope1));
            await Assert.ThrowsAsync<SlopeDBException>(() => rep.Update(added_slope2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetSlopes());


            SlopeBL tmp2 = await rep.AddAutoIncrement(added_slope1);
            Assert.True(1 == tmp2.SlopeID);
            SlopeBL tmp3 = await rep.AddAutoIncrement(added_slope2);
            Assert.True(2 == tmp3.SlopeID);
            await rep.Delete(tmp2);
            await rep.Delete(tmp3);
            Assert.Empty(await rep.GetSlopes());
        }
    }
}


