using Mono.Cecil;
using System;
using System.IO;
using System.Text;

namespace CecilTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var cs = new Classes();
            string a1 = args[0];
            if (a1.StartsWith("@"))
            {
                var lns = File.ReadAllLines(a1.Substring(1));
                foreach (var ln in lns)
                {
                    cs.ReadModule(ln);
                }
            }
            else
            {
                cs.ReadModule(a1);
            }

            cs.DumpData("data.txt");
            Console.WriteLine();
            Console.WriteLine("---");

            var nu = cs.FindNotUsed();
            var res = new StringBuilder();
            foreach (var m in nu)
            {
                res.Append(m).Append("\n");
                Console.WriteLine(m);
            }
            File.WriteAllText("notUsed.txt", res.ToString());
        }
    }
}

// https://stackoverflow.com/questions/4073828/how-to-determine-which-methods-are-called-in-a-method
