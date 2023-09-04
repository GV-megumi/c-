
using System;
using static System.Console;
using System.IO;
using chapter;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace chapter
{
    class chapter20 : Chapter
    {
        void printint(IEnumerable<int> list)
        {
            foreach (int i in list)
            {
                Write(i+"  ");

            }
            WriteLine();
        }

      
        public override void f()
        {
            int[ ] ints= new int[5];
            int i;
            for(i=0;i<5;i++)
             ints[i]= i;
            IEnumerable<int> num= from n in ints
                    where n>=0
                    select n;

            printint(num);

            ints[2]=123;
            printint(num);
            

            


            
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 