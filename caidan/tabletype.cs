using System;
using static System.Console;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace caidan
{

    class TableType
    {


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