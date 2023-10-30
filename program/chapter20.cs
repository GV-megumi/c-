
using System;
using Org.BouncyCastle.Math.EC;
using static System.Console;


namespace chapter
{

    public class Student
    {
        public string name { get; set; }
        public string sex { get; set; }
        public int sno { get; set; }

        public Student(string a, string b, int c)
        {
            this.name = a;
            this.sex = b;
            this.sno = c;
        }
    }

    class chapter20 : Chapter
    {
        //打印输出
        void printint(IEnumerable<int> list)
        {
            foreach (int i in list)
            {
                Write(i + "  ");
            }
            WriteLine();
        }



        public override void f()
        {
            int[] ints = new int[5];
            int i;
            for (i = 0; i < 5; i++)
                ints[i] = i;





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


            Console.WriteLine("\n\njoin语句："); ;

            int[] inta = { 1, 2, 3 }, intb = { 2, 3, 4 };
            var numjoin =
            from a in inta
            join b in intb on a equals b
            select a;
            printint(numjoin);

            Console.WriteLine("下面这1段与上相同（下面为嵌套查询）");
            var numjoin2 =
            from a in inta
            from b in intb
            where a == b
            select a;
            printint(numjoin2);

            Console.WriteLine("let 字句");
            var numjoin3 =
            from a in inta
            from b in intb
            let sum = a + b
            where sum >= 3 && Array.IndexOf(inta, a) == Array.IndexOf(intb, b)
            select new { a, b, sum };

            foreach (var a in numjoin3)
            {
                Console.WriteLine(a);
            }


            Console.WriteLine("order,group和into联合");

            f2();

        }

        void f2()
        {
            var students = new[]
            {
                new Student("张三","男",12),
                new Student("李四","女",12),
                new Student("王二","男",12),
            };

            var names =
            from s in students
            group s.name by s.sex into groupa
            orderby groupa.Key
            select new
            {
                sex = groupa.Key,
                items = groupa.ToList()

            };


            WriteLine("枚举类型里嵌套一个枚举类型:");
            foreach (var a in names)
            {
                Write(a.sex + ": ");
                foreach (var b in a.items)
                    Write(b + " ");
                WriteLine();

            }



            var names2 =
            from s in students
            group s.name by s.sex into groupa
            orderby groupa.Key descending
            from a in groupa
            select new
            {
                groupa.Key,
                a
            };
            WriteLine("没有枚举类型的嵌套：");
            foreach (var a in names2)
                WriteLine(a);


            f3();

        }

        void f3()
        {
            pause();
            Clear();
            
            Console.WriteLine("标准查询运算符（where）");
            /*
            此处可见Linq查询语句和查询方法的区别
            查询方法的优点：简洁
            */

            int[] a={1,2,3,4,5,6};

            var choose=a.Where(n=>n>4);
            foreach(var i in choose)
            {
                Console.Write(i+ " ");
            }
            Console.WriteLine();


        }
    }
}