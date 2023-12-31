#define asddd
#define conn
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using chapter;
using Org.BouncyCastle.Math.EC;

namespace chapter
{
    //反射和特性
    class chapter25 : Chapter
    {
        public override void f()
        {

            Aa aa = new();

            Console.WriteLine("Type类 的成员");
            Type t = aa.GetType();
            Console.WriteLine(t.Name);
            Console.WriteLine(t.Namespace);
            Console.WriteLine(t.Assembly);//返回程序集

            //内嵌输出函数
            void print<T>(T[] t) where T : MemberInfo
                //FieldInfo、PropertyInfo 和 MethodInfo 都是 MemberInfo 的子类。
            {
                foreach (var f in t)
                    Console.WriteLine(f.Name);
                Console.WriteLine();
            }

            //Console.WriteLine(t.GetFields());字段列表
            FieldInfo[] fieldInfos = t.GetFields();
            Console.WriteLine("类Aa 的字段列表");
            print(fieldInfos);

            //Console.WriteLine(t.GetProperties());
            PropertyInfo[] propertyInfos = t.GetProperties();
            Console.WriteLine("类Aa 的属性列表");
            print(propertyInfos);

            //Console.WriteLine(t.GetMethods());
            MethodInfo[] methodInfos = t.GetMethods();
            Console.WriteLine("类Aa 的方法列表");
            print(methodInfos);



            Gvf2();
        }












        //.net 预定义特性




        //Obsolete
        [Obsolete("过时1")]
        public void WarningTip1()
        {
            Console.WriteLine("Warning");

        }



        [Obsolete("过时2", false)]
        public void WarningTip2()
        {
            Console.WriteLine("Warning");

        }

        [Obsolete("过时3", true)]
        public void ErrorTip()
        {
            Console.WriteLine("Error");
        }

        public void Gvf2()
        {
            Console.Clear();
            PrintYellow("Obsolete特性：\n");
            WarningTip1();      //“chapter25.WarningTip1()”已过时:“过时1”
            WarningTip2();      //“chapter25.WarningTip2()”已过时:“过时2”
            //ErrorTip();       //“chapter25.ErrorTip()”已过时:“过时3”

            Gvf3();
        }


        //Conditional

        [Conditional("conn")]
        public void ConditionalPrint(string s) { Console.WriteLine(s+"编译了"); }

        public void Gvf3([CallerMemberName] string memberName = "")
        {

            PrintLn("\n\nConditional特性：\n");
            ConditionalPrint("调用函数：  ");//需要 #define conn 才会调用  作用域在本文件内
            Console.WriteLine("f3()函数退出");





            
            if (memberName == "Gvf2")
                Gvf4();

        }












        //调用者信息
        int lineNumber;
        public void CallerAttributeTest(
            string str,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")

        {
            Console.WriteLine($"信息： {str}");
            Console.WriteLine($"文件路径： {filePath}");
            Console.WriteLine($"代码行数： {lineNumber}");
            Console.WriteLine($"调用者： {memberName}");
            this.lineNumber = lineNumber;  //信息可被存在外部



        }


        public void Gvf4()
        {

            PrintLn("\n\n调用者信息特性：\n", "YellOw");

            CallerAttributeTest("test");
            Console.WriteLine($"{lineNumber}");

        }








    }

}