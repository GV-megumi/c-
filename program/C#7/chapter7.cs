using System;
using static System.Console;
using System.IO;
using chapter;


class chapter7 : Chapter


{

    private int mValue;
    int aaaa;

    public int MValue 
    {
        set {mValue=value;}
        get { return mValue; }
    }
    //c#7.0新语法
    public int MValue2
    {
        set=>mValue=value;
        get=>mValue;

    }

    //自动属性
    public int value2{set;get;}

    //自动只读属性
    public int value3{get;}

    static int aa{set;get;}

    public int this [int a]
    {
        set
        {
            switch(a)
            {
                case 0: MValue=value;
                break;
                case 1: aaaa=value;
                break;
                case 3: value2=value;
                break;


            }
        }
        get
        {
            switch(a)
            {
                case 0: return MValue;
                case 1: return  aaaa;
                case 3: return value2;
                default: return 114514;



            }
        }
    }

    
    
        public override void f()
        {
            WriteLine("---------属性---------");
            this.MValue=1;
            WriteLine(this.MValue);
            //c#7.0新语法
            this.MValue2=123;
            WriteLine(this.MValue);
            //自动属性
            value2=1234;
            WriteLine("自动属性："+value2);
            //自动只读属性    在构造函数中初始化
            WriteLine("自动只读属性："+value3);


            //对象初始化
            chapter7 c7=new chapter7(){MValue=1};
            //索引器
            c7[0]=12;
            c7[1]=134;
            c7[3]=0;

            WriteLine($"{c7[0]}  {c7[1]}   {c7[3]}");



            
                
            

            
        }

        public chapter7()
        {
            value3=9999;
        }
}