﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosk.util;
using MySql.Data.MySqlClient;

namespace Kiosk.remote
{
    class RemoteConnection
    {
        public MySqlConnection con;

        public RemoteConnection()
        {
            con = new MySqlConnection(Constants.DEFAULT_HOST);
        }

        public MySqlDataReader GetData(string sql)
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }
    }
}