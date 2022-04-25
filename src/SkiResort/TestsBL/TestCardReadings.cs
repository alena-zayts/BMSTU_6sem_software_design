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
            added_lift2 = await facade.AdminAddAutoIncrementLiftAsync(TestUsersCreator.adminID, added_lift2);


            // не тот подъемник
            Turnstile added_turnstile1 = new Turnstile(1, added_lift1.LiftID, true);
            await facade.AdminAddTurnstileAsync(TestUsersCreator.adminID, added_turnstile1);

            // тот подъеммник
            Turnstile added_turnstile2 = new Turnstile(2, added_lift2.LiftID, false);
            added_turnstile2 = await facade.AdminAddAutoIncrementTurnstileAsync(TestUsersCreator.adminID, added_turnstile2);
            Turnstile added_turnstile3 = new Turnstile(3, added_lift2.LiftID, false);
            added_turnstile3 = await facade.AdminAddAutoIncrementTurnstileAsync(TestUsersCreator.adminID, added_turnstile3);

            uint exact_time = 10;

            // не тот подъемник
            CardReading added_card_reading1 = new CardReading(1, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            await facade.AdminAddCardReadingAsync(TestUsersCreator.adminID, added_card_reading1);
            CardReading added_card_reading2 = new CardReading(2, added_turnstile1.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            added_card_reading2 = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading2);


            // тот подъемник но не то время
            CardReading added_card_reading3 = new CardReading(3, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time - 1));
            added_card_reading3 = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading3);


            // подходят
            CardReading added_card_reading4 = new CardReading(4, added_turnstile2.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time + 1));
            added_card_reading4 = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading4);

            CardReading added_card_reading5 = new CardReading(5, added_turnstile3.TurnstileID, 9, DateTimeOffset.FromUnixTimeSeconds(exact_time));
            added_card_reading5 = await facade.AdminAddAutoIncrementCardReadingAsync(TestUsersCreator.adminID, added_card_reading5);


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