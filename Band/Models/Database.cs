using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BandApp;

namespace Band.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
