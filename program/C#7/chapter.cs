using System;
using static System.Console;
using System.IO;


namespace chapter
{


    abstract class Chapter
    {

        public abstract void f();






        //控制台输出用

        static void PrintStr(string s, bool line, string color = "YELLOW")
        {

            Console.ForegroundColor = ChooseStrColor(color);

            //line :  要换行为true
            if (line)
                Console.WriteLine(s);
            else
                Console.Write(s);


            Console.ResetColor();

        }

        static ConsoleColor ChooseStrColor(string s)
        {
            s = s.ToLower();
            switch (s)
            {
                case "yellow":
                    return ConsoleColor.Yellow;
                case "red":
                    return ConsoleColor.Red;
                case "green":
                    return ConsoleColor.Green;
                default:
                    PrintStr($"暂未匹配颜色：{s}",true,"reD");
                    return ConsoleColor.White;

            }

        }

        public static void Print(string s, string color = "YELLOW")
        {
            PrintStr(s, false, color);
        }

        public static void PrintLn(string s, string color = "YELLOW")
        {
            PrintStr(s, true, color);
        }



        [Obsolete("有更好的函数,请使用Print（）或PrintLn（）")]
        public void PrintYellow(string s, bool line = true)
        {
            //line :  要换行为true
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(s);
            Console.ResetColor();

        }




















        public void pause()
        {
            WriteLine("\n按任意键继续");
            ReadKey();
        }
    }
}