using System;
using static System.Console;
using System.IO;
using chapter;





namespace Inherit
{

    class Father
    {
        internal int a, b;

        public static int sb;

       

        static Father()
        {
            sb = 1234;
        }

        internal Father(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        protected Father() { }
        public int sum()
        {
            return a + b;
        }


    }





    class Son : Father
    {
        int c;
        new public static int sb;
        static Son()
        {
            sb = 1432;
        }

        public Son(int a, int e, int c) : base(a, e)
        {
            this.c = c;

        }
    }



    //密封类
    sealed class Son2 : Father
    {
        public Son2(int a, int b) : base(a, b)
        {

        }


    }


    //静态类  拓展类
    static class Extend
    {
        public static double average(this Father s)
        {
            return s.sum() / 2.0;
        }

    }



    class chapter8 : Chapter
    {
        //chapter1-7
        public override void f()
        {

            WriteLine("---------密封类------------");

            Father a = new Son2(3, 4);

            WriteLine($"类Son2调用自身函数sum()  得  {a.a}+{a.b}={a.sum()}");

            WriteLine("------扩展方法-----------");
            WriteLine($"类Son2调用扩展的方法average()  得  平均数={a.average()}");


        


        }
    }


}
