using System;
using System.Data;
using System.Data.SqlClient;
using DelatorreStore.Shared;

namespace DelatorreStore.Infra.StoreContext.DataContexts
{
    public class DelatorreDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public DelatorreDataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }


        public void Dispose()
        {
            if(Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}