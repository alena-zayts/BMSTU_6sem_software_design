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
using System.Collections.Generic;


namespace Tests
{
	public class CardReadingsDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public CardReadingsDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_schema);

			CardReadingDB added_card_reading = new CardReadingDB(1, 1, 1, 100);
			rep.Add(added_card_reading);


			CardReadingDB got_card_reading = rep.GetById(added_card_reading.record_id);
			
			Assert.Equal(added_card_reading, got_card_reading);

			rep.Delete(added_card_reading);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_card_reading.record_id));
		}

		[Fact]
		public void Test_Add_GetByLLiftIdFromDate_Delete()
		{
			ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_schema);
			

			ILiftsRepository lifts_rep = new TarantoolLiftsRepository(_schema);
			LiftDB added_lift1 = new LiftDB(1, "A1", true, 100, 60, 360);
			lifts_rep.Add(added_lift1);
			LiftDB added_lift2 = new LiftDB(2, "A2", false, 20, 10, 30);
			lifts_rep.Add(added_lift2);

			ITurnstilesRepository turnstiles_rep = new TarantoolTurnstilesRepository(_schema);
			// не тот подъемник
			TurnstileDB added_turnstile1 = new TurnstileDB(1, added_lift1.lift_id, true);
			turnstiles_rep.Add(added_turnstile1);

			// тот подъеммник
			TurnstileDB added_turnstile2 = new TurnstileDB(2, added_lift2.lift_id, false);
			turnstiles_rep.Add(added_turnstile2);
			TurnstileDB added_turnstile3 = new TurnstileDB(3, added_lift2.lift_id, false);
			turnstiles_rep.Add(added_turnstile3);

			uint exact_time = 10;

			// не тот подъемник
			CardReadingDB added_card_reading1 = new CardReadingDB(1, added_turnstile1.turnstile_id, 9, exact_time - 1);
			rep.Add(added_card_reading1);
			CardReadingDB added_card_reading2 = new CardReadingDB(2, added_turnstile1.turnstile_id, 9, exact_time + 1);
			rep.Add(added_card_reading2);

			// тот подъемник но не то время
			CardReadingDB added_card_reading3 = new CardReadingDB(3, added_turnstile2.turnstile_id, 9, exact_time - 1);
			rep.Add(added_card_reading3);

			// подходят
			CardReadingDB added_card_reading4 = new CardReadingDB(4, added_turnstile2.turnstile_id, 9, exact_time + 1);
			rep.Add(added_card_reading4);
			CardReadingDB added_card_reading5 = new CardReadingDB(5, added_turnstile3.turnstile_id, 9, exact_time);
			rep.Add(added_card_reading5);


			List<CardReadingDB> got_card_reading = rep.GetByLiftIdFromDate(added_lift2.lift_id, exact_time);


			Assert.Equal(2, got_card_reading.Count());
			Assert.Equal(added_card_reading4, got_card_reading[0]);
			Assert.Equal(added_card_reading5, got_card_reading[1]);

			rep.Delete(added_card_reading1);
			rep.Delete(added_card_reading2);
			rep.Delete(added_card_reading3);
			rep.Delete(added_card_reading4);
			rep.Delete(added_card_reading5);
			Assert.Equal(0, rep.GetList().Count());

			lifts_rep.Delete(added_lift1);
			lifts_rep.Delete(added_lift2);
			Assert.Equal(0, lifts_rep.GetList().Count());

			turnstiles_rep.Delete(added_turnstile1);
			turnstiles_rep.Delete(added_turnstile2);
			turnstiles_rep.Delete(added_turnstile3);
			Assert.Equal(0, turnstiles_rep.GetList().Count());

		}


		[Fact]
		public void Test_Update_GetList()
		{

			ICardReadingsRepository rep = new TarantoolCardReadingsRepository(_schema);

			CardReadingDB added_card_reading1 = new CardReadingDB(1, 1, 1, 100);
			rep.Add(added_card_reading1);

			CardReadingDB added_card_reading2 = new CardReadingDB(2, 2, 1, 100);
			rep.Add(added_card_reading2);

			added_card_reading1.reading_time = 9;
			added_card_reading2.turnstile_id = 50;
			rep.Update(added_card_reading2);


			Assert.Equal(2, rep.GetList().Count());

			CardReadingDB got_card_reading1 = rep.GetList()[0];
			CardReadingDB got_card_reading2 = rep.GetList()[1];


			Assert.Equal(added_card_reading2, got_card_reading2);

			rep.Delete(added_card_reading1);
			rep.Delete(added_card_reading2);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_card_reading1.record_id));
			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_card_reading2.record_id));
			Assert.Empty(rep.GetList());
		}
	}
}
