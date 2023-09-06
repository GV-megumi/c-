using System;
using static System.Console;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace caidan
{

    class CaiPU
    {
        public CaiPU(UI i)
        {
            i.putTable+=putTable;

        }

        int putTable(int a)
        {
            if(a==4)
            WriteLine(12);
            WriteLine(22);
            return 123;
        }

   

    }
}
