
using System;
using static System.Console;
using System.IO;
using chapter;
using MySql.Data.MySqlClient;

namespace chapter
{
    class Sql : Chapter
    {
    
      
        public override void f()
        {

   // 连接字符串，根据您的数据库配置进行修改
        string connectionString = "Server=localhost;Port=3306;Database=mhgl;Uid=root;Pwd=453986;";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // 执行查询
                string query = "SELECT * FROM flight";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 从结果集中获取数据并处理
                            string id = reader.GetString("f_id");
                            string name = reader.GetString("f_src");
                            Console.WriteLine($"ID: {id}, Name: {name}");
                        }
                    }
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }



        }
    }
