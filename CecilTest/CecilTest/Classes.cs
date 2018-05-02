using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CecilTest
{
    public class Classes
    {
        public List<ClassRefs> classes { get; }

        public Classes()
        {
            classes = new List<ClassRefs>();
        }

        public void ReadModule(string moduleName)
        {
            var module = ModuleDefinition.ReadModule(moduleName);
            foreach (var type in module.Types)
            {
                var cr = new ClassRefs(type);
                classes.Add(cr);
            }
        }

        internal void DumpData(string fn)
        {
            var res = new StringBuilder();
            foreach (var c in classes)
            {
                res.Append("CLASS ").Append(c.type).Append("\n");
                res.Append("DEFINED:\n");
                foreach (var m in c.definedMethods)
                {
                    res.Append("\t").Append(m).Append("\n");
                }
                res.Append("CALLED:\n");
                foreach (var m in c.calledMethods)
                {
                    res.Append("\t").Append(m.Value).Append("\n");
                }
            }
            res.Append("END\n");
            File.WriteAllText(fn, res.ToString());
        }

        public List<MethodDesc> FindNotUsed()
        {
            var res = new List<MethodDesc>();
            foreach (var cl in classes)
            {
                foreach (var m in cl.definedMethods)
                {
                    if (!isCalled(m))
                    {
                        res.Add(m);
                    }
                }
            }
            return res;
        }

        private bool isCalled(MethodDesc method)
        {
            foreach (var cl in classes)
            {
                foreach (var m in cl.calledMethods.Keys)
                {
                    if (m == method.Signature)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
