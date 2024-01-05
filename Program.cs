//#define conn


using System;
using static System.Console;
using System.IO;
using chapter;
using Nchapter15;
using Nchapter16;
using Nchapter18;
using Nchapter19;
using GuessWord;
using caidan;
using System.Xml.Linq;
using Google.Protobuf.Reflection;


namespace asss
{

    class Program
    {

        public static void putCaidan()
        {


            UI caiDanUI = new();
            ShiCai shiCai = new(caiDanUI);
            MyFoodSql myFoodSql = new(shiCai);
            Clear();
            caiDanUI.run();


            caiDanUI.pause();


        }

        static void Main()
        {



            Chapter a;
            bool za = false;
            //za=true;
            if (za)
                a = new za();
            else
            {
                //a=new chapter1_7();
                //a=new chapter9();
                //a= new chapter10_12();
                //a= new chapter13();
                //a= new chapter7();
                a = new chapter20_1();
                //a=new Sql();
            }





            void shi()
            {


                a.pause();

            }
            //shi();





            a.f();

            //Game.f();
            //putCaidan();
            //extra();













            a.pause();



        }

        static void extra()
        {
            //chapter25 额外内容
            
            chapter25 asd=new chapter25();
            chapter25.PrintLn("chapter25 额外内容");
            asd.Gvf3();


        }
    }

}