﻿using Kiosk.model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.remote
{
    class UserRemote
    {
        private readonly RemoteConnection connection;

        public UserRemote()
        {
            connection = new RemoteConnection();
        }

        public bool SetLogin()
        {
            JObject json = new JObject();
            json.Add("MSGType", 0);
            json.Add("id", "2210");
            json.Add("Content", "");
            json.Add("ShopName", "");
            json.Add("OrderNumber", "");
            json.Add("Menus", "");

            String data = JsonConvert.SerializeObject(json);
            bool isSuccess = connection.SetServerData(data);

            if (isSuccess == true)
            {
                return true;
            }

            return false;
        }

        public bool IsAutoLogin()
        {
            String id = "2210";
            MySqlDataReader reader = connection.GetDBData("Select * from user where id = '" + id + "';");

            while (reader.Read())
            {
                if (reader["isAuto"].ToString() == "1")
                {
                    return true;
                }
            }

            return false;
        }

        public void SetAutoLogin()
        {
            String id = "2210";
            connection.SetDBData("update user set isAuto = 1 where id = '" + id + "';");
        }

        public List<User> GetAllUser()
        {
            List<User> userList = new List<User>();
            MySqlDataReader reader = connection.GetDBData("Select * from user");

            while (reader.Read())
            {
                User user = new User();

                user.idx = int.Parse(reader["idxUser"].ToString());
                user.name = reader["name"].ToString();
                user.qrCode = reader["QRCode"].ToString();
                user.barCode = reader["BaCode"].ToString();

                Console.WriteLine(user.name);

                userList.Add(user);
            }

            connection.con.Close();
            return userList;
        }
    }
}
