using System;
using static System.Console;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Math.EC.Multiplier;


namespace caidan
{

    class ShiCai:TableType
    {
        //sql连接
        string connectionString = "Server=localhost;Port=3306;Database=food;Uid=root;Pwd=;";


        //食材信息
        string[,] shicai = new string[100, 5];
        //食材数量
        public int shicainum;


        //库存信息
        string[,] inventory = new string[100, 6];
        public int inventoryNum;


        //进货渠道信息
        string[,] sourceOfGoods = new string[100, 2];

        public int sogNum;

        //供应商信息表
        string[,] supplier = new string[100, 5];
        public int supplierNum;




        //用于表格对齐
        int[][] strlens = new int[4][];




        public ShiCai(UI i)
        {

            //食材
            shicai[0, 0] = "品名";
            shicai[0, 1] = "产品类别";
            shicai[0, 2] = "配料表";
            shicai[0, 3] = "贮存条件";
            shicai[0, 4] = "营养成分";
            strlens[0] = new int[] { 2, 4, 3, 4, 4 };

            //库存
            inventory[0, 0] = "品名";
            inventory[0, 1] = "生产日期";
            inventory[0, 2] = "净含量";
            inventory[0, 3] = "保质期";

            inventory[0, 4] = "数量";
            inventory[0, 5] = "供应商";
            strlens[1] = new int[] { 2, 4, 3, 3, 2, 3 };


            //进货渠道
            sourceOfGoods[0, 0] = "品名";
            sourceOfGoods[0, 1] = "制造商";
            strlens[2] = new int[] { 2, 3 };

            //供应商
            supplier[0, 0] = "制造商";
            supplier[0, 1] = "地址";
            supplier[0, 2] = "产地";
            supplier[0, 3] = "食品生产许可证号";
            supplier[0, 4] = "产品标准号";
            strlens[3] = new int[] { 3, 2, 2, 8, 5 };





            f();

            //订阅事件
            i.putTable += putTable;
            i.CallDelete += DoDelete;
            i.GetTableHand += SendTableHand;
            i.CallInsert += DoInsert;
            i.CallUpdate += DoUpdate;
        }


        //输出表格
        void putTable(ref int a)
        {





            string[,] aaa;
            int i = 0, j = 1;
            int spacenum;//空格数
            int number = 0; //记录str里的数字/字母数


            if ((aaa = ChooseTable(a)) == null)
                return;


            int num = aaa.GetLength(1);

            Write("行号    ");
            foreach (string s in aaa)
            {
                if (s == null)
                    break;


                if (i == num)
                {
                    i = 0;
                    WriteLine();
                    Write($"{j:D3}     ");
                    j++;
                }
                Write(s);

                number = putspace(s);
                while (number != 0)
                {
                    Write(" ");
                    number--;
                }





                spacenum = strlens[a][i] - s.Length + 4;
                //Write(spacenum);
                while (spacenum != 0)
                {
                    spacenum--;
                    Write("  ");
                }
                i++;


            }

            WriteLine("\n\n");

            a= j - 1;


        }

        public event MySQLDel<bool, string[], int> SqlUpdate;
        bool DoUpdate(string[] updateData, int num)
        {


            string[,] table;
            int row, column, i, j;
            string[] str;


            if ((table = ChooseTable(num)) == null)
                return false;

            ref int tablenum = ref ChooseTableCount(num);

            row = int.Parse(updateData[0]);
            column = int.Parse(updateData[1]);

            if (row > tablenum || row <= 0)
            {
                WriteLine("没有该行");
                return false;
            }
            if (column > table.GetLength(1) || row <= 0)
            {
                WriteLine("没有该列");
                return false;
            }

            if (num == 1 && (column == 0 || column == 5))
            {
                WriteLine("此项不可更改");
                return false;
            }

            //WriteLine(table.GetLength(1));




            //ReadKey();

            //str 储存了修改后的一行数据+上一个 被修改的数据的位置
            str = new string[table.GetLength(1) + 1];
            //传数据
            for (i = 0; i < table.GetLength(1); i++)
            {
                str[i] = table[row, i];
            }

            //更新数据
            str[column] = updateData[2];
            //记录被更新的数据的位置 （列号）
            str[table.GetLength(1)] = updateData[1];





            //更新数据库
            if (!SqlUpdate(str, num))
            {
                pause();
                return false;

            }

            //更新数组
            //pause();

    table[row,column]=updateData[2];







            return true;
        }


        public delegate T MySQLDel<T, R, S>(R a, S b);
        public event MySQLDel<bool, string[], int> SqlDelete;
        bool DoDelete(int row, int num)
        {

            string[,] table;
            int i, j;
            string[] str;


            if ((table = ChooseTable(num)) == null)
                return false;

            ref int tablenum = ref ChooseTableCount(num);

            if (row > tablenum || row <= 0)
            {
                WriteLine("没有该行");
                return false;
            }

            WriteLine(table.GetLength(1));
            //ReadKey();

            str = new string[table.GetLength(1)];
            for (i = 0; i < table.GetLength(1); i++)
            {
                str[i] = table[row, i];
            }


            //删数据库
            if (!SqlDelete(str, num))
            {
                pause();
                return false;

            }

            //删数组

            for (i = row; i <= tablenum; i++)
            {
                for (j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = table[i + 1, j];
                }
            }
            tablenum--;

            return true;
        }





        public event MySQLDel<bool, string[], int> SqlInsert;

        bool DoInsert(string[] str, int num)
        {



            string[,] table;
            int i;

            if ((table = ChooseTable(num)) == null)
                return false;
            ref int tablenum = ref ChooseTableCount(num);





            //加据库
            if (!SqlInsert(str, num))
            {
                pause();
                return false;

            }

            //加数组

            tablenum++;
            for (i = 0; i < str.Length; i++)
            {
                table[tablenum, i] = str[i];
            }


            return true;
        }




























        void f()
        {

            // 连接字符串，根据您的数据库配置进行修改

            string query;
            shicainum = 0;
            inventoryNum = 0;
            sogNum = 0;
            supplierNum = 0;

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


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }







        string[,] ChooseTable(int a)
        {

            if (a == 0)
                return shicai;
            else if (a == 1)
                return inventory;
            else if (a == 2)
                return sourceOfGoods;
            else if (a == 3)
            {
                return supplier;
            }
            else
            {
                // WriteLine("ERROR");
                return null;
            }

        }


        ref int ChooseTableCount(int num)
        {
            switch (num)
            {
                case 0:
                    //食材
                    return ref shicainum;

                case 1:
                    return ref inventoryNum;

                case 2:
                    return ref sogNum;




            }
            return ref supplierNum;
        }
        //返回子符串中的数字和字母以及标点符号的数量（不包含中文字符）
  

        //传递表头
        string[] SendTableHand(int a, int tablenum)
        {
            string[,] table;
            if ((table = ChooseTable(tablenum)) == null)
                return null;
            string[] hand = new string[table.GetLength(1)];
            for (int i = 0; i < table.GetLength(1); i++)
                hand[i] = table[0, i];
            return hand;
        }



    }
}
