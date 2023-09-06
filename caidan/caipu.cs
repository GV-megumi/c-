using System;
using static System.Console;


namespace caidan
{

    class CaiPU : TableType
    {

        
        public CaiPU(UI i)
        {



            i.putTable += putTable;
        }

        void putTable(ref int a)
        {
            if (a == 4)
            {
                WriteLine(12);
            }




        }



    }
}
