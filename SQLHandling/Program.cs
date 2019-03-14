using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHandling
{
    class Program
    {
        static void Main(string[] args)
        {
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
                Console.WriteLine(customer.CustomerID + " " +customer.FirstName + " " + customer.LastName);
            }
            Console.ReadLine();
        }
    }
}
