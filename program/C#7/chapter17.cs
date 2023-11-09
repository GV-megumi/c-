using System;
using static System.Console;
using System.IO;
using chapter;


class Chapter17 : Chapter
{

    double Return(double a)
    {
        return a;
    }


    public override void f()
    {
        double d;
        int a;

        d=3.51415;
        a=checked((int)d);//抛出异常
        WriteLine(a);
        a=unchecked((int)d);
        WriteLine(a);
        a=checked((int)Return(d));
        WriteLine(a);


        //子类->父类  是隐式转换
        Aa aa;
        Aaa aaa=new Aaa();
        aa=aaa;

        WriteLine("自定义的转换:");
        //类的定义见chapter.cs
        struct1 st1= new();
        st1.x=12;
        WriteLine($"struct.x=  {st1.x}");
        aaa=st1;
        WriteLine($"隐式转换后，Aaa.x=  {aaa.x}");


    }
}