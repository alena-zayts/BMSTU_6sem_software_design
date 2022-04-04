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
    public class CardReadingsDbTest
    {
        ContextTarantool _context;
        private readonly ITestOutputHelper output;

        public CardReadingsDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            // add correct
            CardReadingBL added_card_reading = new CardReadingBL(1, 1, 1, 1);
            await rep.Add(added_card_reading);
            // add already existing
            await Assert.ThrowsAsync<CardReadingDBException>(() => rep.Add(added_card_reading));


            // delete correct
            await rep.Delete(added_card_reading);
            // delete not existing
            await Assert.ThrowsAsync<CardReadingDBException>(() => rep.Delete(added_card_reading));

            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }


        [Fact]
        public async Task Test_Add_GetByLLiftIdFromDate_Delete()
        {
            ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_context);
            Assert.Empty(await rep.GetList());


            ILiftsRepository lifts_rep = new TarantoolLiftsRepository(_context);
            Assert.Empty(await lifts_rep.GetList());
            LiftBL added_lift1 = new LiftBL(1, "A1", true, 100, 60, 360);
            await lifts_rep.Add(added_lift1);
            LiftBL added_lift2 = new LiftBL(2, "A2", false, 20, 10, 30);
            await lifts_rep.Add(added_lift2);

            ITurnstilesRepository turnstiles_rep = new TarantoolTurnstilesRepository(_context);
            Assert.Empty(await turnstiles_rep.GetList());
            // не тот подъемник
            TurnstileBL added_turnstile1 = new TurnstileBL(1, added_lift1.lift_id, true);
            await turnstiles_rep.Add(added_turnstile1);

            // тот подъеммник
            TurnstileBL added_turnstile2 = new TurnstileBL(2, added_lift2.lift_id, false);
            await turnstiles_rep.Add(added_turnstile2);
            TurnstileBL added_turnstile3 = new TurnstileBL(3, added_lift2.lift_id, false);
            await turnstiles_rep.Add(added_turnstile3);

            uint exact_time = 10;

            // не тот подъемник
            CardReadingBL added_card_reading1 = new CardReadingBL(1, added_turnstile1.turnstile_id, 9, exact_time - 1);
            await rep.Add(added_card_reading1);
            CardReadingBL added_card_reading2 = new CardReadingBL(2, added_turnstile1.turnstile_id, 9, exact_time + 1);
            await rep.Add(added_card_reading2);

            // тот подъемник но не то время
            CardReadingBL added_card_reading3 = new CardReadingBL(3, added_turnstile2.turnstile_id, 9, exact_time - 1);
            await rep.Add(added_card_reading3);

            // подходят
            CardReadingBL added_card_reading4 = new CardReadingBL(4, added_turnstile2.turnstile_id, 9, exact_time + 1);
            await rep.Add(added_card_reading4);
            CardReadingBL added_card_reading5 = new CardReadingBL(5, added_turnstile3.turnstile_id, 9, exact_time);
            await rep.Add(added_card_reading5);

            uint card_readings_amount = await rep.CountForLiftIdFromDate(added_lift2.lift_id, exact_time);
            Assert.True(card_readings_amount == 2);

            card_readings_amount = await rep.CountForLiftIdFromDate(added_lift1.lift_id, exact_time);
            Assert.True(card_readings_amount == 1);

            card_readings_amount = await rep.CountForLiftIdFromDate(added_lift1.lift_id, exact_time + 2);
            Assert.True(card_readings_amount == 0);

            var tmp = await rep.GetList();

            await rep.Delete(added_card_reading1);
            await rep.Delete(added_card_reading2);
            await rep.Delete(added_card_reading3);
            await rep.Delete(added_card_reading4);
            await rep.Delete(added_card_reading5);
            Assert.Empty(await rep.GetList());

            await lifts_rep.Delete(added_lift1);
            await lifts_rep.Delete(added_lift2);
            Assert.Empty(await lifts_rep.GetList());

            await turnstiles_rep.Delete(added_turnstile1);
            await turnstiles_rep.Delete(added_turnstile2);
            await turnstiles_rep.Delete(added_turnstile3);
            Assert.Empty(await turnstiles_rep.GetList());

        }



    }
}


