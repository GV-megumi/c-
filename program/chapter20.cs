
using System;
using static System.Console;
using System.IO;
using chapter;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace chapter
{
    class chapter20 : Chapter
    {



        public override void f()
        {
            int[] ints = new int[5];
            int i;
            for (i = 0; i < 5; i++)
                ints[i] = i;

            //打印输出
            void printint(IEnumerable<int> list)
            {
                foreach (int i in list)
                {
                    Write(i + "  ");
                }
                WriteLine();
            }



            //查询语句
            var num =

            from n in ints
            where n >= 0
            select n;
            printint(num);
            WriteLine(num.Count());//sql count 函数

            /*
            此段与上面效果一样
            var num =
            (from n in ints
            where n >= 0
            select n).Count();
            WriteLine(num);//sql count 函数
            */

            //方法语句，lambda表达式
            var numb = ints.Where(n => n >= 0);
            printint(numb);


            /*
            总结：一般用查询语句，但是有一些运算符必须用方法语句来写
            */

            ints[2] = -12;
            printint(num);
            WriteLine(num.Count());//sql count 函数






        }
    }
}