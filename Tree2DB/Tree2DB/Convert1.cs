using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tree2DB
{
    class Convert1
    {
        private List<string> output = new List<string>(); // ID,parentID,name,desc,prer,releated
        private Stack<string> path = new Stack<string>();

        internal void Main(string fn)
        {
            var ids = new HashSet<string>();
            foreach (string ln in File.ReadAllLines(fn))
            {
                string[] x = ln.Split('\t');
                if (x.Length < 8)
                {
                    Console.WriteLine("Line too short: " + ln);
                    continue; // return;
                }
                string k0 = x[0].Trim();
                string k1 = x[1].Trim();
                string k2 = x[2].Trim();
                string k3 = x[3].Trim();
                string descr = x[4].Trim();
                string id = x[5].Trim();
                string prer = x[6].Trim();
                string related = x[7].Trim();

                if (ids.Contains(id))
                {
                    Console.WriteLine("ID duplicity: " + id);
                    //return;
                }
                ids.Add(id);

                if (k0.Length > 0)
                {
                    clearPath(0);
                    wr(id, "*", k0, descr, prer, related);
                    path.Push(id);
                    continue;
                }
                if (k1.Length > 0)
                {
                    clearPath(1);
                    wr(id, path.Peek(), k1, descr, prer, related);
                    path.Push(id);
                    continue;
                }
                if (k2.Length > 0)
                {
                    clearPath(2);
                    wr(id, path.Peek(), k2, descr, prer, related);
                    path.Push(id);
                    continue;
                }
                if (k3.Length > 0)
                {
                    clearPath(3);
                    wr(id, path.Peek(), k3, descr, prer, related);
                    path.Push(id);
                    continue;
                }
            }
            File.WriteAllLines("output.txt", output, Encoding.UTF8);
        }

        private void clearPath(int level)
        {
            while (path.Count > level)
            {
                path.Pop();
            }
        }

        private void wr(string id, string parent, string name, string descr, string prer, string related)
        {
            output.Add("insert into tree values('" + string.Join("','", id, parent, name, descr, prer, related) + "');");
        }

    }
}
