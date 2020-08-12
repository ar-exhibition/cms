using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//sql nuget package
using MySql.Data.MySqlClient;

namespace cms.ar.xarchitecture.de
{
    public class MySQLDatabase
    {
        public MySqlConnection Connection;

        public MySQLDatabase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
