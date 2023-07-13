
using System;

namespace asss
{
         class program
    {
        static void Main()
        {
            Console.WriteLine("helloqqqqq,world");
            Console.WriteLine("aaaaaa");

            int[] num={1,2,3};
            int a=1,b=2;
            //Console.Write("bbbbbbb{2} and {1}",num); 不可用
            //Console.WriteLine("bbbbbbb{2} and {3}\n",1,2,3);  3:抛出异常
            Console.WriteLine("bbbbbbb{2} and {1}",1,2,3);
            //格式化数字字符串
            Console.WriteLine($"{a:C} and {b:f3}");



            

        }
    }

}