using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CodeWorksWarehouse.Data
{
    public static class DbConnectionOptions
    {
        public static IDbConnection ConnectToMySQL(string connectionstring)
        {
            var connection = new MySqlConnection(connectionstring);
            connection.Open();
            return connection;
        }
    }
}
