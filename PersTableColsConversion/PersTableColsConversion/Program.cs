using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PersTableColsConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = new GdprTables() { Records = new List<GdprTableField>() };
            string path = @"c:\Users\Pribram01\Documents\Visual Studio 2015\Projects\PersTableColsConversion\PersTableColsConversion\";
            process(res, path + "zaci.txt", "zaci");
            process(res, path + "ucitele.txt", "ucitele");
            string s = JsonConvert.SerializeObject(res);
            File.WriteAllText(path + "output.txt", s, Encoding.UTF8);
            Console.WriteLine(s);
            Console.ReadKey();
        }

        private static void process(GdprTables res, string fn, string table)
        {
            string[] lns = File.ReadAllLines(fn);
            for (int i = 1; i < lns.Length; i++)
            {
                string[] x = lns[i].Split('\t');
                var rec = new GdprTableField()
                {
                    Table = table,
                    Column = x[0].Trim(),
                    CollectionPermision = typ(x[1].Trim()),
                    PermisionDescription = x[2].Trim(),
                    Description = x[3].Trim()
                };
                res.Records.Add(rec);
            }
        }

        private static int typ(string str)
        {
            switch (str)
            {
                case "ŠZ":
                    return 2;
                case "OZ":
                    return 3;
                case "S":
                    return 4;
                case "ND":
                    return 0;
                case "N":
                    return 1;
                default:
                    return 0;
            }
        }

        //ŠZ - školský zákon
        //OZ - oprávněný zájem
        //S - souhlas
        //ND - nelze definovat
        //N - není potřeba

        ///// <summary>
        ///// Nedefinováno
        ///// </summary>
        //NotDefined = 0,
        ///// <summary>
        ///// Není potřeba (neobsahuje OÚ)
        ///// </summary>
        //NotNeeded = 1,
        ///// <summary>
        ///// Školský zákon
        ///// </summary>
        //EducationAct = 2,
        ///// <summary>
        ///// Oprávněný zájem
        ///// </summary>
        //LegitimateInterest = 3,
        ///// <summary>
        ///// Souhlas
        ///// </summary>
        //Consent = 4,


    }
}
