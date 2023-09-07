using System;
using static System.Console;
using MySql.Data.MySqlClient;
using System.Text;


namespace caidan
{
    class MyFoodSql
    {

        string connectionString;
        string[][] sqlHand = new string[5][];

        public MyFoodSql(ShiCai shiCai)
        {
            connectionString = "Server=localhost;Port=3306;Database=food;Uid=root;Pwd=;";

            sqlHand[0] = new string[] { "name", "P_C", "I_N_D", "S_C", "NUTR" };
            sqlHand[1] = new string[] { "NAME", "date", "nc", "sl", "number", "M_F" };
            sqlHand[2] = new string[] { "NAME", "M_F", };
            sqlHand[3] = new string[] { "M_F", "address", "poo", "FPLN", "PSN" };
            sqlHand[4] = new string[] { "name", "M_I", "S_S", "R_S", "R_C", "M_V" };



            shiCai.SqlDelete += Delete;
            shiCai.SqlInsert += Insert;
            shiCai.SqlUpdate += Update;
            shiCai.SqlGetShicaiTable += SqlGetShicai;
            shiCai.SqlGetCaipuTable += SqlGetCaipu;


        }

        //检查是否有单引号
        string CheckData(string data, bool isint)
        {

            //对int的特殊照顾
            if (isint)
            {
                return data;
            }


            StringBuilder result = new StringBuilder();

            foreach (char c in data)
            {
                // 如果当前字符是单引号，将其后面再添加一个单引号
                if (c == '\'')
                {
                    result.Append("''");
                }
                else
                {
                    result.Append(c);
                }
            }

            return "'" + result.ToString() + "'";
        }






        bool Update(string[] str, int num)
        {
            int column, columnnum; //要修改的列号，总列数
            columnnum = str.Length - 1;
            column = int.Parse(str[columnnum]);

            // string updateQuery = "UPDATE yourTableName SET columnName = newValue WHERE yourCondition"
            // 要执行的更新操作 SQL 语句
            string updateQuery = "UPDATE ";
            string data;

            //判断sql属性类型是否为int
            data = CheckData(str[column], num == 1 && column == 4);

            // 要执行的删除操作 SQL 语句
            switch (num)
            {
                case 0:
                    //食材
                    updateQuery += " food_i SET ";

                    break;
                case 1:
                    //库存
                    updateQuery += " inv SET ";
                    break;
                case 2:
                    //进货渠道
                    //sourceOfGoods[0, 0] = "品名";
                    //sourceOfGoods[0, 1] = "制造商";
                    updateQuery += " sog SET ";
                    break;
                case 3:
                    //商
                    updateQuery += " sup SET ";
                    break;
                case 4:
                    updateQuery += " rmm SET ";
                    break;

            }




            updateQuery += sqlHand[num][column] + " = " + data + " where ";

            for (int i = 0; i < columnnum; i++)
            {
                if ((i == column)//更新行
                || (num == 1 && (i == 0 || i == 5))//对库存的特殊照顾
                )
                    continue;

                //  后一项为数据是int的条件
                data = CheckData(str[i], num == 1 && i == 4);



                updateQuery += " " + sqlHand[num][i] + " = " + data + " and";


            }

            updateQuery = updateQuery.Substring(0, updateQuery.Length - 3);

            //对库存的特殊照顾
            if (num == 1)
            {
                updateQuery += " and sog_id in ( SELECT DISTINCT subquery.sog_id FROM ( SELECT inv.sog_id FROM inv, sog WHERE inv.sog_id = sog.SOG_ID AND sog.NAME = '" + str[0] + "' AND sog.M_F = '" + str[5] + "' ) AS subquery ) ;";
            }

            WriteLine(updateQuery);
            //ReadKey();














            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"更新了 {rowsAffected} 行数据.");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLine("错误，请检查更新后是否有重复行/食材是否存在/供应商是否存在/其他错误");
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }







            return true;

        }



        bool Delete(string[] s, int num)
        {
            //deleteQuery = "DELETE FROM yourTableName WHERE yourCondition";
            string deleteQuery = "DELETE FROM ";

            // 要执行的删除操作 SQL 语句
            switch (num)
            {
                case 0:
                    //食材
                    deleteQuery += " food_i WHERE `name`= " + CheckData(s[0], false) + ";";

                    break;
                case 1:
                    //库存
                    deleteQuery += " inv WHERE `date` = "
                    + CheckData(s[1], false) + " AND `sog_id` IN ("
                    + "SELECT * FROM (SELECT inv.sog_id FROM inv, sog WHERE inv.sog_id = sog.SOG_ID AND "
                    + "sog.NAME = " + CheckData(s[0], false) + " AND sog.M_F = "
                    + CheckData(s[5], false) + ") AS subquery);";
                    break;
                case 2:
                    //进货渠道
                    //sourceOfGoods[0, 0] = "品名";
                    //sourceOfGoods[0, 1] = "制造商";
                    deleteQuery += " sog WHERE `NAME`= " + CheckData(s[0], false) + " AND `M_F` = "
                     + CheckData(s[1], false) + ";";
                    break;
                case 3:
                    //商
                    deleteQuery += " sup WHERE `M_F`= " + CheckData(s[0], false) + ";";
                    break;
                case 4:
                    //商
                    deleteQuery += " rmm WHERE `name`= " + CheckData(s[0], false) + ";";
                    break;
            }


            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"删除了 {rowsAffected} 行数据.");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLine("失败：请先检查库存或进货渠道是否还有该食材/进货商");
                WriteLine("Error: " + ex.Message);

                return false;
            }




            //ReadKey();








            return true;
        }



        bool Insert(string[] str, int num)
        {


            string insertQuery = "INSERT INTO "; ;




            // 要执行操作 SQL 语句
            switch (num)
            {
                case 0:
                    insertQuery += "`food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) VALUES (";
                    //食材
                    break;

                case 1:
                    //库存
                    /*
                    inventory[0, 0] = "品名";
                    inventory[0, 1] = "生产日期";
                    inventory[0, 2] = "净含量";
                    inventory[0, 3] = "保质期";
                    inventory[0, 4] = "数量";
                    inventory[0, 5] = "供应商";


                    INSERT INTO 
                    `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) VALUES (
                    (SELECT SOG_ID FROM sog WHERE 
                    sog.NAME = 'zx' AND sog.M_F = 'zx'),'2023.01','12','12',12);
                    */


                    insertQuery += "`inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) VALUES ("
                    + "(SELECT SOG_ID FROM sog WHERE sog.NAME = "
                    + CheckData(str[0], false)
                    + " AND sog.M_F = "
                    + CheckData(str[5], false) + "),"
                    + CheckData(str[1], false) + ","
                    + CheckData(str[2], false) + ","
                    + CheckData(str[3], false) + ","
                    + CheckData(str[4], true) + ")";
                    goto link;

                case 2:
                    //进货渠道

                    insertQuery += "`SOG` (`NAME`,`M_F`)  VALUES (";
                    break;
                case 3:
                    //商
                    insertQuery += "`SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`)  VALUES (";
                    break;

                case 4:
                    //菜谱
                    insertQuery += "`rmm` (  `name` ,`M_I`,`S_S` ,`R_S` ,`R_C`,`M_V`)  VALUES (";
                    break;
            }


            //添加值
            foreach (string s in str)
            {
                insertQuery += "" + CheckData(s, false) + ",";
            }
            insertQuery = insertQuery.Substring(0, insertQuery.Length - 1);
            insertQuery += ")";







        link:

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"添加了了 {rowsAffected} 行数据.");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLine("错误：请检查是否有该食材或供应商/数量请输入数字");
                WriteLine("Error: " + ex.Message);

                return false;
            }



            return true;
        }






        void SqlGetShicai(
            string[,] shicai, ref int shicainum,
            string[,] supplier, ref int supplierNum,
            string[,] sourceOfGoods, ref int sogNum,
            string[,] inventory, ref int inventoryNum,
            int[][] strlens)
        {

            // 连接字符串，根据您的数据库配置进行修改

            string query;


            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // 执行查询
                    WriteLine("正在加载食材相关数据：");
                    query = "SELECT * FROM food_i";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shicainum++;
                                // 从结果集中获取数据并处理


                                shicai[shicainum, 0] = reader.GetString("name");
                                shicai[shicainum, 1] = reader.GetString("P_C");
                                shicai[shicainum, 2] = reader.GetString("I_N_D");
                                shicai[shicainum, 3] = reader.GetString("S_C");
                                shicai[shicainum, 4] = reader.GetString("NUTR");

                                for (int i = 0; i < 5; i++)
                                {
                                    if (shicai[shicainum, i].Length > strlens[0][i])
                                    {
                                        strlens[0][i] = shicai[shicainum, i].Length;
                                    }
                                }

                            }
                        }
                    }
                    WriteLine("食材数据加载完成");





                    WriteLine("正在加载供应商相关数据：");
                    query = "SELECT * FROM SUP";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                supplierNum++;
                                // 从结果集中获取数据并处理
                                supplier[supplierNum, 0] = reader.GetString("M_F");
                                supplier[supplierNum, 1] = reader.GetString("address");
                                supplier[supplierNum, 2] = reader.GetString("poo");
                                supplier[supplierNum, 3] = reader.GetString("FPLN");
                                supplier[supplierNum, 4] = reader.GetString("PSN");

                                for (int i = 0; i < 5; i++)
                                {
                                    if (supplier[supplierNum, i].Length > strlens[3][i])
                                    {
                                        strlens[3][i] = supplier[supplierNum, i].Length;
                                    }
                                }

                            }
                        }
                    }
                    WriteLine("供应商数据加载完成");




                    //sourceOfGoods  sogNum



                    WriteLine("正在加载进货渠道相关数据：");
                    query = "SELECT * FROM SOG";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sogNum++;
                                // 从结果集中获取数据并处理
                                sourceOfGoods[sogNum, 0] = reader.GetString("NAME");
                                sourceOfGoods[sogNum, 1] = reader.GetString("M_F");

                                for (int i = 0; i < 2; i++)
                                {
                                    if (sourceOfGoods[sogNum, i].Length > strlens[2][i])
                                    {
                                        strlens[2][i] = sourceOfGoods[sogNum, i].Length;
                                    }
                                }

                            }
                        }
                    }
                    WriteLine("进货渠道数据加载完成");






                    //inventory    inventoryNum   1



                    WriteLine("正在加载库存相关数据：");
                    query = "SELECT * FROM SOG,inv where SOG.SOG_ID=inv.sog_id";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                inventoryNum++;
                                // 从结果集中获取数据并处理

                                /*
                                            inventory[0, 0] = "品名";
            inventory[0, 1] = "生产日期";
            inventory[0, 2] = "净含量";
            inventory[0, 3] = "保质期";

            inventory[0, 4] = "数量";
            inventory[0, 5] = "供应商";
            */
                                inventory[inventoryNum, 0] = reader.GetString("NAME");
                                inventory[inventoryNum, 1] = reader.GetString("date");
                                inventory[inventoryNum, 2] = reader.GetString("nc");
                                inventory[inventoryNum, 3] = reader.GetString("sl");
                                inventory[inventoryNum, 4] = reader.GetString("number");
                                inventory[inventoryNum, 5] = reader.GetString("M_F");


                                for (int i = 0; i < 6; i++)
                                {
                                    if (inventory[inventoryNum, i].Length > strlens[1][i])
                                    {
                                        strlens[1][i] = inventory[inventoryNum, i].Length;
                                    }
                                }

                            }
                        }
                    }
                    WriteLine("库存数据加载完成");












                    connection.Close();
                    //putfood_i();

                    Clear();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }




        void SqlGetCaipu(string[,] caiPU, ref int Caipunum, int[] strlens)
        {





            string query;


            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // 执行查询
                    WriteLine("正在加载食材相关数据：");
                    query = "SELECT * FROM rmm";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Caipunum++;

                                for (int i = 0; i < 6; i++)
                                {
                                    // 从结果集中获取数据并处理
                                    caiPU[Caipunum, i] = reader.GetString(sqlHand[4][i]);
                                    if (caiPU[Caipunum, i].Length > strlens[i])
                                    {
                                        strlens[i] = caiPU[Caipunum, i].Length;
                                    }
                                }

                            }
                        }
                    }
                    WriteLine("食材数据加载完成");







                    connection.Close();
                    //putfood_i();

                    Clear();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


        }

    }


}