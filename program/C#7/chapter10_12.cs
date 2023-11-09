using System;
using static System.Console;
using System.IO;
using chapter;


class chapter10_12 : Chapter
{

           //10,11,12
        public override void f()
        {
            int i;
            int[] a = { 0, 1, 2, 3, 4, 5 };


            for (i = 0; i < 5; i++)
            {
                WriteLine(i);
                switch (i)
                {
                    case 1 when a[i] == 1:
                        WriteLine($"a[{i}]=1");
                        break;

                }

            }



            WriteLine("---------using语句-----------");

            using (TextWriter r = File.CreateText("aa.txt"))
            {
                r.WriteLine("aaaaaaa");
                r.WriteLine("cccccccc");
            }

            using (TextReader r = File.OpenText("aa.txt"))
            {
                WriteLine(r.ReadLine());
            }

            WriteLine();



            TextReader rr = File.OpenText("aa.txt");
            WriteLine(rr.ReadLine());
            WriteLine();

            using (rr)
            {
                WriteLine(rr.ReadLine());
            }
            WriteLine();
            //WriteLine(rr.ReadLine()); 不可用，因为已被using回收,
            //********此时rr指向空
            //重新指向后又可用
            rr = File.OpenText("aa.txt");
            WriteLine(rr.ReadLine());
            rr.Close();








            WriteLine("---------结构体-----------");

            struct1 s1, s2 = new struct1(1, 2);
            s1.x = 1;
            WriteLine($"s1.x={s1.x}");
            //WriteLine($"s1.y={s1.y}");  // 不可用，未赋值，即使在结构体内事先赋值也不可用
            WriteLine($"s2:  x={s2.x},y={s2.y},z={s2.getz()}");// static类型可事先赋值




            WriteLine("------------枚举-----------");

            /*
                enum enum1
              {
                  a,
                  b,
                  c,
              }
            */

            enum1 enum1a=enum1.a;
            enum1 enum1b=enum1.b;
            WriteLine($"未给a赋初值：{(int)enum1a} {(int)enum1b}");

            enum2 enum2a=enum2.a;
            enum2 enum2b=enum2.b;
            WriteLine($"  给a赋初值：{(int)enum2a} {(int)enum2b}");










            







        }

}