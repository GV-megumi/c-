using System;
using static System.Console;
using System.IO;
using chapter;


class chapter13 : Chapter
{

           //10,11,12
        public override void f()
        {
            int i,j;



            WriteLine("----------数组---------------");

            int[] array={1,2,3,4,5,};
            //数组是引用类型
            array=new int[]{6,7,8,9,10,11};

            WriteLine($"{array.Rank}维数组array长度为{array.Length}");

            for(i=0;i<array.Length;i++)
            WriteLine($"array[{i}]={array[i]}");
            WriteLine();
           




            WriteLine("----------交错数组---------------");


            //声明
            int[][] array2=new int[2][];
            array2[0]=new int[]{1,2,5,4,3,};
            array2[1]=new int[10];
            WriteLine($"array2:");

            //排序函数
            Array.Sort(array2[0]);


            for(i=0;i<array2.GetLength(0);i++)//也可写成   i<array2.Length
            {
                Write($"第{i+1}行：  ");
                for(j=0;j<array2[i].GetLength(0);j++)//此处也可写成  j<array2[i].Length
                {
                    Write(array2[i][j]+",");
                }
                WriteLine();
            }


            //---三维---
            int[][][] array3=new int[2][][];
            //int[][]  array4=new int[2][2];  Error

            WriteLine("-----------foreach语句--------------");

           i=0;

            foreach(int[] a in array2)
            {
                Write($"第{i}行：  ");

                foreach(int aa in a)
                {
                    Write(aa+",");

                }
                i++;
                WriteLine();


            }


            //数组协变（多态）
            Chapter[] num=new Chapter[4];
            num[0]=new chapter1_6();
            num[1]=new chapter9(); 
            num[2]=new chapter10_12();




            WriteLine("---------Sort()与BinarySearch()---------");

            int[] array5={12,14,2,6,5,4,9,32,45,13,67};
            WriteLine($"数组：");
            PrintArray(array5);
            WriteLine("Sort()排序后：");
            Array.Sort(array5);
            PrintArray(array5);
           
            WriteLine($"14在array[{Array.BinarySearch(array5,14)}]");
            //数值不存在返回负数，取反后-1  为 该（不存在的）数值应该存在的位置
            WriteLine($"15应该在array[{-Array.BinarySearch(array5,15)-1}]");
            










        }

        void PrintArray(int[] a){
           
            foreach(int aa in a)
            {
                Write(aa+",");
            }
            WriteLine();
        }


}