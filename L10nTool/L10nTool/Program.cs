using System;
using System.IO;
using System.Text;

namespace L10nTool
{
    class Program
    {
        private static int cnt = 0;

        static void Main(string[] args)
        {
            Program me = new Program();
            me.run(args[0]);
            Console.WriteLine("Found " + cnt + " items");
        }

        private void run(String dir)
        {
            Console.Error.WriteLine("Searching directory " + dir);
            DirectoryInfo d = new DirectoryInfo(dir);
            foreach (FileInfo fi in d.EnumerateFiles())
            {
                if (fi.Extension.ToUpper() == ".CS")
                {
                    if (!fi.Name.ToUpper().Contains(".DESIGNER."))
                    {
                        parseFile(fi.FullName);
                    }
                }
            }
            foreach (DirectoryInfo di in d.EnumerateDirectories())
            {
                run(di.FullName);
            }
        }

        private void parseFile(String fn)
        {
            Console.WriteLine("Parsing file " + fn);
            int cnt = 0;
            using (StreamReader file = new StreamReader(fn, Encoding.UTF8))
            {
                while (true)
                {
                    String line = file.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    parseLine(line, ++cnt);
                }
            }
        }

        private void parseLine(String line, int no)
        {
            var ln = line.Trim();
            if (ln.StartsWith("//") || ln.StartsWith("[assembly: ") || ln.StartsWith("[Obsolete(") || ln.StartsWith("[XmlElement(") || ln.StartsWith("throw new "))
            {
                return;
            }

            int idx = 0;
            while (true)
            {
                int i = line.IndexOf('"', idx);
                if (i < 0)
                {
                    break;
                }
                int j = line.IndexOf('"', i + 1);
                if (j < 0)
                {
                    Console.WriteLine("> PAIR NOT MATCHED (" + no + "," + i + "): " + line);
                    break;
                }
                if (i + 1 != j && !sustpectedString(line, i + 1, j) && !line.Contains("// NOL10N"))
                {
                    Console.WriteLine("> LOCALIZE? (" + no + "," + i + "): " + line);
                    cnt++;
                }
                idx = j + 1;
            }
        }

        private bool sustpectedString(String line, int idx1, int idx2)
        {
            if (line[idx1] == '@')
            {
                return false;
            }
            for (int i = idx1; i < idx2; i++)
            {
                if (line[i] != '_' && (line[i] < 'A' || line[i] > 'Z') && (line[i] < '0' || line[i] > '9'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
