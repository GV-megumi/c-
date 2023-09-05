using System;
using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;
using Org.BouncyCastle.Crypto.Fpe;
using static System.Console;

namespace caidan
{

    class UI
    {
        //输入按键
        ConsoleKey key;
        int[] table=new int[6]
        {
        5,
       6,
        2,
        5,
        99,
        99
        };
        int tabnum;

        //自定义委托声明，用于非标准事件,输出表格
        public delegate void MyDel(int a, int b);
        public event MyDel putTable;



        void DoReadKey()
        {
              // 禁用控制台回显
        Console.ForegroundColor = Console.BackgroundColor; // 设置文本颜色和背景色相同
        Console.SetWindowSize(1, 1); // 设置控制台窗口大小为最小，以确保不可见

  
            key = Console.ReadKey(intercept: true).Key; // 设置 intercept 参数为 true，以禁止字符回显
            // 在这里可以处理用户输入的键



        // 还原控制台属性
        Console.ResetColor();
        Console.SetWindowSize(80, 25); // 还原控制台窗口大小
    
        }



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
            WriteLine("请键入字母选择：");

            key = Console.ReadKey().Key;
            WriteLine(key);

            Clear();

            if (key == ConsoleKey.Q)
                tabnum=0;
            else if (key == ConsoleKey.W)
                tabnum=1;
            else if (key == ConsoleKey.E)
                tabnum=2;
            else if (key == ConsoleKey.R)
                tabnum=3;
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

            putTable(tabnum, table[tabnum]);
            IDCUI();


        }

        void IDCUI()
        {

            WriteLine("Q:添加");
            WriteLine("W:删除");
            WriteLine("E:修改：");
            WriteLine("R:返回");
            Write("请键入按键完成对应操作：");

        backk:
            key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.Q)
                ;
            else if (key == ConsoleKey.W)
                DoDelete();
            else if (key == ConsoleKey.E)
                ;
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

        void DoDelete()
        {

            

            //CallDelete();
        }






































        public void run()
        {
            mainui();


        }
    }
}