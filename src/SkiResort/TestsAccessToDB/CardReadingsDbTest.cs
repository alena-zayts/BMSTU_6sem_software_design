using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System;

using BL.Models;
using BL.IRepositories;

using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions.CardReadingExceptions;
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
            await rep.AddCardReadingAsync(added_card_reading.RecordID, added_card_reading.TurnstileID, added_card_reading.CardID, added_card_reading.ReadingTime);
            // add already existing
            await Assert.ThrowsAsync<CardReadingAddException>(() => rep.AddCardReadingAsync(added_card_reading.RecordID, added_card_reading.TurnstileID, added_card_reading.CardID, added_card_reading.ReadingTime));


            // delete correct
            await rep.DeleteCardReadingAsync(added_card_reading.RecordID);
            // delete not existing
            await Assert.ThrowsAsync<CardReadingDeleteException>(() => rep.DeleteCardReadingAsync(added_card_reading.RecordID));

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
            await lifts_rep.AddLiftAsync(added_lift1.LiftID, added_lift1.LiftName, added_lift1.IsOpen, added_lift1.SeatsAmount, added_lift1.LiftingTime, added_lift1.QueueTime);
            Lift added_lift2 = new Lift(2, "A2", false, 20, 10, 30);
            await lifts_rep.AddLiftAsync(added_lift2.LiftID, added_lift2.LiftName, added_lift2.IsOpen, added_lift2.SeatsAmount, added_lift2.LiftingTime, added_lift2.QueueTime);

            ITurnstilesRepository turnstiles_rep = new TarantoolTurnstilesRepository(_context);
            Assert.Empty(await turnstiles_rep.GetTurnstilesAsync());
            // �� ��� ���������
            Turnstile added_turnstile1 = new Turnstile(1, added_lift1.LiftID, true);
            await turnstiles_rep.AddTurnstileAsync(added_turnstile1.TurnstileID, added_turnstile1.LiftID, added_turnstile1.IsOpen);

            // ��� ����������
            Turnstile added_turnstile2 = new Turnstile(2, added_lift2.LiftID, false);
            await turnstiles_rep.AddTurnstileAsync(added_turnstile2.TurnstileID, added_turnstile2.LiftID, added_turnstile2.IsOpen);
            Turnstile added_turnstile3 = new Turnstile(3, added_lift2.LiftID, false);
            await turnstiles_rep.AddTurnstileAsync(added_turnstile3.TurnstileID, added_turnstile3.LiftID, added_turnstile3.IsOpen);

            uint exact_time = 10;

            // �� ��� ���������
            CardReading added_card_reading1 = new CardReading(1, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            await rep.AddCardReadingAsync(added_card_reading1.RecordID, added_card_reading1.TurnstileID, added_card_reading1.CardID,  added_card_reading1.ReadingTime);
            CardReading added_card_reading2 = new CardReading(2, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            await rep.AddCardReadingAsync(added_card_reading2.RecordID, added_card_reading2.TurnstileID, added_card_reading2.CardID, added_card_reading2.ReadingTime);

            // ��� ��������� �� �� �� �����
            CardReading added_card_reading3 = new CardReading(3, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            await rep.AddCardReadingAsync(added_card_reading3.RecordID, added_card_reading3.TurnstileID, added_card_reading3.CardID, added_card_reading3.ReadingTime);

            // ��������
            CardReading added_card_reading4 = new CardReading(4, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            await rep.AddCardReadingAsync(added_card_reading4.RecordID, added_card_reading4.TurnstileID, added_card_reading4.CardID, added_card_reading4.ReadingTime);
            CardReading added_card_reading5 = new CardReading(5, added_turnstile3.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            await rep.AddCardReadingAsync(added_card_reading5.RecordID, added_card_reading5.TurnstileID, added_card_reading5.CardID, added_card_reading5.ReadingTime);

            uint card_readings_amount = await rep.CountForLiftIdFromDateAsync(added_lift2.LiftID, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            Assert.True(card_readings_amount == 2);

            card_readings_amount = await rep.CountForLiftIdFromDateAsync(added_lift1.LiftID, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            Assert.True(card_readings_amount == 1);

            card_readings_amount = await rep.CountForLiftIdFromDateAsync(added_lift1.LiftID, DateTimeOffset.FromUnixTimeSeconds(exact_time + 2));
            Assert.True(card_readings_amount == 0);

            var tmp = await rep.GetCardReadingsAsync();

            await rep.DeleteCardReadingAsync(added_card_reading1.RecordID);
            await rep.DeleteCardReadingAsync(added_card_reading2.RecordID);
            await rep.DeleteCardReadingAsync(added_card_reading3.RecordID);
            await rep.DeleteCardReadingAsync(added_card_reading4.RecordID);
            await rep.DeleteCardReadingAsync(added_card_reading5.RecordID);
            Assert.Empty(await rep.GetCardReadingsAsync());

            await lifts_rep.DeleteLiftByIDAsync(added_lift1.LiftID);
            await lifts_rep.DeleteLiftByIDAsync(added_lift2.LiftID);
            Assert.Empty(await lifts_rep.GetLiftsAsync());

            await turnstiles_rep.DeleteTurnstileByIDAsync(added_turnstile1.TurnstileID);
            await turnstiles_rep.DeleteTurnstileByIDAsync(added_turnstile2.TurnstileID);
            await turnstiles_rep.DeleteTurnstileByIDAsync(added_turnstile3.TurnstileID);
            Assert.Empty(await turnstiles_rep.GetTurnstilesAsync());

            uint tmpCardReadingID2 = await rep.AddCardReadingAutoIncrementAsync(added_card_reading1.TurnstileID, added_card_reading1.CardID, added_card_reading1.ReadingTime);
            Assert.True(1 == tmpCardReadingID2);
            uint tmpCardReadingID3 = await rep.AddCardReadingAutoIncrementAsync(added_card_reading1.TurnstileID, added_card_reading1.CardID, added_card_reading1.ReadingTime);
            Assert.True(2 == tmpCardReadingID3);
            await rep.DeleteCardReadingAsync(tmpCardReadingID2);
            await rep.DeleteCardReadingAsync(tmpCardReadingID3);
            Assert.Empty(await rep.GetCardReadingsAsync());

        }
    }
}


