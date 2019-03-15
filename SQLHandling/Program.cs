using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Data;

namespace SQLHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SandboxDB";

            using (ISqlConnectionManager dbManager = new SandBoxDbManager(connectionString))
            {
                dbManager.Logik();
            }

            Console.ReadLine();
            Log.CloseAndFlush();
        }
    }
}
