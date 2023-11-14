#define conn


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
                a = new chapter25();
                //a=new Sql();
            }


            


            void shi()
            {
         
            }
            shi();





            a.f();
            //Game.f();
            //putCaidan();




            //chapter25 额外内容
            //chapter25 asd=new chapter25();
            //asd.Gvf3();








            a.pause();



        }
    }

}