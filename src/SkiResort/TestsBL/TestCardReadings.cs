using Xunit;
using BL;
using Ninject;
using BL.Models;
using System.Threading.Tasks;
using BL.Exceptions;
using System.Collections.Generic;
using System;

namespace TestsBL
{
    public class TestCardReadings
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();
            Facade facade = new(ninjectKernel.Get<IRepositoriesFactory>());

            await TestUsersCreator.Create();


            Assert.Empty(await facade.GetLiftsInfoAsync(TestUsersCreator.unauthorizedID));
            Lift added_lift1 = new(1, "A1", true, 100, 60, 360);
            await facade.AdminAddLiftAsync(TestUsersCreator.adminID, added_lift1);
            Lift added_lift2 = new Lift(2, "A2", false, 20, 10, 30);
            uint added_lift2_id = await facade.AdminAddAutoIncrementLiftAsync(TestUsersCreator.adminID, added_lift2);
            added_lift2 = new Lift(added_lift2_id, added_lift2.LiftName, added_lift2.IsOpen, added_lift2.SeatsAmount, added_lift2.LiftingTime, added_lift2.QueueTime);


            // не тот подъемник
            Turnstile added_turnstile1 = new Turnstile(1, added_lift1.LiftID, true);
            await facade.AdminAddTurnstileAsync(TestUsersCreator.adminID, added_turnstile1);

            // тот подъеммник
            Turnstile added_turnstile2 = new Turnstile(2, added_lift2.LiftID, false);
            uint addedTurnstile2ID = await facade.AdminAddAutoIncrementTurnstileAsync(TestUsersCreator.adminID, added_turnstile2);
            added_turnstile2 = new(addedTurnstile2ID, added_turnstile2.LiftID, added_turnstile2.IsOpen);
            Turnstile added_turnstile3 = new Turnstile(3, added_lift2.LiftID, false);
            uint addedTurnstile3ID = await facade.AdminAddAutoIncrementTurnstileAsync(TestUsersCreator.adminID, added_turnstile3);
            added_turnstile3 = new(addedTurnstile3ID, added_turnstile3.LiftID, added_turnstile3.IsOpen);

            uint exact_time = 10;

            // не тот подъемник
            CardReading added_card_reading1 = new CardReading(1, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            await facade.AdminAddCardReadingAsync(TestUsersCreator.adminID, added_card_reading1);
            CardReading added_card_reading2 = new CardReading(2, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            uint tmpCardID;
            tmpCardID = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading2);
            added_card_reading2 = new CardReading(tmpCardID, added_card_reading2.TurnstileID, added_card_reading2.CardID, added_card_reading2.ReadingTime);


            // тот подъемник но не то время
            CardReading added_card_reading3 = new CardReading(3, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            tmpCardID = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading3);
            added_card_reading3 = new CardReading(tmpCardID, added_card_reading3.TurnstileID, added_card_reading3.CardID, added_card_reading3.ReadingTime);


            // подходят
            CardReading added_card_reading4 = new CardReading(4, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            tmpCardID = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading3);
            added_card_reading4 = new CardReading(tmpCardID, added_card_reading4.TurnstileID, added_card_reading4.CardID, added_card_reading4.ReadingTime);

            CardReading added_card_reading5 = new CardReading(5, added_turnstile3.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            tmpCardID = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading3);
            added_card_reading5 = new CardReading(tmpCardID, added_card_reading5.TurnstileID, added_card_reading5.CardID, added_card_reading5.ReadingTime);


            await facade.AdminDeleteCardReadingAsync(TestUsersCreator.adminID, added_card_reading1);
            await facade.AdminDeleteCardReadingAsync(TestUsersCreator.adminID, added_card_reading2);
            await facade.AdminDeleteCardReadingAsync(TestUsersCreator.adminID, added_card_reading3);
            await facade.AdminDeleteCardReadingAsync(TestUsersCreator.adminID, added_card_reading4);
            await facade.AdminDeleteCardReadingAsync(TestUsersCreator.adminID, added_card_reading5);

            await facade.AdminDeleteLiftAsync(TestUsersCreator.adminID, added_lift1);
            await facade.AdminDeleteLiftAsync(TestUsersCreator.adminID, added_lift2);
            Assert.Empty(await facade.GetLiftsInfoAsync(TestUsersCreator.authorizedID));

            await facade.AdminDeleteTurnstileAsync(TestUsersCreator.adminID, added_turnstile1);
            await facade.AdminDeleteTurnstileAsync(TestUsersCreator.adminID, added_turnstile2);
            await facade.AdminDeleteTurnstileAsync(TestUsersCreator.adminID, added_turnstile3);
        }
    }
}