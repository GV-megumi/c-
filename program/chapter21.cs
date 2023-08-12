
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
        CancellationTokenSource cts = new();





        async Task Readk(CancellationToken c)
        {


            WriteLine("(按f5结束)");
            await Task.Run(() =>


                    {


                        while (true)
                        {

                            try
                            {
                            key = Console.ReadKey(intercept: true).Key;
                            }
                            catch
                            {
                                
                            }
                            clicked = true;
                            if (key == ConsoleKey.F5)
                                stop();
                            //break;

                            if (c.IsCancellationRequested)
                            {
                                WriteLine("读线程终止");
                                return;
                            }
                        }

                    }



            );






        }

        async Task Writek(CancellationToken c)
        {


            await Task.Run(() =>
            {
                while (true)
                {


                    if (c.IsCancellationRequested)
                    {
                        WriteLine("写线程终止");
                        return;
                    }

                    if (!clicked)
                        continue;
                    else
                    {
                        //if (key == ConsoleKey.F5)
                        //break;

                        Write($"       {key}\n");
                        clicked = false;

                    }
                }


            });



        }

        async void waitt()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                cts.Cancel();
            });
        }

        async void stop()
        {
            await Task.Run(() =>
            {

                cts.Cancel();
            });
        }
        public override void f()
        {

            //CancellationTokenSource cts=new();
            CancellationToken token = cts.Token;
            CancellationToken token1 = cts.Token;
            Task read = Readk(token);
            Task write = Writek(token1);

            WriteLine("asdadd;");
            //waitt();





            read.Wait();
            WriteLine("读事件线程结束\n\n");
            write.Wait();
            WriteLine("写事件线程结束\n\n");


            WriteLine("主线程结束");


        }
    }
}