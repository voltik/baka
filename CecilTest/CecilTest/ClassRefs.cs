using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;

namespace CecilTest
{
    public class ClassRefs
    {
        public TypeDefinition type { get; }
        public List<string> definedMethods { get; }
        public HashSet<string> calledMethods { get; }

        public ClassRefs(TypeDefinition type)
        {
            this.type = type;
            definedMethods = new List<string>();
            calledMethods = new HashSet<string>();
            scan();
        }

        private void scan()
        {
            Console.WriteLine("Type " + type);
            foreach (var method in type.Methods)
            {
                Console.WriteLine("Method " + method);
                definedMethods.Add(method.ToString());
                if (method.HasBody)
                {
                    foreach (var instruction in method.Body.Instructions)
                    {
                        var op = instruction.OpCode;
                        if (op == OpCodes.Call || op == OpCodes.Calli || op == OpCodes.Callvirt || op == OpCodes.Newobj)
                        {
                            var methodCall = instruction.Operand as MethodReference;
                            if (methodCall != null && !filter(methodCall))
                            {
                                calledMethods.Add(methodCall.ToString());
                            }
                        }
                    }
                }
            }
        }

        private static List<string> FILTER = new List<string>() {
            "System.", "Newtonsoft.Json."
        };

        private bool filter(MethodReference m)
        {
            var decl = m.DeclaringType.ToString();
            foreach (var fp in FILTER)
            {
                if (decl.StartsWith(fp))
                {
                    return true;
                }
            }
            return FILTER.Contains(decl);
        }

        public void Print()
        {
            Console.WriteLine("TYPE: " + type.ToString());
            Console.WriteLine("DEFINED:");
            foreach (var m in definedMethods)
            {
                Console.WriteLine("\t" + m);
            }
            Console.WriteLine("CALLED:");
            foreach (var m in calledMethods)
            {
                Console.WriteLine("\t" + m);
            }
        }
    }
}
