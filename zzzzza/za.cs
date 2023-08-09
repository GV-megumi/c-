using System;
using static System.Console;
using System.IO;

using chapter;



class za: Chapter
{
     public override void f()
     {
        WriteLine("----------随机数-----------");
        Random random= new Random();
        WriteLine(random.Next());
        WriteLine(random.Next(99));
        WriteLine(random.Next(-12,0));
        WriteLine(random.NextDouble());

     }
}