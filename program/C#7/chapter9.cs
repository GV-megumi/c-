using System;
using static System.Console;
using System.IO;
using chapter;


class chapter9 : Chapter
{

            //9
         public override void f()
        {

            int a = 1, b = 2;
            long d = 500_000_000;
            WriteLine($"{0x16}\n {d}\n-------字符串常规与逐字字面量区别：-----------");

            string s1 = "aaa\x0009bbb\"bbb ";
            WriteLine(s1);
            WriteLine();
            string s2 = @s1;//s1在赋值给s2前已被转义
            WriteLine(s2);
            s2 = @"aaa\x0009bbbbbb""";
            WriteLine(s2);


            double a1 = 11.52, a2 = 1.512;
            WriteLine($"-----------求余运算符：------------\n{a1}%{a2}={a1 % a2}");

            WriteLine("-------------浅比较与引用的直接赋值------------------\n浅引用在简单类型：");

            ref int aref = ref a;
            WriteLine(aref == a);
            b = a;
            WriteLine();
            WriteLine(aref == b);

            WriteLine("浅引用在类：");

            Aa class1 = new Aa();
            Aa class2 = class1;
            Aa class3 = new Aa();
            ref Aa class4 = ref class1;
            WriteLine(class1 == class2);//true
            WriteLine();
            WriteLine(class1 == class4);//true
            WriteLine(class1 == class3);//False

            //表明 引用的直接赋值   class2=class1; 则class1与class2指向同一内存地址,但1与2指向两个不同的ref
            //                                   class4与class1 指向同一个ref

            WriteLine("------------------");

            a = (int)3.1415;

            WriteLine($"double:{a1}   int:{(int)a1}");


            WriteLine(class1.GetType().Name);

            WriteLine(nameof(a));








        }
}
