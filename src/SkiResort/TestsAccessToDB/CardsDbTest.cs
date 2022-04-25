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
    public class CardsDbTest
    {
        TarantoolContext _context;
        private readonly ITestOutputHelper output;

        public CardsDbTest(ITestOutputHelper output)
        {
            this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ICardsRepository rep = new TarantoolCardsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetCardsAsync());

            // add correct
            Card added_card = new Card(1, DateTimeOffset.FromUnixTimeSeconds(1), "child");
            await rep.AddCardAsync(added_card);
            // add already existing
            await Assert.ThrowsAsync<CardException>(() => rep.AddCardAsync(added_card));

            // get_by_id correct
            Card got_card = await rep.GetCardByIdAsync(added_card.CardID);
            Assert.Equal(added_card, got_card);

            // delete correct
            await rep.DeleteCardAsync(added_card);

            // get_by_id not existing
            await Assert.ThrowsAsync<CardException>(() => rep.GetCardByIdAsync(added_card.CardID));

            // delete not existing
            await Assert.ThrowsAsync<CardException>(() => rep.DeleteCardAsync(added_card));

            // end tests - empty getlist
            Assert.Empty(await rep.GetCardsAsync());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ICardsRepository rep = new TarantoolCardsRepository(_context);


            //start testing 
            Assert.Empty(await rep.GetCardsAsync());


            Card added_card1 = new Card(1, DateTimeOffset.FromUnixTimeSeconds(1), "child");
            await rep.AddCardAsync(added_card1);

            Card added_card2 = new Card(2, DateTimeOffset.FromUnixTimeSeconds(9), "adult");
            await rep.AddCardAsync(added_card2);

            added_card2 = new Card(added_card2.CardID, added_card2.ActivationTime, "wow");
            added_card1 = new Card(added_card1.CardID, DateTimeOffset.FromUnixTimeSeconds(99), added_card1.Type);

            // updates correct
            await rep.UpdateCardAsync(added_card1);
            await rep.UpdateCardAsync(added_card2);

            var list = await rep.GetCardsAsync();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_card1, list[0]);
            Assert.Equal(added_card2, list[1]);

            await rep.DeleteCardAsync(added_card1);
            await rep.DeleteCardAsync(added_card2);


            // updates not existing
            await Assert.ThrowsAsync<CardException>(() => rep.UpdateCardAsync(added_card1));
            await Assert.ThrowsAsync<CardException>(() => rep.UpdateCardAsync(added_card2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetCardsAsync());


            Card tmp2 = await rep.AddCardAutoIncrementAsync(added_card1);
            Assert.True(1 == tmp2.CardID);
            Card tmp3 = await rep.AddCardAutoIncrementAsync(added_card1);
            Assert.True(2 == tmp3.CardID);
            await rep.DeleteCardAsync(tmp2);
            await rep.DeleteCardAsync(tmp3);
            Assert.Empty(await rep.GetCardsAsync());
        }
    }
}
