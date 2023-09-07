using System;

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
        public delegate void MyDel1(ref int a);

        //非标准事件委托，调用
        public delegate T MyDel2<T, R>(R a, int tablenum);

        //输出表格
        public event MyDel1? putTable;







        void mainui()
        {

            WriteLine("Q:食材管理");
            WriteLine("W:菜谱管理");
            WriteLine("E:菜单");
            WriteLine("R:退出");
            Write("请键入字母选择：");
        backk:
            key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Q)
            {
                Clear();
                shicaiui();
            }
            else if (key == ConsoleKey.W)
            {
                Clear();
                tabnum = 4;
                IDCUI();
            }
            else if (key == ConsoleKey.E)
                ;
            else if (key == ConsoleKey.R)
                return;
            else
            {
                goto backk;
            }

        }

        void shicaiui()
        {

            WriteLine("Q:食材管理");
            WriteLine("W:库存管理");
            WriteLine("E:进货渠道管理：");
            WriteLine("R:供应商管理");
            WriteLine("T:返回");
            Write("请键入字母选择：");
        backk:
            key = Console.ReadKey(intercept: true).Key;

            

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
                goto backk;
            }


            IDCUI();


        }

        void IDCUI()
        {
            Clear();
            int rowmax = tabnum;
            putTable?.Invoke(ref rowmax);
            rowmax = 0 - rowmax;

            if (rowmax == -1)
            {
                WriteLine("错误：未获取到表格,将返回到主菜单");
                pause();
                mainui();
                return;
            }

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






        public event MyDel2<bool, string[]>? CallUpdate;
        void Update()
        {

            int row, column, rowmax = tabnum, i;//行号，列号,总行数
            string[]? hand, updateData = new string[3];



            //获取行
            Clear();

            putTable?.Invoke(ref rowmax);
            rowmax = 0 - rowmax;

            if (rowmax == -1)
            {
                WriteLine("错误：未获取到表格,将返回到主菜单");
                pause();
                mainui();
                return;
            }
        backk:
            Write("请输入要修改的行号并按回车确定：");
            try
            {

                row = int.Parse(ReadLine() ?? "-999");

            }
            catch (FormatException)
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


            if (row > rowmax || row < 0)
            {
                //Clear();
                Write("错误：没有该行。   ");
                goto backk;
            }







            //获取表头
            Clear();
            if ((hand = GetTableHand?.Invoke(-1, tabnum)) == null)
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

                column = int.Parse(ReadLine() ?? "-99");

            }
            catch (FormatException)
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
            if (tabnum == 1 && (column == 0 || column == 5))
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
            rowmax = tabnum;
            putTable?.Invoke(ref rowmax);
            rowmax = 0 - rowmax;
            if (rowmax == -1)
            {
                WriteLine("错误：未获取到表格,将返回到主菜单");
                pause();
                mainui();
                return;
            }
            Write($"将第{row:D3}的  {hand[column]}  改为：");
            try
            {
                updateData[2] = ReadLine() ?? " ";
            }
            catch (Exception ex)
            {
                WriteLine("发生异常" + ex.Message);
                pause();
                Update();

                return;
            }








            Clear();
            if (CallUpdate?.Invoke(updateData, tabnum) ?? false)
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






        public event MyDel2<bool, int>? CallDelete;

        void Delete()
        {
            int row, rowmax = tabnum;
            Clear();
            putTable?.Invoke(ref rowmax);
            rowmax = 0 - rowmax;

            if (rowmax == -1)
            {
                WriteLine("错误：未获取到表格,将返回到主菜单");
                pause();
                mainui();
                return;
            }
        backk:
            Write("请输入要删除的行号并按回车确定：");
            try
            {

                row = int.Parse(ReadLine() ?? "-1");

            }
            catch (FormatException)
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
            if (CallDelete?.Invoke(row, tabnum) ?? false)
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
        public event MyDel2<string[], int>? GetTableHand;





        public event MyDel2<bool, string[]>? CallInsert;

        void Insert()
        {

            Clear();
            //putTable(tabnum);

            string[]? hand;
            if ((hand = GetTableHand?.Invoke(-1, tabnum)) == null)
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
                    hand[i] = ReadLine() ?? " ";


                }
                catch (FormatException)
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
            if (CallInsert?.Invoke(hand, tabnum) ?? false)
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

































        public delegate void Link();
        public event Link? LinkToSql;




        public void run()
        {
            LinkToSql?.Invoke();

            mainui();


        }

        public void pause()
        {
            WriteLine("\n按任意键继续");
            ReadKey();
        }


    }
}