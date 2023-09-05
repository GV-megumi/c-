using System;
using static System.Console;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace caidan
{

    class ShiCai
    {
        //食材信息
        string[,] shicai = new string[100, 5];
        //食材数量
        public int shicainum { get; set; }
        //库存信息
        string[,] inventory = new string[100, 6];
        public int inventoryNum { get; set; }

        //进货渠道信息
        string[,] sourceOfGoods = new string[100, 2];

        public int sogNum { get; set; }
        //供应商信息表
        string[,] supplier = new string[100, 5];
        public int supplierNum { get; set; }

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


            i.putTable += putTable;
        }

    //返回子符串中的数字和字母以及标点符号的数量（不包含中文字符）
    int putnumber(string inputString)
    {

         

        // 使用正则表达式匹配包含字母、数字和标点符号的子串，排除中文字符
        string pattern =  @"[a-zA-Z0-9\p{P}]+";
        MatchCollection matches = Regex.Matches(inputString, pattern);

        int letterDigitAndPunctuationCount = 0;

        foreach (Match match in matches)
        {
            letterDigitAndPunctuationCount += match.Value.Length;
        }
        return letterDigitAndPunctuationCount;
    }

        //输出表格
        void putTable(int a, int num)
        {


            for (int ii = 0; ii < num; ii++)
                Write(strlens[a ][ii] + "  ");
            WriteLine();


            string[,] aaa;
            int i = 0, j = 1;
            int spacenum;//空格数
            int number=0; //记录str里的数字/字母数

            if (a == 0)
                aaa = shicai;
            else if (a == 1)
                aaa = inventory;
            else if (a == 2)
                aaa = sourceOfGoods;
            else if(a == 3)
            {
                aaa = supplier;
                
            }
            else
            {
                WriteLine("ERROR");
                return;
            }

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

                number=putnumber(s);
                while(number!=0)
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

            WriteLine();


        }



        public void f()
        {

            // 连接字符串，根据您的数据库配置进行修改
            string connectionString = "Server=localhost;Port=3306;Database=food;Uid=root;Pwd=;";
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


    }
}
