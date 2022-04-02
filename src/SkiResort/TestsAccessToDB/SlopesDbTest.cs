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
	public class SlopesDbTest
	{
		ISchema _schema;
		private readonly ITestOutputHelper output;
		public SlopesDbTest(ITestOutputHelper output)
		{
			this.output = output;

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_schema = box.GetSchema();
		}
		[Fact]
		public void Test_Add_GetById_Delete()
		{
			ISlopesRepository rep = new TarantoolSlopesRepository(_schema);

			SlopeBL added_slope = new SlopeBL(100000, "A1", true, 1);
			rep.Add(added_slope);


			SlopeBL got_slope = rep.GetById(added_slope.slope_id);
			

			Assert.Equal(added_slope, got_slope);


			rep.Delete(added_slope);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_slope.slope_id));
		}

		[Fact]
		public void Test_Add_GetByName_Delete()
		{

			ISlopesRepository rep = new TarantoolSlopesRepository(_schema);

			SlopeBL added_slope = new SlopeBL(200000, "A2", false, 20);
			rep.Add(added_slope);


			SlopeBL got_slope = rep.GetByName(added_slope.slope_name);


			Assert.Equal(added_slope, got_slope);


			rep.Delete(added_slope);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_slope.slope_id));
		}


		[Fact]
		public void Test_Update_GetList()
		{

			ISlopesRepository rep = new TarantoolSlopesRepository(_schema);

			SlopeBL added_slope1 = new SlopeBL(100000, "A1", true, 1);
			rep.Add(added_slope1);

			SlopeBL added_slope2 = new SlopeBL(200000, "A2", false, 20);
			rep.Add(added_slope2);

			added_slope2.is_open = true;
			added_slope2.difficulty_level = 50;
			rep.Update(added_slope2);


			Assert.Equal(2, rep.GetList().Count());

			SlopeBL got_slope1 = rep.GetList()[0];
			SlopeBL got_slope2 = rep.GetList()[1];


			Assert.Equal(added_slope2, got_slope2);


			rep.Delete(added_slope1);
			rep.Delete(added_slope2);
			Assert.Empty(rep.GetList());
		}
	}
}
