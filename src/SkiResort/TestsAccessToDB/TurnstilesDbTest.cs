using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using SkiResort.ComponentBL.ModelsBL;
using SkiResort.ComponentAccessToDB.RepositoriesInterfaces;
using SkiResort.ComponentAccessToDB.RepositoriesTarantool;


namespace Tests
{
	public class TurnstilesDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public TurnstilesDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_schema);


			List<TurnstileBL> got_turnstiles = rep.GetList();
			Assert.Empty(got_turnstiles);


			TurnstileBL added_turnstile = new TurnstileBL(100000, 1, true);
			rep.Add(added_turnstile);


			TurnstileBL got_turnstile = rep.GetById(added_turnstile.turnstile_id);
			

			Assert.Equal(added_turnstile, got_turnstile);


			rep.Delete(added_turnstile);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_turnstile.turnstile_id));
		}

        [Fact]
        public void Test_Add_GetByLiftId_Delete()
        {

            ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_schema);

            TurnstileBL added_turnstile1 = new TurnstileBL(1000, 3, true);
            rep.Add(added_turnstile1);
            TurnstileBL added_turnstile2 = new TurnstileBL(2000, 3, false);
            rep.Add(added_turnstile2);


            List<TurnstileBL> got_turnstiles = rep.GetByLiftId(3);
            Assert.Equal(2, got_turnstiles.Count());

            TurnstileBL got_turnstile1 = got_turnstiles[0];
            TurnstileBL got_turnstile2 = got_turnstiles[1];


            Assert.Equal(added_turnstile1, got_turnstile1);
            Assert.Equal(added_turnstile2, got_turnstile2);



            rep.Delete(added_turnstile1);
            rep.Delete(added_turnstile2);

            got_turnstiles = rep.GetByLiftId(1);
            Assert.Empty(got_turnstiles);

        }


        [Fact]
        public void Test_Update_GetList()
        {

            ITurnstilesRepository rep = new TarantoolTurnstilesRepository(_schema);

            TurnstileBL added_turnstile1 = new TurnstileBL(100000, 1, true);
            rep.Add(added_turnstile1);
            TurnstileBL added_turnstile2 = new TurnstileBL(200000, 2, false);
            rep.Add(added_turnstile2);

            added_turnstile2.is_open = true;
            added_turnstile2.lift_id = 1;
            rep.Update(added_turnstile2);


            Assert.Equal(2, rep.GetList().Count());

            TurnstileBL got_turnstile1 = rep.GetList()[0];
            TurnstileBL got_turnstile2 = rep.GetList()[1];


            Assert.Equal(added_turnstile2, got_turnstile2);

            rep.Delete(added_turnstile1);
            rep.Delete(added_turnstile2);
            Assert.Empty(rep.GetList());
        }
    }
}
