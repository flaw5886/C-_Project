﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosk.remote;
using Kiosk.util;
using MySql.Data.MySqlClient;

namespace Kiosk.database
{
    class FoodRemote
    {
        private readonly RemoteConnection connection;

        public FoodRemote()
        {
            connection = new RemoteConnection();
        }
        public void SetFoodSale(Food food, int sale)
        {
            connection.SetData("update menu set sale = " + sale + " where idxMenu = " + food.idx + ";");
        }
        public List<Food> GetAllFood()
        {
            List<Food> foodList = new List<Food>();
            MySqlDataReader reader = connection.GetData("Select * from menu");

            while (reader.Read())
            {
                Food food = new Food();
                food.idx = int.Parse(reader["idxMenu"].ToString());
                food.name = reader["MenuName"].ToString();
                food.category = (Category)int.Parse(reader["category"].ToString());
                food.imagePath = reader["img"].ToString();
                food.page = int.Parse(reader["page"].ToString());
                food.sale = int.Parse(reader["sale"].ToString());
                food.originalPrice = int.Parse(reader["price"].ToString());
                food.price = food.originalPrice - food.sale;

                foodList.Add(food);
            }

            connection.con.Close();
            return foodList;
        }
    }
}
