using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace ToDoListConversion
{
    class Program
    {
        private const string GUID_PREFIX = "42414B41-0001-0001-"; // BAKA + ver + ver
        private const string LIST_GUID_PREFIX = GUID_PREFIX + "1111-"; // todo list
        private static ToDoList list = new ToDoList() { Name = "Kontrolní seznam GDPR", Guid = createGuid("0000") };

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"c:\petr\GDPR\to-do-checks.txt");

            checkHeader(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                Console.WriteLine(i);
                convert(lines[i], i);
            }

            string outp = JsonConvert.SerializeObject(list);
            File.WriteAllText(@"c:\petr\GDPR\to-do-checks-json.txt", outp, Encoding.UTF8);
        }

        private static string convert(string s, int lnNo)
        {
            string[] x = s.Split('\t');
            string id = x[0];
            string num = x[1];
            if (num.Length > 0)
            {
                string secName = x[2];
                string chkName = x[3];
                string action = x[5];
                string secDesc = x[7];
                if (secName.Length > 0)
                {
                    var sec = new ToDoSection() { Name = secName, Number = num, Priority = lnNo, Guid = createGuid(id) };
                    list.SectionSet.Add(sec);
                }
                else
                {
                    var chk = new ToDoCheck() { Name = chkName, Number = num, Priority = lnNo, Action = action, Description = secDesc, IsObsolete = false, Guid = createGuid(id) };
                    var sec = list.SectionSet[list.SectionSet.Count - 1];
                    sec.CheckSet.Add(chk);
                }
            }
            return null;
        }

        private static Guid createGuid(string id)
        {
            foreach (char z in id)
            {
                if (z < '0' || z > '9') throw new Exception("Incorrect line ID: " + id);
            }
            string part = id;
            while (part.Length < 12) part = "0" + part;
            return new Guid(LIST_GUID_PREFIX + part);
        }

        private static void checkHeader(string s)
        {
            string[] x = s.Split('\t');
            match(x, 0, "ID");
            match(x, 1, "Check.Number");
            match(x, 2, "Section.Name");
            match(x, 3, "Check.Name");
            match(x, 4, "");
            match(x, 5, "Check.Action");
            match(x, 6, "");
            match(x, 7, "Check.Description");
        }

        private static void match(string[] cells, int cell, string val)
        {
            if (cells[cell] != val) throw new Exception("Header does not match: Cell " + cell + ", expected: " + val + ", found: " + cells[cell]);
        }
    }
}
