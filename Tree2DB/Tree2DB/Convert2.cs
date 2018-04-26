using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tree2DB
{
    class Convert2
    {
        private Node root = new Node() { Id = "root" };
        private Stack<Node> path = new Stack<Node>();

        internal void Main(string fn)
        {
            var ids = new HashSet<string>();
            path.Push(root);
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
                    var n = new Node() { Id = id, Name = k0, Descr = descr, Prereq = prer, Related = related };
                    addNode(1, n);
                    continue;
                }
                if (k1.Length > 0)
                {
                    var n = new Node() { Id = id, Name = k1, Descr = descr, Prereq = prer, Related = related };
                    addNode(2, n);
                    continue;
                }
                if (k2.Length > 0)
                {
                    var n = new Node() { Id = id, Name = k2, Descr = descr, Prereq = prer, Related = related };
                    addNode(3, n);
                    continue;
                }
                if (k3.Length > 0)
                {
                    var n = new Node() { Id = id, Name = k3, Descr = descr, Prereq = prer, Related = related };
                    addNode(4, n);
                    continue;
                }
            }
            string s = JsonConvert.SerializeObject(root);
            File.WriteAllText("output.txt", s, Encoding.UTF8);
        }

        private void addNode(int onLevel, Node n)
        {
            clearPath(onLevel);
            var par = path.Peek();
            par.Subnodes.Add(n);
            path.Push(n);
        }

        private void clearPath(int level)
        {
            while (path.Count > level)
            {
                path.Pop();
            }
        }
    }

    internal class Node
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public string Prereq { get; set; }
        public string Related { get; set; }
        public List<Node> Subnodes { get; set; }

        internal Node()
        {
            Subnodes = new List<Node>();
        }
    }
}
