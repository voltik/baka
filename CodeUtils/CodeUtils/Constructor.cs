using System;
using System.Collections.Generic;

namespace CodeUtils
{
    internal static class Constructor
    {
        internal static String processConstructor(String[] lines)
        {
            List<Argument> args = new List<Argument>();
            foreach (String line in lines)
            {
                String l2 = line.Replace("public ", "").Replace("private ", "").Replace("protected ", "").Replace("internal ", "");
                l2 = l2.Replace(" { get; }", "").Replace(" { set; }", "").Replace(" { get; set; }", "");
                l2 = l2.Trim();
                if (l2.Length > 0)
                {
                    args.Add(new Argument(l2));
                }
            }
            String res = "public X(";
            bool first = true;
            foreach (Argument a in args)
            {
                if (!first)
                {
                    res += ",";
                }
                res += a.Type + " " + a.NameLC;
                first = false;
            }
            res += ") {\r\n";
            foreach (Argument a in args)
            {
                res += " " + a.NameUC + "=" + a.NameLC + ";\r\n";
            }
            res += "}\r\n";
            return res;
        }
    }

    internal class Argument
    {
        internal String Type;
        internal String NameUC;
        internal String NameLC;

        internal Argument(String def)
        {
            String[] x = def.Split(' ');
            Type = x[0].Trim();
            NameUC = x[1].Trim();
            NameLC = x[1].Substring(0, 1).ToLower() + x[1].Substring(1);
        }
    }
}
