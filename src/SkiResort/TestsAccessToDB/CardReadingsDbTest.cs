using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System;

using BL.Models;
using BL.IRepositories;

using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions;
using AccessToDB;


namespace Tests
{
    public class CardReadingsDbTest
    {
        TarantoolContext _context;
        private readonly ITestOutputHelper output;

        public CardReadingsDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetCardReadingsAsync());

            // add correct
            CardReading added_card_reading = new CardReading(1, 1, 1, DateTimeOffset.FromUnixTimeSeconds(1));
            await rep.AddCardReadingAsync(added_card_reading);
            // add already existing
            await Assert.ThrowsAsync<CardReadingException>(() => rep.AddCardReadingAsync(added_card_reading));


            // delete correct
            await rep.DeleteCardReadingAsync(added_card_reading);
            // delete not existing
            await Assert.ThrowsAsync<CardReadingException>(() => rep.DeleteCardReadingAsync(added_card_reading));

            // end tests - empty getlist
            Assert.Empty(await rep.GetCardReadingsAsync());
        }


        [Fact]
        public async Task Test_Add_GetByLLiftIdFromDate_Delete()
        {
            ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_context);
            Assert.Empty(await rep.GetCardReadingsAsync());


            ILiftsRepository lifts_rep = new TarantoolLiftsRepository(_context);
            Assert.Empty(await lifts_rep.GetLiftsAsync());
            Lift added_lift1 = new Lift(1, "A1", true, 100, 60, 360);
            await lifts_rep.AddLiftAsync(added_lift1);
            Lift added_lift2 = new Lift(2, "A2", false, 20, 10, 30);
            await lifts_rep.AddLiftAsync(added_lift2);

            ITurnstilesRepository turnstiles_rep = new TarantoolTurnstilesRepository(_context);
            Assert.Empty(await turnstiles_rep.GetTurnstilesAsync());
            // не тот подъемник
            Turnstile added_turnstile1 = new Turnstile(1, added_lift1.LiftID, true);
            await turnstiles_rep.AddTurnstileAsync(added_turnstile1);

            // тот подъеммник
            Turnstile added_turnstile2 = new Turnstile(2, added_lift2.LiftID, false);
            await turnstiles_rep.AddTurnstileAsync(added_turnstile2);
            Turnstile added_turnstile3 = new Turnstile(3, added_lift2.LiftID, false);
            await turnstiles_rep.AddTurnstileAsync(added_turnstile3);

            uint exact_time = 10;

            // не тот подъемник
            CardReading added_card_reading1 = new CardReading(1, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            await rep.AddCardReadingAsync(added_card_reading1);
            CardReading added_card_reading2 = new CardReading(2, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            await rep.AddCardReadingAsync(added_card_reading2);

            // тот подъемник но не то время
            CardReading added_card_reading3 = new CardReading(3, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            await rep.AddCardReadingAsync(added_card_reading3);

            // подходят
            CardReading added_card_reading4 = new CardReading(4, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            await rep.AddCardReadingAsync(added_card_reading4);
            CardReading added_card_reading5 = new CardReading(5, added_turnstile3.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            await rep.AddCardReadingAsync(added_card_reading5);

            uint card_readings_amount = await rep.CountForLiftIdFromDateAsync(added_lift2.LiftID, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            Assert.True(card_readings_amount == 2);

            card_readings_amount = await rep.CountForLiftIdFromDateAsync(added_lift1.LiftID, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            Assert.True(card_readings_amount == 1);

            card_readings_amount = await rep.CountForLiftIdFromDateAsync(added_lift1.LiftID, DateTimeOffset.FromUnixTimeSeconds(exact_time + 2));
            Assert.True(card_readings_amount == 0);

            var tmp = await rep.GetCardReadingsAsync();

            await rep.DeleteCardReadingAsync(added_card_reading1);
            await rep.DeleteCardReadingAsync(added_card_reading2);
            await rep.DeleteCardReadingAsync(added_card_reading3);
            await rep.DeleteCardReadingAsync(added_card_reading4);
            await rep.DeleteCardReadingAsync(added_card_reading5);
            Assert.Empty(await rep.GetCardReadingsAsync());

            await lifts_rep.DeleteLiftAsync(added_lift1);
            await lifts_rep.DeleteLiftAsync(added_lift2);
            Assert.Empty(await lifts_rep.GetLiftsAsync());

            await turnstiles_rep.DeleteTurnstileAsync(added_turnstile1);
            await turnstiles_rep.DeleteTurnstileAsync(added_turnstile2);
            await turnstiles_rep.DeleteTurnstileAsync(added_turnstile3);
            Assert.Empty(await turnstiles_rep.GetTurnstilesAsync());

            CardReading tmp2 = await rep.AddCardReadingAutoIncrementAsync(added_card_reading1);
            Assert.True(1 == tmp2.RecordID);
            CardReading tmp3 = await rep.AddCardReadingAutoIncrementAsync(added_card_reading1);
            Assert.True(2 == tmp3.RecordID);
            await rep.DeleteCardReadingAsync(tmp2);
            await rep.DeleteCardReadingAsync(tmp3);
            Assert.Empty(await rep.GetCardReadingsAsync());

        }
    }
}


