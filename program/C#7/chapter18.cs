using System;
using static System.Console;
using System.IO;
using chapter;


namespace Nchapter18
{



    interface Imyi<T> where T : struct
    {
        void PrintType(T name);
    }



    class Ca : Imyi<int>
    {
        public void PrintType(int name)
        {
            WriteLine(name.GetType());
        }
    }


    class Cb<T, S> : Imyi<T>
     where T : struct
     where S : struct
    {
        public void PrintType(T name)
        {
            WriteLine(name.GetType());
        }

        public void PrintType1(S name)
        {
            WriteLine(name.GetType());
        }

        public void PrintType2<Q>(Q name)
        {
            WriteLine(name.GetType());
        }

    }


    static class C
    {
        //类cb的扩展方法
        public static void PrintType3<T, S>(this Cb<T, S> cb, T t) 
        where T : struct 
        where S : struct
        {
            cb.PrintType2<T>(t);

        }

    }
    //泛型委托
    delegate void MyD1<T>(T v);
    delegate T MyD2<T,R>(R a,R b);










    //协变和逆变
    delegate T Myout<out T>();
    delegate void Myin<in T>(T father);

    class Father
    {
        public int a{set;get;}
    }
    class Son:Father
    {

    }

    class Chapter18 : Chapter
    {


        Son MakeSon()
        {
            return new Son();
        }

        static void puta(Father father)
        {
            WriteLine(father.a);
        }


        //协变和逆变,适用于引用类型（委托，接口
        void f2()
        {

            Myout<Son> mks=MakeSon;
            Myout<Father> mkf=mks;//没有 out 参数 此处报错
            Myin<Father> myin=puta;
            Myin<Son> myin1=myin;//没有 in 参数 此处报错


        }



        void f1(int a)
        {
            WriteLine(a);
        }

        bool f1(int a, int b)
        {
            WriteLine($"{a}+ {b}");
            return true;
        }

        void f2(double a)
        {
            WriteLine(a);
        }

        bool f2(double a, double b)
        {
            WriteLine($"{a}+ {b}");
            return true;
        }


        public override void f()
        {
            Ca c;
            c = new Ca();
            c.PrintType(123);

            var cb = new Cb<int, double>();
            Write("接口泛型T:");
            cb.PrintType(0);
            Write("类泛型S:");
            cb.PrintType1(3.12);
            Write("函数泛型Q:");
            cb.PrintType2<byte>(3);
            Write("Cb的扩展方法：");
            //此处类型为int     -》   var cb = new Cb<int, double>();
            //！！！！！！！！！！！调用函数不用<>,d但声明函数要<>,用于类的<>   <> : 泛型参数！！！！！！！！！！！！！
            cb.PrintType3(4);



            WriteLine("泛型委托");
            var myD1=new MyD1<int>(f1);
            myD1(1);

            var myD12=new MyD1<double>(f2);
            myD12(12.2);


            var myD2=new MyD2<bool,int>(f1);
            myD2(1,2);

            var myD22=new MyD2<bool,double>(f2);
            myD22(1.1,1.34);




        }
    }
}