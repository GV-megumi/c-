using System;
using System.Xml.Linq;

namespace chapter
{
    class chapter20_1 : Chapter
    {
        public override void f()
        {
            Console.WriteLine("LINQ to XML:");



            Console.WriteLine("XML基础：");

            XDocument emp1 = new  //创建XML文档
            (
                new XElement
                (
                    "students",     //根元素

                    new XElement
                    (
                        "student",
                        new XElement("Name", "张三  "),
                        new XElement("Sno", "20210101"),
                        new XElement("Phone", "12345")
                    ),

                    new XElement
                    (
                        "student",
                        new XElement("Name", "李四  "),
                        new XElement("Sno", "20210102"),
                        new XElement("Phone", "111111")
                    ),
                    new XElement
                    (
                        "student",
                        new XElement("Name", "王二  "),
                        new XElement("Sno", "20210103"),
                        new XElement("Phone", "22345")
                    )
                )

            );

            emp1.Save("empfile.xml");
            XDocument emp2 = XDocument.Load("empfile.xml");
            Console.WriteLine(emp2);
            Console.Clear();

            //Nodes用法
            IEnumerable<XElement> student = emp2.Nodes().OfType<XElement>();


            //Element Elements 用法
            XElement? students;
            students = emp2?.Element("students");//students 节点
            Console.WriteLine($"输出students节点所有内容：{students?.Value}");


            student = students.Elements();
            string[] studentinfo = { "Name", "Sno", "Phone" };

            void print1()
            {
                foreach (XElement xElement in student)
                {
                    XElement? x;
                    foreach (string s in studentinfo)
                    {
                        x = xElement?.Element(s);
                        if(x!=null)
                        Console.Write(s + ": " + x?.Value + "    ");
                    }
                    Console.WriteLine("");

                }
                Console.WriteLine("\n\n");
            }
            print1();

            //添加

            students.Add
            (
                new XElement
                (
                    "student",
                     new XElement(studentinfo[0],"刘武  "),
                    new XElement(studentinfo[1],"20210112")
                )
            );
            emp2.Save("empfile.xml");
            Console.WriteLine("添加了刘武");
            print1();



            //删除
            foreach(XElement xElement in student)
            {
                /*
                xElement 指向单个student节点
                student 存储了students里的所有student节点的地址
                */
                if(xElement.Element("Name").Value=="王二  ")
                {
                    //xElement.RemoveNodes();//此方法会留一个  <student /> （空元素）
                    xElement.Remove(); //此方法 不会留空
                }
            }
            Console.WriteLine("删除了王二");
            emp2.Save("empfile.xml");
            print1();



        }
    }

}