using System;
using System.Linq;
using Xunit;
using SkiResortApp.ComponentAccessToDB.DBModels;
using SkiResortApp.ComponentAccessToDB.RepositoryInterfaces;
using SkiResortApp.ComponentAccessToDB.RepositoriesTarantool;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using System.IO;
using Xunit.Abstractions;


namespace Tests
{
	public class CardsDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public CardsDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ICardsRepository rep = new TarantoolCardsRepository(_schema);

			CardDB added_card = new CardDB(100000, 10, "tet");
			rep.Add(added_card);


			CardDB got_card = rep.GetById(added_card.card_id);
			
			Assert.Equal(added_card, got_card);

			rep.Delete(added_card);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_card.card_id));
		}


		[Fact]
		public void Test_Update_GetList()
		{

			ICardsRepository rep = new TarantoolCardsRepository(_schema);

			CardDB added_card1 = new CardDB(100000, 10, "tet");
			rep.Add(added_card1);

			CardDB added_card2 = new CardDB(200000, 20, "pe");
			rep.Add(added_card2);

			added_card2.type = "dfd";
			added_card1.activation_time = 50;
			rep.Update(added_card1);
			rep.Update(added_card2);


			Assert.Equal(2, rep.GetList().Count());

			CardDB got_card1 = rep.GetList()[0];
			CardDB got_card2 = rep.GetList()[1];


			Assert.Equal(added_card2, got_card2);

			rep.Delete(added_card1);
			rep.Delete(added_card2);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_card1.card_id));
			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_card2.card_id));
			Assert.Empty(rep.GetList());
		}
	}
}
