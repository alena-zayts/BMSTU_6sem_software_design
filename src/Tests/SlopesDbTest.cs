using System;
using System.Linq;
using Xunit;
using SkiResortApp.DbModels;
using SkiResortApp.IRepositories;
using SkiResortApp.TarantoolRepositories;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using System.IO;
using Xunit.Abstractions;


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

			Slope added_slope = new Slope(100000, "A1", true, 1);
			rep.Add(added_slope);


			Slope got_slope = rep.GetById(added_slope.slope_id);
			

			Assert.Equal(added_slope.slope_id, got_slope.slope_id);
			Assert.Equal(added_slope.slope_name, got_slope.slope_name);
			Assert.Equal(added_slope.is_open, got_slope.is_open);
			Assert.Equal(added_slope.difficulty_level, got_slope.difficulty_level);


			rep.Delete(added_slope);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetById(added_slope.slope_id));
		}

		[Fact]
		public void Test_Add_GetByName_Delete()
		{

			ISlopesRepository rep = new TarantoolSlopesRepository(_schema);

			Slope added_slope = new Slope(200000, "A2", false, 20);
			rep.Add(added_slope);


			Slope got_slope = rep.GetByName(added_slope.slope_name);


			Assert.Equal(added_slope.slope_id, got_slope.slope_id);
			Assert.Equal(added_slope.slope_name, got_slope.slope_name);
			Assert.Equal(added_slope.is_open, got_slope.is_open);
			Assert.Equal(added_slope.difficulty_level, got_slope.difficulty_level);


			rep.Delete(added_slope);

			Assert.Throws<IndexOutOfRangeException>(() => rep.GetByName(added_slope.slope_name));
		}


		[Fact]
		public void Test_Update_GetList()
		{

			ISlopesRepository rep = new TarantoolSlopesRepository(_schema);

			Slope added_slope1 = new Slope(100000, "A1", true, 1);
			rep.Add(added_slope1);

			Slope added_slope2 = new Slope(200000, "A2", false, 20);
			rep.Add(added_slope2);

			added_slope2.is_open = true;
			added_slope2.difficulty_level = 50;
			rep.Update(added_slope2);


			Assert.Equal(2, rep.GetList().Count());

			Slope got_slope1 = rep.GetList()[0];
			Slope got_slope2 = rep.GetList()[1];


			Assert.Equal(added_slope2.slope_id, got_slope2.slope_id);
			Assert.Equal(added_slope2.slope_name, got_slope2.slope_name);
			Assert.Equal(added_slope2.is_open, got_slope2.is_open);
			Assert.Equal(added_slope2.difficulty_level, got_slope2.difficulty_level);

			rep.Delete(added_slope1);
			rep.Delete(added_slope2);
		}
	}
}