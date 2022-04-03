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
    public class CardsDbTest
    {
        ISchema _schema;
        ContextTarantool _context;
        private readonly ITestOutputHelper output;

        public CardsDbTest(ITestOutputHelper output)
        {
            this.output = output;

            var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

            _schema = box.GetSchema();
            _context = new ContextTarantool(_schema);
        }

        [Fact]
        public async Task Test_Add_GetById_Delete()
        {
            ICardsRepository rep = new TarantoolCardsRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());

            // add correct
            CardBL added_card = new CardBL(1, 1, "child");
            await rep.Add(added_card);
            // add already existing
            await Assert.ThrowsAsync<CardDBException>(() => rep.Add(added_card));

            // get_by_id correct
            CardBL got_card = await rep.GetById(added_card.card_id);
            Assert.Equal(added_card, got_card);

            // delete correct
            await rep.Delete(added_card);

            // get_by_id not existing
            await Assert.ThrowsAsync<CardDBException>(() => rep.GetById(added_card.card_id));

            // delete not existing
            await Assert.ThrowsAsync<CardDBException>(() => rep.Delete(added_card));

            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }


        [Fact]
        public async Task Test_Update_GetList()
        {

            ICardsRepository rep = new TarantoolCardsRepository(_context);


            //start testing 
            Assert.Empty(await rep.GetList());


            CardBL added_card1 = new CardBL(1, 1, "child");
            await rep.Add(added_card1);

            CardBL added_card2 = new CardBL(2, 9, "adult");
            await rep.Add(added_card2);

            added_card2.type = "wow";
            added_card1.activation_time = 99;

            // updates correct
            await rep.Update(added_card1);
            await rep.Update(added_card2);

            var list = await rep.GetList();
            Assert.Equal(2, list.Count);
            Assert.Equal(added_card1, list[0]);
            Assert.Equal(added_card2, list[1]);

            await rep.Delete(added_card1);
            await rep.Delete(added_card2);


            // updates not existing
            await Assert.ThrowsAsync<CardDBException>(() => rep.Update(added_card1));
            await Assert.ThrowsAsync<CardDBException>(() => rep.Update(added_card2));


            // end tests - empty getlist
            Assert.Empty(await rep.GetList());
        }
    }
}
