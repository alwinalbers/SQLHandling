using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SQLHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            List<Customer> custumers = new List<Customer>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SandboxDB";

            
            using (ISqlConnectionManager dbManager = new SandBoxDbManager(connectionString))
            {
                dbManager.Connect();
                if (dbManager.isConnected)
                {
                    custumers = dbManager.ReadAllCustomer();
                }
            }

            foreach (var customer in custumers)
            {
                Console.WriteLine(customer.CustomerId + " " +customer.CustomerFirstName + " " + customer.CustomerLastName);
            }
            Console.ReadLine();

            Log.CloseAndFlush();
        }
    }
}
