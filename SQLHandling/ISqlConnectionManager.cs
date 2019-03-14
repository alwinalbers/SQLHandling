using System.Collections.Generic;

namespace SQLHandling
{
    internal interface ISqlConnectionManager: System.IDisposable
    {
        bool isConnected { get; }
        void Connect();

        List<Customer> ReadAllCustomer();
    }
}