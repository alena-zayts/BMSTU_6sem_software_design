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
    public class TurnstilesDbTest
    { 
        ContextTarantool _context;
        private readonly ITestOutputHelper output;

        public TurnstilesDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetTurnstilesAsync());

            // add correct
            TurnstileBL added_turnstile = new TurnstileBL(1, 2, true);
            await rep.Add(added_turnstile);
            // add already existing
            await Assert.ThrowsAsync<TurnstileDBException>(() => rep.Add(added_turnstile));

            // get_by_id correct
            TurnstileBL got_turnstile = await rep.GetById(added_turnstile.TurnstileID);
            Assert.Equal(added_turnstile, got_turnstile);

            // delete correct
            await rep.Delete(added_turnstile);

            // get_by_id not existing
            await Assert.ThrowsAsync<TurnstileDBException>(() => rep.GetById(added_turnstile.TurnstileID));

            // delete not existing
            await Assert.ThrowsAsync<TurnstileDBException>(() => rep.Delete(added_turnstile));

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

            TurnstileBL added_turnstile1 = new TurnstileBL(1, LiftID, true);
            await rep.Add(added_turnstile1);

            TurnstileBL added_turnstile2 = new TurnstileBL(2, 2, false);
            await rep.Add(added_turnstile2);

            added_turnstile2.IsOpen = false;

            // updates correct
            await rep.Update(added_turnstile1);
            await rep.Update(added_turnstile2);

            var list = await rep.GetTurnstilesAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_turnstile1, list[0]);
            Assert.Equal(added_turnstile2, list[1]);


            // by lift id
            TurnstileBL added_turnstile3 = new TurnstileBL(3, LiftID, true);
            await rep.Add(added_turnstile3);
            list = await rep.GetTurnstilesByLiftIdAsync(LiftID);
            Assert.Equal(2, list.Count);
            Assert.Equal(added_turnstile1, list[0]);
            Assert.Equal(added_turnstile3, list[1]);
            Assert.Empty(await rep.GetTurnstilesByLiftIdAsync(999));




            await rep.Delete(added_turnstile1);
            await rep.Delete(added_turnstile2);
            await rep.Delete(added_turnstile3);


            // updates not existing
            await Assert.ThrowsAsync<TurnstileDBException>(() => rep.Update(added_turnstile1));
            await Assert.ThrowsAsync<TurnstileDBException>(() => rep.Update(added_turnstile2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetTurnstilesAsync());



            TurnstileBL tmp2 = await rep.AddAutoIncrement(added_turnstile1);
            Assert.True(1 == tmp2.TurnstileID);
            TurnstileBL tmp3 = await rep.AddAutoIncrement(added_turnstile1);
            Assert.True(2 == tmp3.TurnstileID);
            await rep.Delete(tmp2);
            await rep.Delete(tmp3);
            Assert.Empty(await rep.GetTurnstilesAsync());
        }
    }
}
