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
            Console.WriteLine("yy");
            using (var box = await Box.Connect(
                "ski_admin:Tty454r293300@localhost:3301"))
            {
                Console.WriteLine("pop");
                var schema = box.GetSchema();

                var space = await schema.GetSpace("users");
                var primaryIndex = await space.GetIndex("primary_id");

                var data = await primaryIndex.Select<TarantoolTuple<string>,
                    TarantoolTuple<string, string, string, string, long>>(
                    TarantoolTuple.Create(String.Empty), new SelectOptions
                    {
                        Iterator = Iterator.All
                    });

                foreach (var item in data.Data)
                {
                    Console.WriteLine(item);
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
