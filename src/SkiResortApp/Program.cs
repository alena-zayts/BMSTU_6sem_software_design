/*using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/

using System;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using System.IO;

namespace SkiResortApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DoWork().Wait();
        }

        static async Task DoWork()
        {

            using (var box = await Box.Connect(
                "ski_admin:Tty454r293300@localhost:3301"))
            {
                Console.WriteLine("1\n");
                var schema = box.GetSchema();

                var slope_space = await schema.GetSpace("slopes");

                Console.WriteLine("1\n");
                var slope_primary_index = await slope_space.GetIndex("primary");
                Console.WriteLine("1\n");


                try
                {
                    var data = await slope_primary_index.Select<ValueTuple<uint>, ValueTuple<uint, string, uint, bool>>(ValueTuple.Create(1u));
                    /*
                    var data = await slope_primary_index.Select<TarantoolTuple<int>,
                        TarantoolTuple<int, string, int, bool>>(
                        TarantoolTuple.Create(1), new SelectOptions
                        {
                            Iterator = Iterator.All
                        });
                    */
                    Console.WriteLine("1\n");
                    foreach (var item in data.Data)
                    {
                        //Models.Slope slope = new Models.Slope(item.Item1, item.Item2, item.Item3, item.Item4);
                        Console.WriteLine(item);
                    }
                }
                catch (System.Exception e)
                {
                    TextWriter errorWriter = Console.Error;
                    errorWriter.WriteLine(e.Message);
                    return;
                }

            }
        }
    }

    /*
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    */
}
