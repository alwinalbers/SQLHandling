using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHandling
{
    public class SandBoxDbManager : ISqlConnectionManager
    {
        SqlConnection sqlConnection;
        public bool isConnected { get; private set; }


        public SandBoxDbManager(string connectionString)
        {
            isConnected = false;
            sqlConnection = new SqlConnection(connectionString);
        }        

        public List<Customer> ReadAllCustomer()
        {
            List<Customer> CustomerList = new List<Customer>();
            string sql = "SELECT * FROM Customer";

            Connect();
            using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = Convert.ToInt32(reader["CustomerID"]);
                        customer.FirstName = reader["CustomerFirstName"].ToString();
                        customer.LastName = reader["CustomerSurname"].ToString();
                        CustomerList.Add(customer);
                    }
                }
            }
            Dispose();

            return CustomerList;
        }



        public void Connect()
        {
            try
            {
                sqlConnection.Open();
                isConnected = true;
            }
            catch (Exception e)
            {
                isConnected = false;
                //ToDo Logging
            }
        }

        public void Dispose()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection.Dispose();
        }
    }
}
