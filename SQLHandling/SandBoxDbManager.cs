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
    public class SandBoxDbManager : ISqlConnectionManager
    {
        SqlConnection sqlConnection;
        public bool isConnected { get; private set; }
        public string workingTable { get; set; }


        public SandBoxDbManager(string connectionString)
        {
            isConnected = false;
            sqlConnection = new SqlConnection(connectionString);
        }        

        public void Logik()
        {
            
            Connect();
            if (isConnected)
            {
                Log.Information("Tables");

                List<Table> tables = GetTables();
                PrintTableNames(tables);
                var selectTable = new Table();
                    selectTable = SelectTable(tables);

                List<IDatabaseTableEntity> entities = new List<IDatabaseTableEntity>();
                entities = ReadAllEntities();
            }
        }

        public Table SelectTable(List<Table> tables)
        {
            Log.Information("Select TableID:");
            var inputTableId = Convert.ToInt32(Console.ReadLine());

            Table table = new Table();

            while (table.TableId != inputTableId)
            {
                try
                {
                    table = tables.Find(i => i.TableId == inputTableId);
                }
                catch (Exception e)
                {
                    Log.Error($"{e}");
                }

                if (table != null && table.TableId == inputTableId)
                {
                    Log.Information("Tabelle gefunden");
                }
                else
                {
                    Log.Information("Tabelle nicht gefunden");
                }
            }

            return table;
        }

        public List<IDatabaseTableEntity> ReadAllEntities()
        {
            List<IDatabaseTableEntity> EntityList = new List<IDatabaseTableEntity>();
            string sql = "SELECT * FROM Customer";

            using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = Convert.ToInt32(reader["CustomerID"]);
                        customer.CustomerFirstName = reader["CustomerFirstName"].ToString();
                        customer.CustomerLastName = reader["CustomerSurName"].ToString();
                        EntityList.Add(customer);
                    }
                }
            }
            Dispose();

            return EntityList;
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
                Log.Error($"{e}");
                isConnected = false;
            }
        }

        public void Dispose()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection.Dispose();
        }

        public List<Table> GetTables()
        {
            var tablenames = GetTableNames();
            List<Table> tables = new List<Table>();

            int id = 0;
            foreach (string name in tablenames)
            {
                Table table = new Table();
                table.TableId = id;
                id++;
                table.TableName = name;
                tables.Add(table);
            }

            return tables;
        }

        public List<string> GetTableNames()
        {
            var tableNames = new List<string>();
            
            var workingTables = new DataTable();
            workingTables = sqlConnection.GetSchema("Tables");
            
            foreach(DataRow row in workingTables.Rows)
            {
                string tableName = row[2].ToString();
                tableNames.Add(tableName);
            }

            return tableNames;
        }

        public void PrintTableNames(List<Table> tables)
        {            
            foreach (Table table in tables)
            {                               
                Log.Information(table.TableId +"  " +table.TableName);
            }
        }
    }
}
