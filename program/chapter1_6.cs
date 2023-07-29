
using System;
using static System.Console;
using System.IO;
using chapter;

 


class chapter1_6 : Chapter
{
            //chapter1-7
        public override void f()
        {

            Console.WriteLine("helloqqqqq,world");
            Console.WriteLine("aaaaaa");

            //输出

            int[] num = { 1, 2, 3 };
            int a = 1, b = 2;
            //Console.Write("bbbbbbb{2} and {1}",num); 不可用
            //Console.WriteLine("bbbbbbb{2} and {3}\n",1,2,3);  3:抛出异常
            Console.WriteLine("bbbbbbb{2} and {1}", 1, 2, 3);
            //格式化数字字符串
            Console.WriteLine($"{a:C} and {b:f3}");




            string c = "aaaaaaa";
            WriteLine(c);



            Aa classa = new Aa();

            classa.get();

            void shi(ref int a)
            {
                a++;
            }

            WriteLine($"前：{a}");
            shi(ref a);
            WriteLine($"后：{a}");


            //输出参数是用
            void ref_out(bool a, out int b)
            {

                //return ;  无法返回，因为此时返回b还未被赋值

                //  b+=1;  错误，b还未被赋值

                b = 1;
            }


            ref_out(true, out b);


            WriteLine("\n\n--------------\n\n");



            ref_out(true, out int theb);//c#7.0特性
            WriteLine($"在函数调用时声明的实参theb={theb}");



            WriteLine("\n\n----ref return test----------\n\n");

            Aa r1 = new Aa();

            int refa = r1.getreftest();
            refa += 10;
            WriteLine($"refa={refa}\nprivate={r1.getreftest()}");

            /*refa=20
           private=10
            */

            //ref int refb=r1.getreftest(); 报错
            ref int refb = ref r1.getreftest();
            refb += 10;
            WriteLine($"refa={refb}\nprivate={r1.getreftest()}");
            /*refa=20
            private=20*/



            WriteLine($"\n\na={a}");
            ref int refc = ref a;
            WriteLine($"refc={refc}");
            refc = 10;
            WriteLine($"直接修改后：a={a},refc={refc}");





            int refctest1(int a)
            {
                a += 10;
                return a;
            }

            b = refctest1(refc);
            WriteLine($"形参修改后：a={a},refc={refc}，形参={b}");
            //形参修改后：a=10,refc=10，形参=20

            int refctest2(ref int a)
            {
                a += 10;
                return a;
            }

            b = refctest2(ref refc);
            WriteLine($"引参修改后：a={a},refc={refc}，引参={b}");
            //引参修改后：a=20,refc=20，引参=20








            WriteLine("\n\n----可选参数与parasm参数试用----------\n\n");

            void c_parasmtest(int a = 12, params int[] num)

            {
                WriteLine($"可选参数：{a}");

                foreach (int x in num)
                {
                    WriteLine(x);
                }

                WriteLine("\n");

            }

            c_parasmtest(1, 2, 3, 4);
            /*
            可选参数：1
            2
            3
            4
            */
            /*
            c_parasmtest(,num);------error
            c_parasmtest(num);-------error
            必须先声明前面
            */

        }
}
