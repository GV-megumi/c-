using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Org.BouncyCastle.Crypto.Fpe;
using static System.Console;

namespace caidan
{

    class UI
    {
        //输入按键
        ConsoleKey key;
        //表格


        int tabnum;

        //自定义委托声明，
        public delegate t MyDel1<t>(int a);

        //非标准事件委托，调用
        public delegate T MyDel2<T, R>(R a, int tablenum);

        //输出表格
        public event MyDel1<int> putTable;







        void mainui()
        {
        backk:
            WriteLine("Q:食材管理");
            WriteLine("W:菜谱管理");
            WriteLine("E:菜单");
            WriteLine("请键入字母选择：");

            key = Console.ReadKey().Key;
            WriteLine(key);
            if (key == ConsoleKey.Q)
            {
                Clear();
                shicaiui();
            }
            else if (key == ConsoleKey.W)
                ;
            else if (key == ConsoleKey.E)
                ;
            else
            {
                Clear();
                goto backk;
            }

        }

        void shicaiui()
        {
        backk:
            WriteLine("Q:食材管理");
            WriteLine("W:库存管理");
            WriteLine("E:进货渠道管理：");
            WriteLine("R:供应商管理");
            WriteLine("T:返回");
            Write("请键入字母选择：");

            key = Console.ReadKey().Key;
            WriteLine(key);

            Clear();

            if (key == ConsoleKey.Q)
                tabnum = 0;
            else if (key == ConsoleKey.W)
                tabnum = 1;
            else if (key == ConsoleKey.E)
                tabnum = 2;
            else if (key == ConsoleKey.R)
                tabnum = 3;
            else if (key == ConsoleKey.T)
            {
                Clear();
                mainui();
                return;
            }
            else
            {
                Clear();
                goto backk;
            }


            IDCUI();


        }

        void IDCUI()
        {
            Clear();

            putTable(tabnum);

            WriteLine("Q:添加");
            WriteLine("W:删除");
            WriteLine("E:修改");
            WriteLine("R:返回");
            Write("请键入按键完成对应操作：");

        backk:
            key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.Q)
                Insert();
            else if (key == ConsoleKey.W)
                Delete();
            else if (key == ConsoleKey.E)
                Update();
            else if (key == ConsoleKey.R)
            {
                Clear();
                shicaiui();
            }
            else
            {
                //Clear();
                goto backk;
            }


        }






        public event MyDel2<bool, string[]> CallUpdate;
        void Update()
        {

            int row, column, maxrow, i;//行号，列号,总行数
            string[] hand, updateData = new string[3];



            //获取行
            Clear();
            maxrow = putTable(tabnum);
        backk:
            Write("请输入要修改的行号并按回车确定：");
            try
            {

                row = int.Parse(ReadLine());

            }
            catch (FormatException ex)
            {
                goto backk;
            }
            catch (Exception ex)
            {
                WriteLine("发生异常" + ex.Message);
                pause();
                Update();
                return;
            }


            if (row > maxrow || row < 0)
            {
                //Clear();
                Write("错误：没有该行。   ");
                goto backk;
            }







            //获取表头
            Clear();
            if ((hand = GetTableHand(-1, tabnum)) == null)
            {
                WriteLine("ERROR:未获取到表头");
                pause();
                goto f1;
            }

            i = 0;
            foreach (string s in hand)
            {
                WriteLine($"{i:D3}:  {s}");
                i++;
            }


        //获取列
        backkk:
            Write("请输入要修改的列前面的标号并按回车确定：");
            try
            {

                column = int.Parse(ReadLine());

            }
            catch (FormatException ex)
            {
                goto backkk;
            }
            catch (Exception ex)
            {
                Write("发生异常" + ex.Message);
                pause();
                Update();

                return;
            }

            //对库存表的单独照顾：库存不可以改 食材名和供应商
            if(tabnum==1&&(column==0||column==5))
            {
                WriteLine("此项不可更改");
                pause();
                goto f1;

            }







            if (column >= hand.Length || column < 0)
            {
                //Clear();
                Write("错误：没有该列。  ");
                goto backkk;
            }



            WriteLine($"[{row},{column}]");


            //记录行列数值
            updateData[0] = row.ToString();
            updateData[1] = column.ToString();



            //获取修改的内容
            Clear();
            putTable(tabnum);
            Write($"将第{row:D3}的  {hand[column]}  改为：");
            try
            {
                updateData[2] = ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine("发生异常" + ex.Message);
                pause();
                Update();

                return;
            }








            Clear();
            if (CallUpdate(updateData, tabnum))
            {
                WriteLine("修改成功");

            }
            else
            {
                WriteLine("修改失败");

            }
            Thread.Sleep(500);
        f1:
            Clear();
            IDCUI();


        }






        public event MyDel2<bool, int> CallDelete;

        void Delete()
        {
            int row, rowmax;
            Clear();
            rowmax = putTable(tabnum);
        backk:
            Write("请输入要删除的行号并按回车确定：");
            try
            {

                row = int.Parse(ReadLine());

            }
            catch (FormatException ex)
            {
                goto backk;
            }
            catch (Exception ex)
            {
                WriteLine("发生异常" + ex.Message);
                pause();
                Delete();

                return;
            }

            if (row > rowmax || row < 0)
            {
                //Clear();
                Write("错误：没有该行。   ");
                goto backk;
            }



            WriteLine(row);



            Clear();
            if (CallDelete(row, tabnum))
            {
                WriteLine("删除成功");

            }
            else
            {
                WriteLine("删除失败");

            }
            Thread.Sleep(500);
            Clear();
            IDCUI();

        }

        //用于获取表头
        public event MyDel2<string[], int> GetTableHand;





        public event MyDel2<bool, string[]> CallInsert;

        void Insert()
        {

            Clear();
            //putTable(tabnum);

            string[] hand;
            if ((hand = GetTableHand(-1, tabnum)) == null)
            {
                WriteLine("ERROR:未获取到表头");
                pause();
                goto f1;
            }


            for (int i = 0; i < hand.Length; i++)
            {
            backk:
                try
                {

                    Write($"请输入 {hand[i]}:");
                    hand[i] = ReadLine();


                }
                catch (FormatException ex)
                {
                    goto backk;
                }

                catch (Exception ex)
                {
                    WriteLine("发生异常" + ex.Message);
                    pause();
                    Insert();

                    return;
                }
            }

            foreach (string s in hand)
            {
                WriteLine(s);
            }
            //ReadKey();


            Clear();
            if (CallInsert(hand, tabnum))
            {
                WriteLine("添加成功");

            }
            else
            {
                WriteLine("添加失败");

            }
            Thread.Sleep(500);
        f1:
            Clear();
            IDCUI();


        }




































        public void run()
        {
            mainui();


        }

        public void pause()
        {
            WriteLine("\n按任意键继续");
            ReadKey();
        }


    }
}