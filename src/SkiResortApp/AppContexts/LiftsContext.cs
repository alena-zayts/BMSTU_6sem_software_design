using System;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using System.IO;
/*
namespace SkiResortApp.AppContexts
{
    public class LiftsContext
    {
        public Box box;

        public LiftsContext()
        {
            Connect().Wait();

        }
        static async Task GetBox()
        {
            var box = await Box.Connect("ski_admin:Tty454r293300@localhost:3301");
            this.box = box;

        }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
*/