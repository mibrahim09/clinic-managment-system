using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalManagmentSystem.Controller.MySQL
{
    using MYSQLCOMMAND = MySql.Data.MySqlClient.MySqlCommand;
    using MYSQLREADER = MySql.Data.MySqlClient.MySqlDataReader;
    using MYSQLCONNECTION = MySql.Data.MySqlClient.MySqlConnection;
    public static class DataHolder
    {
        public static MYSQLCONNECTION MySqlConnection
        {
            get
            {
                MYSQLCONNECTION conn = new MYSQLCONNECTION();
                conn.ConnectionString = Config.ConnectionString;
                return conn;
            }
        }

    }
}
