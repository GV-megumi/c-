using System;
using static System.Console;


namespace caidan
{

    class CaiPU : TableType
    {


        string[,] caiPu = new string[100, 6];
        int[] strlens;
        //食材数量
        public int caiPunum;


        public CaiPU(UI i)
        {
            caiPunum = 0;
            caiPu[0, 0] = "名称";
            caiPu[0, 1] = "主料";
            caiPu[0, 2] = "调料";
            caiPu[0, 3] = "做法步骤";
            caiPu[0, 4] = "菜系";
            caiPu[0, 5] = "荤素";
            strlens = new int[] { 2, 2, 2, 4, 2, 2 };



            i.putTable += putTable;
            i.LinkToSql += LinkWithTable;
        }





















        //连接数据库

        public delegate void MySQLGetTableDel(string[,] caiPU,ref int Caipunum,int[] strlens);
        public event MySQLGetTableDel SqlGetTable;

        void LinkWithTable()
        {
            SqlGetTable(caiPu,ref  caiPunum,strlens);

        }

        void putTable(ref int a)
        {
            if (a == 4)
            {
                base.putTable(caiPu, strlens);
            }




        }



    }
}
