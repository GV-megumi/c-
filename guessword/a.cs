using System;
using static System.Console;

namespace GuessWord
{
    public class Game
    {
        public static void f()
        {




            string filePath = "danci.txt"; // 指定输出文件路径

            if (File.Exists(filePath))
            {
                Console.WriteLine("The file exists.");
                return;
            }

            try
            {
                string[] lines = new string[]
                {
            "profitable","decade","expel","manual","symptom","comprehensive","deliver","soar","trim","curb","concession",
            "intimidate","discipline","literate","accelerat","simplify","ultimate","blessing","agony","weigh","vow","tragedy","tremendous",
            "minimize","generate","acquisition","detect","evolve","elaborate","mechanism","significant","appraise","radiate","consciousness",
            "anticipate","necessarily","strategy","slump","biological","instinctive","evaluate","retrieve","sensible","ridiculous","corporate",
            "disgusted","lust","frustrate","ethics","insight","morality","flourish","faculty","manipulate","reinforce","scandal","motive","constrain",
            "boom","fertile","priority","attribute","corruption","tendency","refrain","well","implement","collision","combat","productivity","headquarter",
            "fascinating","initiate","academic","eventually","orientation","capitalists","exaggeration","essentially","originate","give","Depression",
            "materialism","breed","condition","executive","eliminate","stability","erode","disposable","stem","contradiction","multiply",
            "fulfillment","extravagant","obesity","pursuit","guarantee","necessary","nlation","uneven","misery","conflict","release","asset",
            "elevate","conform","self","rest","exert","record","low","esteem","masculine","adolescent","norm","stereotype","campaign","maturity",
            "trend","denial","prevailing","incidentally","accidentally","exile","conclusive","evacuate","displace","convention","negotiation",
            "conserve","alternative","sacrifice","emission","diversity","convert","recycle","incentive","derive","compromise","cozy","lounge",
            "consultant","permanently","competitive","skeptically","vanish","abuse","meditation","sensational","career","defy","model","exclude",
            "winner","corporate","flexible","cope","deserve","transfer","representative","courtesy","respectable","embarrassed","destine","bankruptcy",
            "expectancy","solidarity","insulate","prospect","lifestyle","integration","reciprocal","inflict","obstacle","allocate","barrier","ignite",
            "preach","contend","compatible","successive","deficit","substantially","transplantation","donate","alien","catastrophe","dynamic","nuisance",
            "eligible","appealing","designate","toxic","enforce","sector","explicitly","release","depiction","length"
                };



                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(lines.Length);
                    for (int i = 0; i < lines.Length; i++)
                        writer.WriteLine(lines[i]);

                }


                //File.WriteAllLines(filePath, lines);

                Console.WriteLine("Array has been written to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }








        }
    }
}