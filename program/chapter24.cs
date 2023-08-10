#define fh




using System;
using static System.Console;
using System.IO;
using chapter;


class chapter24 : Chapter
{
    public override void f()
    {
        //WriteLine(fh); error

#if fh
        WriteLine("fh is exit");
#warning fh is exit 
        //见问题警告

#else
        WriteLine("fh is not exit");
#endif


        WriteLine("异常：");

        int x=10;
        try
        {
            x/=0;
        }
        catch(Exception e)
        {
            WriteLine($"异常{e.StackTrace}：不能除0");
            throw;
        }




    }

}