using System;
using static System.Console;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace caidan
{
    class MyFoodSql
    {

        string connectionString;
        string[][] sqlHand = new string[5][];

        public MyFoodSql(ShiCai shiCai, CaiPU caiPU)
        {
            connectionString = "Server=localhost;Port=3306;Database=food;Uid=root;Pwd=;";

            sqlHand[0] = new string[] { "name", "P_C", "I_N_D", "S_C", "NUTR" };
            sqlHand[1] = new string[] { "NAME", "date", "nc", "sl", "number", "M_F" };
            sqlHand[2] = new string[] { "NAME", "M_F", };
            sqlHand[3] = new string[] { "M_F", "address", "poo", "FPLN", "PSN" };



            shiCai.SqlDelete += Delete;
            shiCai.SqlInsert += Insert;
            shiCai.SqlUpdate += Update;


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
            if (num == 1 && column == 4)
            {
                data = str[column];
            }
            else
            {
                data = "'" + str[column] + "'";

            }



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
            }




            updateQuery += sqlHand[num][column] + " = " + data + " where ";

            for (int i = 0; i < columnnum; i++)
            {
                if ((i == column)//更新行
                ||(num==1&&(i==0||i==5))//对库存的特殊照顾
                )
                    continue;

                //对int的特殊照顾
                if (num == 1 && i == 4)
                {
                    data = str[i];
                }
                else
                {
                    data = "'" + str[i] + "'";

                }


                updateQuery +=" "+ sqlHand[num][i] + " = " +data +" and";


            }

            updateQuery=updateQuery.Substring(0, updateQuery.Length - 3);

            //对库存的特殊照顾
            if(num==1)
            {
                updateQuery+=" and sog_id in ( SELECT DISTINCT subquery.sog_id FROM ( SELECT inv.sog_id FROM inv, sog WHERE inv.sog_id = sog.SOG_ID AND sog.NAME = '" + str[0] + "' AND sog.M_F = '" + str[5] + "' ) AS subquery ) ;";
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
                    deleteQuery += " food_i WHERE `name`= '" + s[0] + "';";

                    break;
                case 1:
                    //库存
                    deleteQuery += " inv WHERE `date` = '"
                    + s[1] + "' AND `sog_id` IN ("
                    + "SELECT * FROM (SELECT inv.sog_id FROM inv, sog WHERE inv.sog_id = sog.SOG_ID AND "
                    + "sog.NAME = '" + s[0] + "' AND sog.M_F = '" + s[5] + "') AS subquery);";
                    break;
                case 2:
                    //进货渠道
                    //sourceOfGoods[0, 0] = "品名";
                    //sourceOfGoods[0, 1] = "制造商";
                    deleteQuery += " sog WHERE `NAME`= '" + s[0] + "' AND `M_F` = '" + s[1] + "';";
                    break;
                case 3:
                    //商
                    deleteQuery += " sup WHERE `M_F`= '" + s[0] + "';";
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
                    + "(SELECT SOG_ID FROM sog WHERE sog.NAME = '"
                    + str[0]
                    + "' AND sog.M_F = '"
                    + str[5] + "'),'"
                    + str[1] + "','"
                    + str[2] + "','"
                    + str[3] + "',"
                    + str[4] + ")";
                    goto link;

                case 2:
                    //进货渠道

                    insertQuery += "`SOG` (`NAME`,`M_F`)  VALUES (";
                    break;
                case 3:
                    //商
                    insertQuery += "`SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`)  VALUES (";
                    break;
            }


            //添加值
            foreach (string s in str)
            {
                insertQuery += "'" + s + "',";
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
                WriteLine("错误：请检查是否有该食材或供应商");
                WriteLine("Error: " + ex.Message);

                return false;
            }



            return true;
        }


    }


}