
using System;
using static System.Console;
using System.IO;
using chapter;

namespace Nchapter16
{


    interface IMyInterface

    {

        public int f();
    }


    class Player : IComparable
    {
        public string? PlayerName { get; set; }
        public int Score { get; set; }

        public Player()
        {
            Random random = new();
            Score = random.Next(1, 100);
        }

        public int CompareTo(object o)
        {

            Player player = (Player)o;
            if (this.Score < player.Score)
                return -1;
            if (this.Score > player.Score)
                return 1;

            return 0;

        }
    }




    class Chapter16 : Chapter
    {

        static void  PrintScore(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Write(players[i].Score + "   ");
            }
            WriteLine();
        }



        //chapter1-7
        public override void f()
        {

            WriteLine("接口和实现自定义Sort");

            Player[] players = new Player[5];

            //初始化
            for (int i = 0;i<players.Length;i++)
            {
                players[i]=new Player();
            }

            Write("Score：   ");
            PrintScore(players);

            Write("sort:     ");
            Array.Sort(players);
            PrintScore(players);





        }
    }

}