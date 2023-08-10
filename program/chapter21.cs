
using System;
using static System.Console;
using System.IO;
using chapter;

namespace chapter
{
    class chapter21 : Chapter
    {

        ConsoleKey key = ConsoleKey.F5;
        bool clicked = false;
        int over=0;




        async void Readk()
        {


            WriteLine("(按f5结束)");
            await Task.Run(() =>


                    {

                        
                        while (true)
                        {

                            key = Console.ReadKey(intercept: true).Key;
                            clicked = true;
                            if (key == ConsoleKey.F5)
                                break;
                        }
                       
                    }



            );


            WriteLine("读事件线程结束\n\n");
            over++;



        }

        async void Writek()
        {


            await Task.Run(() =>
            {
                while (true)
                {

                    if (!clicked)
                        continue;
                    else
                    {
                        if(key == ConsoleKey.F5)
                        break;
                        Write($"       {key}\n");
                        clicked = false;

                    }
                }
                

            });

            WriteLine("写事件线程结束\n\n");
            over++;
        }
        public override void f()
        {
            Readk();
            Writek();

            WriteLine("asdadd;");
            while (over!=2)
                ;

            WriteLine("主线程结束");


        }
    }
}