using System;
using static System.Console;
using System.IO;

 
namespace chapter{

    //const int adsaff=1; 报错

           class Aa
    {
        const int adsaff=1;


        private int reftest = 10;

        string aa = "类Aa的成员汴梁";
        public void get() { WriteLine(aa); }

        public ref int getreftest() { return ref reftest; }



    }

    class Aaa:Aa
    {
        public int x{get;set;}   

        public static implicit operator Aaa(struct1 s)
        {
            Aaa aaa=new Aaa();
            aaa.x=s.x;
            return aaa;
        }

    }

    
    struct struct1
    {
        public int x;
        public int y = 0;
        public static int z = 1;
        public struct1(int a, int b)
        {
            this.x = a;
            this.y = b;
            //this.z = 1;  不可声明静态


        }

        public struct1(){}
        static struct1()
        {
            z = 2;
            //x=2;
            //z=x;
        }
        public int getz()
        {
            return z;

        }
    }

    enum enum1
    {
        a,
        b,
        c,
    }

        enum enum2
    {
        a=10,
        b,
        c,
    }

    abstract class Chapter {

        public abstract void f();
        public void pause()
        {
            WriteLine("\n按任意键继续");
            ReadKey();
        }
    }
}