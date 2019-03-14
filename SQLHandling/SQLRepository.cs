using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQLHandling
{
    class SQLRepository
    {
        public SqlConnection ConnectToDB()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SandboxDB";
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    Console.WriteLine("Connection Open ! ");

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection !" + ex);
            }

            return sqlConnection;
        }

        //public void ReadData()
        //{
        //    command = new SqlCommand()
        //}
    }
}
