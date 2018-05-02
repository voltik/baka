using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CecilTest
{
    class Program
    {
        private static List<string> AspxMethods = new List<string>()
        {
            "::Page_Load(System.Object,System.EventArgs)", "::Page_Init(System.Object,System.EventArgs)"
        };

        static void Main(string[] args)
        {
            var cs = new Classes();
            int a0 = int.Parse(args[0]);
            bool ignoreGettersAndSetters = (a0 & 1) != 0;
            bool ignoreAspxMethods = (a0 & 2) != 0;

            string a1 = args[1];
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

            var res = new StringBuilder();
            foreach (var m in cs.FindNotUsed())
            {
                if (ignoreGettersAndSetters && (m.IsGetter || m.IsSetter))
                {
                    continue;
                }
                if (ignoreAspxMethods && isAspxMethod(m.Signature))
                {
                    continue;
                }
                res.Append(m).Append("\n");
                Console.WriteLine(m);
            }
            File.WriteAllText("notUsed.txt", res.ToString());
        }

        private static bool isAspxMethod(string signature)
        {
            foreach (var suff in AspxMethods)
            {
                if (signature.EndsWith(suff))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

// https://stackoverflow.com/questions/4073828/how-to-determine-which-methods-are-called-in-a-method
