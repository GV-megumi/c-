using System;
using static System.Console;
using System.IO;
using chapter;


namespace chapter14
{




    //声明委托类型
    delegate void MyDel(int value);
    delegate void MyDel2();
    //参数数组与委托，匿名方法
    delegate void MyDel3(params int[] num);




    class Test
    {

        public void PrintLow(int v)
        {
            Console.WriteLine($"{v}<50");
        }
        public void PrintHigh(int v)
        {
            Console.WriteLine($"{v}>=50");
        }

        public void testt1()
        {
            WriteLine("调用了方法1");
        }

        public void testt2()
        {
            WriteLine("调用了方法2");
        }
        public void testt3()
        {
            WriteLine("调用了方法3");
        }
    }



    class chapter14 : Chapter
    {

        void paramsput(int[] num)
        {
            WriteLine("\n调用了类方法，参数数组的元素有：");
            for (int i = 0; i < num.Length; i++)
            {
                Write(num[i] + " ");
            }
        }


        public override void f()
        {



            //创建测试类
            Test test = new Test();
            MyDel myDel;
            MyDel2 myDel2;

            Random random = new Random();
            int value = random.Next(99);


            //此时mydel持有两个方法
            myDel = value < 50
            ? new MyDel(test.PrintLow)
            : new MyDel(test.PrintHigh);
            //执行委托,把值传给委托并让它判断
            myDel(value);


            WriteLine("一个委托顺序调用三个方法：");
            myDel2 = test.testt1;
            myDel2 += test.testt2;
            myDel2 += test.testt3;
            myDel2();

            WriteLine("\n\n另一种调用方式：");

            myDel2?.Invoke();

            WriteLine("\n\n减去了方法2：");
            myDel2 -= test.testt2;
            myDel2();


            WriteLine("\n同一个方法可调用多次：");
            myDel2 += test.testt3;
            myDel2();


            WriteLine("\n添加了一个匿名方法：");
            myDel2 += delegate ()
            {
                WriteLine("调用了匿名方法");
            }
            ;
            myDel2();



            WriteLine("-----------数数组与委托，匿名方法---------");
            MyDel3 myDel3 = delegate (int[] num)
            {
                WriteLine("\n调用了匿名方法，参数数组的元素有：");
                for (int i = 0; i < num.Length; i++)
                {
                    Write(num[i] + " ");
                }
            }
            ;
            myDel3(1, 2, 3, 4, 5, 12, 3);

            WriteLine("\n加入了一般方法:");
            myDel3 += this.paramsput;
            myDel3(1, 2, 3, 4, 5, 12, 3);
            WriteLine();






        }

    }



}