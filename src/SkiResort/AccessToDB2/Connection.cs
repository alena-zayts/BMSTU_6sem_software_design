using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using Microsoft.Extensions.Configuration.Json;


namespace AccessToDB2
{
    public static class Connection
    {
        public static string GetConnection() //(IConfiguration config)
        {
            var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();
            var config = configurationBuilder.Build();
            string connectionString = config["Connections:ConnectPostgres"];
            return connectionString;
        }
    }
}
