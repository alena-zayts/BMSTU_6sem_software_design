using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL.Models;
using BL.IRepositories;


using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions.TurnstileExceptions;
using AccessToDB;



namespace Tests
{
    public class TurnstilesDbTest
    { 
        TarantoolContext _context;
        private readonly ITestOutputHelper output;

        public TurnstilesDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetTurnstilesAsync());

            // add correct
            Turnstile added_turnstile = new Turnstile(1, 2, true);
            await rep.AddTurnstileAsync(added_turnstile.TurnstileID, added_turnstile.LiftID, added_turnstile.IsOpen);
            // add already existing
            await Assert.ThrowsAsync<TurnstileAddException>(() => rep.AddTurnstileAsync(added_turnstile.TurnstileID, added_turnstile.LiftID, added_turnstile.IsOpen));

            // get_by_id correct
            Turnstile got_turnstile = await rep.GetTurnstileByIdAsync(added_turnstile.TurnstileID);
            Assert.Equal(added_turnstile, got_turnstile);

            // delete correct
            await rep.DeleteTurnstileByIDAsync(added_turnstile.TurnstileID);

            // get_by_id not existing
            await Assert.ThrowsAsync<TurnstileNotFoundException>(() => rep.GetTurnstileByIdAsync(added_turnstile.TurnstileID));

            // delete not existing
            await Assert.ThrowsAsync<TurnstileDeleteException>(() => rep.DeleteTurnstileByIDAsync(added_turnstile.TurnstileID));

            // end tests - empty getlist
            Assert.Empty(await rep.GetTurnstilesAsync());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetTurnstilesAsync());

            uint LiftID = 10;

            Turnstile added_turnstile1 = new Turnstile(1, LiftID, true);
            await rep.AddTurnstileAsync(added_turnstile1.TurnstileID, added_turnstile1.LiftID, added_turnstile1.IsOpen);

            Turnstile added_turnstile2 = new Turnstile(2, 2, false);
            await rep.AddTurnstileAsync(added_turnstile2.TurnstileID, added_turnstile2.LiftID, added_turnstile2.IsOpen);

            added_turnstile2 = new Turnstile(added_turnstile2.TurnstileID, added_turnstile2.LiftID, !added_turnstile2.IsOpen);

            // updates correct
            await rep.UpdateTurnstileByIDAsync(added_turnstile1.TurnstileID, added_turnstile1.LiftID, added_turnstile1.IsOpen);
            await rep.UpdateTurnstileByIDAsync(added_turnstile2.TurnstileID, added_turnstile2.LiftID, added_turnstile2.IsOpen);

            var list = await rep.GetTurnstilesAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_turnstile1, list[0]);
            Assert.Equal(added_turnstile2, list[1]);


            // by lift id
            Turnstile added_turnstile3 = new Turnstile(3, LiftID, true);
            await rep.AddTurnstileAsync(added_turnstile3.TurnstileID, added_turnstile3.LiftID, added_turnstile3.IsOpen);
            list = await rep.GetTurnstilesByLiftIdAsync(LiftID);
            Assert.Equal(2, list.Count);
            Assert.Equal(added_turnstile1, list[0]);
            Assert.Equal(added_turnstile3, list[1]);
            Assert.Empty(await rep.GetTurnstilesByLiftIdAsync(999));




            await rep.DeleteTurnstileByIDAsync(added_turnstile1.TurnstileID);
            await rep.DeleteTurnstileByIDAsync(added_turnstile2.TurnstileID);
            await rep.DeleteTurnstileByIDAsync(added_turnstile3.TurnstileID);


            // updates not existing
            await Assert.ThrowsAsync<TurnstileUpdateException>(() => rep.UpdateTurnstileByIDAsync(added_turnstile1.TurnstileID, added_turnstile1.LiftID, added_turnstile1.IsOpen));
            await Assert.ThrowsAsync<TurnstileUpdateException>(() => rep.UpdateTurnstileByIDAsync(added_turnstile2.TurnstileID, added_turnstile2.LiftID, added_turnstile2.IsOpen));


            // end tests - empty getlist
            Assert.Empty(await rep.GetTurnstilesAsync());



            uint tmpTurnstileID2 = await rep.AddTurnstileAutoIncrementAsync(added_turnstile1.LiftID, added_turnstile1.IsOpen);
            Assert.True(1 == tmpTurnstileID2);
            uint tmpTurnstileID3 = await rep.AddTurnstileAutoIncrementAsync(added_turnstile2.LiftID, added_turnstile2.IsOpen);
            Assert.True(2 == tmpTurnstileID3);
            await rep.DeleteTurnstileByIDAsync(tmpTurnstileID2);
            await rep.DeleteTurnstileByIDAsync(tmpTurnstileID3);
            Assert.Empty(await rep.GetTurnstilesAsync());
        }
    }
}
