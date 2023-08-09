using System;
using static System.Console;
using System.IO;
using chapter;



namespace Nchapter15
{


    //一个包含订阅者和发布者的完整事件程序示例：


    //自定义委托声明，用于非标准事件
    public delegate void MyDel(ConsoleKey c);





    //继承自EventArgs 的类，用于在标准事件中传递数据
    public class PutKeynumEventArgs : EventArgs
    {
        //存储按键信息
        public ConsoleKeyInfo keyInfo { get; set; }
    }




    public class 发布者
    {

        //非标准事件
        public event MyDel event1;


        //非标准事件触发程序
        public void 触发事件()
        {
            if (event1 != null)
            {

                ConsoleKeyInfo keyInfo;
                WriteLine("触发事件1(按f5结束)");
                while (true)
                {
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.F5)
                        break;
                    event1(keyInfo.Key);
                }
                WriteLine("事件结束\n\n");

            }

        }







        //标准事件      事件访问器
        public event EventHandler<PutKeynumEventArgs> event2;

        //标准程序触发程序
        public void 触发事件2()
        {
            //EventArgs的子类，存放了一个按键的信息
            PutKeynumEventArgs k = new PutKeynumEventArgs();
            if (event2 != null)
            {

                WriteLine("触发事件2(按f5结束)");


                while (true)
                {
                    k.keyInfo = Console.ReadKey();
                    if (k.keyInfo.Key == ConsoleKey.F5)
                        break;
                    event2(this, k);
                }
                WriteLine("事件结束\n\n");

            }

        }

    }

    public class 订阅者
    {
        //int num;

        public 订阅者(发布者 f)
        {
            f.event1 += print;
            f.event2 += print2;

        }


        //非标准事件处理程序
        void print(ConsoleKey c)
        {

            WriteLine("调用了处理程序print（）    你按了  " + c);


        }






        //标准事件处理程序
        void print2(object source, PutKeynumEventArgs e)
        {

            WriteLine("调用了处理程序print2（）        你按了  " + e.keyInfo.Key+source.GetType());


        }

        //移除事件处理程序
        public void DeleteEvents(发布者 f)
        {
            f.event2 -= print2;
            f.event1 -= print;
        }



    }



















    class chapter15 : Chapter
    {


        //10,11,12
        public override void f()
        {

            发布者 f = new 发布者();
            订阅者 d = new 订阅者(f);


            //"触发事件(按f5结束)
            f.触发事件();
            f.触发事件2();

            WriteLine("移除了事件处理程序：");
            d.DeleteEvents(f);

            WriteLine("此时再调用触发事件的代码：");
            f.触发事件();

            WriteLine("因为触发事件中没有处理程序，所以没有任何效果。");









            Console.WriteLine("----------ReadKey()----------");
            Console.WriteLine("Press any key to continue...");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            Console.WriteLine("\nYou pressed the key: " + keyInfo.Key);
            Console.WriteLine("Character entered: " + keyInfo.KeyChar);


        }
    }







}