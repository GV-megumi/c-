
using System;
using static System.Console;
using System.IO;
using chapter;
using Nchapter15;
using Nchapter16;
using Nchapter18;
using Nchapter19;


namespace asss
{

    class Program
    {

        static void Main()
        {

           

            Chapter a;
            bool za=false;
            //za=true;
            if(za)
            a=new za();
            else
            {
            //a=new chapter1_7();
            //a=new chapter9();
            //a= new chapter10_12();
            //a= new chapter13();
            //a= new chapter7();
            a= new chapter19();
            }


            a.f();
            a.pause();
  


        }
    }

}