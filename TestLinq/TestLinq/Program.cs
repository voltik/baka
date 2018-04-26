using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var o1 = new Obj(1, "k1", "l1", "g1");
            var o2 = new Obj(1, "k1", "l1", "g2");
            var o2b = new Obj(1, "k1", "l1", "g2");
            var o3 = new Obj(2, "k2", "l2", "g3");
            var o4 = new Obj(4, "k1", "l1", "g1");
            var arr = new Obj[] { o1, o2, o3, o4, o2b };

            var r1 = arr.GroupBy(k => new { k.hour, k.kind });
            foreach (var r in r1)
            {
                Console.WriteLine("key=" + r.Key);
            }

            var r2 = arr.GroupBy(k => new { k.hour, k.kind }, (k,g)=>new Grouped(k.hour,k.kind,g));
            foreach (var r in r2)
            {
                Console.WriteLine("key=" + r.hour+":"+r.kind);
                foreach(var g in r.grps)
                {
                    Console.WriteLine(" > " + g);
                }
            }

        }
    }

    public class Obj
    {
        public int hour;
        public string kind;
        public string link;
        public string grp;

        public Obj(int h, string k, string l, string g)
        {
            hour = h;
            kind = k;
            link = l;
            grp = g;
        }
    }

    public class Grouped
    {
        public int hour;
        public string kind;
        public List<string> grps = new List<string>();

        public Grouped(int h, string k, IEnumerable<Obj> objs)
        {
            hour = h;
            kind = k;
            grps = objs.Select(x=>x.grp).Distinct().ToList();
        }
    }

}
