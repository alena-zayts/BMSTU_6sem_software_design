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
            await rep.AddTurnstileAsync(added_turnstile);
            // add already existing
            await Assert.ThrowsAsync<TurnstileException>(() => rep.AddTurnstileAsync(added_turnstile));

            // get_by_id correct
            Turnstile got_turnstile = await rep.GetTurnstileByIdAsync(added_turnstile.TurnstileID);
            Assert.Equal(added_turnstile, got_turnstile);

            // delete correct
            await rep.DeleteTurnstileAsync(added_turnstile);

            // get_by_id not existing
            await Assert.ThrowsAsync<TurnstileException>(() => rep.GetTurnstileByIdAsync(added_turnstile.TurnstileID));

            // delete not existing
            await Assert.ThrowsAsync<TurnstileException>(() => rep.DeleteTurnstileAsync(added_turnstile));

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
            await rep.AddTurnstileAsync(added_turnstile1);

            Turnstile added_turnstile2 = new Turnstile(2, 2, false);
            await rep.AddTurnstileAsync(added_turnstile2);

            added_turnstile2 = new Turnstile(added_turnstile2.TurnstileID, added_turnstile2.LiftID, !added_turnstile2.IsOpen);

            // updates correct
            await rep.UpdateTurnstileAsync(added_turnstile1);
            await rep.UpdateTurnstileAsync(added_turnstile2);

            var list = await rep.GetTurnstilesAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_turnstile1, list[0]);
            Assert.Equal(added_turnstile2, list[1]);


            // by lift id
            Turnstile added_turnstile3 = new Turnstile(3, LiftID, true);
            await rep.AddTurnstileAsync(added_turnstile3);
            list = await rep.GetTurnstilesByLiftIdAsync(LiftID);
            Assert.Equal(2, list.Count);
            Assert.Equal(added_turnstile1, list[0]);
            Assert.Equal(added_turnstile3, list[1]);
            Assert.Empty(await rep.GetTurnstilesByLiftIdAsync(999));




            await rep.DeleteTurnstileAsync(added_turnstile1);
            await rep.DeleteTurnstileAsync(added_turnstile2);
            await rep.DeleteTurnstileAsync(added_turnstile3);


            // updates not existing
            await Assert.ThrowsAsync<TurnstileException>(() => rep.UpdateTurnstileAsync(added_turnstile1));
            await Assert.ThrowsAsync<TurnstileException>(() => rep.UpdateTurnstileAsync(added_turnstile2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetTurnstilesAsync());



            Turnstile tmp2 = await rep.AddTurnstileAutoIncrementAsync(added_turnstile1);
            Assert.True(1 == tmp2.TurnstileID);
            Turnstile tmp3 = await rep.AddTurnstileAutoIncrementAsync(added_turnstile1);
            Assert.True(2 == tmp3.TurnstileID);
            await rep.DeleteTurnstileAsync(tmp2);
            await rep.DeleteTurnstileAsync(tmp3);
            Assert.Empty(await rep.GetTurnstilesAsync());
        }
    }
}
