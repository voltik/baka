using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeUtilsCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //removeDoubleEmptyLines(args[0], args[1], args[2]);
            //removeExtraEmptyLines(args[0], args[1], args[2]); -- ver2 is more generic
            //findIfThenElse(args[0], args[1]);
            //removeExtraEmptyLines2(args[0], args[1], args[2]);
            replaceInStr(args[0], args[1]);
            //replaceInStr("", "");
        }

        private static void removeDoubleEmptyLines(String srcDir, String destDir, String mask)
        {
            foreach (String file in Directory.EnumerateFiles(srcDir, mask))
            {
                Console.WriteLine("<= " + file);
                List<String> res = new List<String>();
                String[] lines = File.ReadAllLines(file);
                if (lines[0].Trim().Length != 0)
                {
                    res.Add(lines[0]);
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i].Trim().Length == 0 && lines[i - 1].Trim().Length == 0)
                    {
                    }
                    else
                    {
                        res.Add(lines[i]);
                    }
                }
                String newFile = Path.Combine(destDir, Path.GetFileName(file));
                Console.WriteLine("=> " + newFile);
                File.WriteAllLines(newFile, res.ToArray(), Encoding.UTF8);
            }
        }

        private static void removeExtraEmptyLines(String srcDir, String destDir, String mask)
        {
            foreach (String file in Directory.EnumerateFiles(srcDir, mask))
            {
                Console.WriteLine("<= " + file);
                List<String> res = new List<String>();
                String[] lines = File.ReadAllLines(file);
                if (lines[0].Trim().Length != 0)
                {
                    res.Add(lines[0]);
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    bool skip = mask == "*.cs" && lines[i - 1].Trim() == "}" && i + 1 < lines.Length && lines[i + 1].Trim() == "}" && lines[i].Trim().Length == 0;
                    skip |= mask == "*.vb" && lines[i - 1].Trim() == "End If" && i + 1 < lines.Length && lines[i + 1].Trim() == "End If" && lines[i].Trim().Length == 0;
                    if (skip)
                    {
                    }
                    else
                    {
                        res.Add(lines[i]);
                    }
                }
                String newFile = Path.Combine(destDir, Path.GetFileName(file));
                Console.WriteLine("=> " + newFile);
                File.WriteAllLines(newFile, res.ToArray(), Encoding.UTF8);
            }
        }

        private static void findIfThenElse(String srcDir, String mask)
        {
            foreach (String file in Directory.EnumerateFiles(srcDir, mask))
            {
                Console.WriteLine("<= " + file);
                String[] lines = File.ReadAllLines(file);
                for (int i = 1; i < lines.Length; i++)
                {
                    //bool skip = mask == "*.cs" && lines[i].Trim() == "}" && i + 1 < lines.Length && lines[i + 1].Trim() == "}" && lines[i].Trim().Length == 0;
                    bool skip = mask == "*.vb" && lines[i].Trim() == "End If" && lines[i - 1].Trim().StartsWith("Return ")
                        && lines[i - 2].Trim() == "Else" && lines[i - 3].Trim().StartsWith("Return ") &&
                        lines[i - 4].Trim().StartsWith("If ");
                    if (skip)
                    {
                        Console.WriteLine("!!! " + i);
                    }
                }
            }
        }

        private static void removeExtraEmptyLines2(String srcDir, String destDir, String mask)
        {
            foreach (String file in Directory.EnumerateFiles(srcDir, mask))
            {
                Console.WriteLine("<= " + file);
                List<String> res = new List<String>();
                String[] lines = File.ReadAllLines(file);
                if (lines[0].Trim().Length != 0)
                {
                    res.Add(lines[0]);
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    bool skip = mask == "*.cs" && i + 1 < lines.Length && lines[i + 1].Trim() == "}" && lines[i].Trim().Length == 0;
                    skip |= mask == "*.vb" && i + 1 < lines.Length && (lines[i + 1].Trim().StartsWith("End") || lines[i + 1].Trim().StartsWith("Catch")) && lines[i].Trim().Length == 0;
                    if (skip)
                    {
                    }
                    else
                    {
                        res.Add(lines[i]);
                    }
                }
                String newFile = Path.Combine(destDir, Path.GetFileName(file));
                Console.WriteLine("=> " + newFile);
                File.WriteAllLines(newFile, res.ToArray(), Encoding.UTF8);
            }
        }

        // If (InStr(FiltrNaZpravy, "@sysall") > 0) Then ' od systemu chci
        //String test = "If (InStr(FiltrNaZpravy, \"@sysall\") > 0) Then ' od systemu chci";
        //String res = rx.Replace(test, "($1.Contains($2))");
        private static void replaceInStr(String srcDir, String destDir)
        {
            String RX = "\\(InStr\\((\\w+),\\s*(.*?)\\)\\s*>\\s*0\\)";
            Regex rx = new Regex(RX);
            foreach (String file in Directory.EnumerateFiles(srcDir, "*.vb"))
            {
                Console.WriteLine("<= " + file);
                List<String> res = new List<String>();
                String[] lines = File.ReadAllLines(file);
                foreach (var ln in lines)
                {
                    String lnt = ln.Trim();
                    String ln2 = ln;
                    if (lnt.StartsWith("If "))
                    {
                        ln2 = rx.Replace(ln, "($1.Contains($2))");
                    }
                    res.Add(ln2);
                }
                String newFile = Path.Combine(destDir, Path.GetFileName(file));
                Console.WriteLine("=> " + newFile);
                File.WriteAllLines(newFile, res.ToArray(), Encoding.UTF8);
            }
        }


    }
}
