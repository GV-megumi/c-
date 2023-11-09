using System;
using static System.Console;
using System.IO;
using chapter;
using System.Collections;

namespace Nchapter19
{

    class Color<R> : IEnumerator<R>, IEnumerable<R>
    {

        int num = -1;
        R[] s;


        public R Current
        {
            get
            {
                if (num == -1 || num >= s.Length)
                {
                    WriteLine("Error：溢出");
                    throw new InvalidOperationException();
                }
                return s[num];


            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }



        public bool MoveNext()
        {
            if (num < s.Length - 1)
            {
                num++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            num = -1;
        }

        public IEnumerator<R> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Color(R[] r)
        {
            s = new R[r.Length];
            for (int i = 0; i < r.Length; i++)
                s[i] = r[i];
        }
    }



    //通常IEnumerator<R>, IEnumerable<R>，两个接口用不同的类实现：单一责任制原则

    /*在实现枚举器时，IEnumerator 和 IEnumerable 接口通常应该在两个不同的类中实现，
    每个类负责一个接口的实现。这样的设计更符合单一责任原则，使得代码更加清晰、模块化和易于理解。

    具体来说，一般情况下：

    枚举器（IEnumerator 接口）的实现：创建一个单独的类来实现 IEnumerator 接口，这个类负责维护枚举
    器的状态（例如当前位置）和逻辑（例如移动到下一个元素、获取当前元素）。这个类的实例将用于在集合内部遍历元素。

    可枚举类型（IEnumerable 接口）的实现：创建另一个类来实现 IEnumerable 接口，这个类负责返回枚举
    器的实例，以便外部代码可以通过 foreach 循环遍历集合元素。这个类的实例将用于在外部遍历集合元素。
   */
    class ColorPut<R> : IEnumerable<R>
    {
        R[] s;

        public ColorPut(R[] r)
        {
            s = new R[r.Length];
            for (int i = 0; i < r.Length; i++)
                s[i] = r[i];

        }

        public IEnumerator<R> GetEnumerator()
        {
            return new Color<R>(s);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }






    class chapter19 : Chapter
    {

        void puteach<T>(Color<T> strings)
        {
            foreach (T a in strings)
            {
                Write(a+ "");
            }
            WriteLine();

        }

        void puteach<T>(ColorPut<T> strings)
        {
            foreach (T a in strings)
            {
                Write(a + "  ");
            }
            WriteLine();

        }


        public override void f()
        {

            WriteLine("枚举器：");
            string[] s = { "qwe", "ert", "qwe" };
            int[] i = { 1, 2, 3, 4, 5, };

            Color<string> strings = new(s);
            puteach<string>(strings);


            ColorPut<int> ints = new(i);
            puteach<int>(ints);


            //迭代器声明见class chapter19 后面
            WriteLine("\n迭代器1");

            yield1 ints1 = new(1, 2, 3, 4);
            foreach (int asd in ints1)
            {
                Write($"{asd}  ");

            }
            WriteLine("\n迭代器2：");
            yield2 ints2 = new(2, 3, 4, 5, 6);
            WriteLine("用类迭代：");
            foreach (int asd in ints2)
            {
                Write(asd + " ");

            }
            WriteLine("\n用成员函数迭代：");
            foreach (int asd in ints2.enumerator())
            {
                Write(asd + " ");

            }




        }
    }

    class c19_father
    {

        protected int[] d;

        public c19_father(params int[] ints)
        {

            d = new int[ints.Length];
            for (int i = 0; i < ints.Length; i++)
            {
                d[i] = ints[i];
                //WriteLine(d[i]);
            }
        }

    }

    //常用迭代器1,此迭代器只能用类迭代
    class yield1 : c19_father
    {
        public yield1(params int[] ints) : base(ints)
        {

        }

        //返回类型不同
        public IEnumerator<int> enumerator
        {
            get
            {
                for (int i = 0; i < d.Length; i++)
                {
                    yield return d[i];
                }
            }
        }

        //无此函数无法用类直接迭代
        public IEnumerator<int> GetEnumerator()
        {
            return enumerator;
        }

    }


    //常用迭代器2
    /*
    当无GetEnumerator()函数时，只可以用函数迭代 ：
     foreach (int asd in ints2.enumerator()){}


    当有GetEnumerator()函数时，还可以用类的对象迭代：

            yield2 ints2 = new(2, 3, 4, 5, 6);
            foreach (int asd in ints2){}
         

    */
    class yield2 : c19_father
    {
        public yield2(params int[] ints) : base(ints)
        { }

        //返回类型不同
        public IEnumerable<int> enumerator()
        {
            for (int i = 0; i < d.Length; i++)
            {
                yield return d[i];
            }
        }

        //无此函数无法用类直接迭代，需访问上面的函数
        public IEnumerator<int> GetEnumerator()
        {
            return enumerator().GetEnumerator();
        }


    }
}