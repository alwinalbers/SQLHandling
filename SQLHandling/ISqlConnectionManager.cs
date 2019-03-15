using System.Collections.Generic;
using System.Data;

namespace SQLHandling
{
    interface ISqlConnectionManager: System.IDisposable
    {
        bool isConnected { get; }
        string workingTable { get; set; }
        void Connect();

        void Logik();

        List<Table> GetTables();
        List<string> GetTableNames();

        Table SelectTable(List<Table> tables);

        void PrintTableNames(List<Table> tables);
        //DataTable SetWorkingTable();
        List<IDatabaseTableEntity> ReadAllEntities();


    }
}