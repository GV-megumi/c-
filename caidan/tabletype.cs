using System;
using static System.Console;
using System.Text.RegularExpressions;


namespace caidan
{

    class TableType
    {




        //输出表格
        public int putTable(string[,] aaa, int[] strlens)
        {

            if (aaa == null)
                return 1;


            int i = 0, j = 1;
            int spacenum;//空格数
            int number = 0; //记录str里的数字/字母数

            int num = aaa.GetLength(1);

            Write("行号    ");
            foreach (string s in aaa)
            {
                if (s == null)
                    break;


                if (i == num)
                {
                    i = 0;
                    WriteLine();
                    Write($"{j:D3}     ");
                    j++;
                }
                Write(s);

                number = putspace(s);
                while (number != 0)
                {
                    Write(" ");
                    number--;
                }





                spacenum = strlens[i] - s.Length + 4;
                //Write(spacenum);
                while (spacenum != 0)
                {
                    spacenum--;
                    Write("  ");
                }
                i++;


            }

            WriteLine("\n\n");

            return 0 - j + 1;


        }





        //返回子符串中的数字和字母以及标点符号的数量（不包含中文字符）
        public int putspace(string inputString)
        {



            // 使用正则表达式匹配包含字母、数字和标点符号的子串，排除中文字符
            string pattern = @"[a-zA-Z0-9\p{P}]+";
            MatchCollection matches = Regex.Matches(inputString, pattern);

            int letterDigitAndPunctuationCount = 0;

            foreach (Match match in matches)
            {
                letterDigitAndPunctuationCount += match.Value.Length;
            }
            return letterDigitAndPunctuationCount;
        }




        public void pause()
        {
            WriteLine("\n按任意键继续");
            ReadKey();
        }
    }
}