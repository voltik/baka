using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CodeAnalysis
{
    class Program
    {
        private static List<String> exclude = new List<String>(new String[]
        {
            "Void Clear()",
            "Int32 CompareTo(System.Object)",
            "Boolean Equals(System.Object)",
            "Void Finalize()",
            "Int32 get_Capacity()",
            "Int32 get_Count()",
            "Enumerator GetEnumerator()",
            "Int32 GetHashCode()",
            "System.Type GetType()",
            "Boolean HasFlag(System.Enum)",
            "System.Object MemberwiseClone()",
            "Void set_Capacity(Int32)",
            "Void RemoveAt(Int32)",
            "Void RemoveRange(Int32, Int32)",
            "Void Reverse()",
            "Void Reverse(Int32, Int32)",
            "Void Sort()",
            "System.String ToString()",
            "Void TrimExcess()",
            "Void System.Collections.ICollection.CopyTo(System.Array, Int32)",
            "System.Object System.Collections.IList.get_Item(Int32)",
            "System.Object System.Collections.ICollection.get_SyncRoot()",
            "Void System.Collections.IList.Insert(Int32, System.Object)",
            "Void System.Collections.IList.Remove(System.Object)",
            "Void System.Collections.IList.set_Item(Int32, System.Object)",
            "UInt64 System.IConvertible.ToUInt64(System.IFormatProvider)",
            "UInt32 System.IConvertible.ToUInt32(System.IFormatProvider)",
            "UInt16 System.IConvertible.ToUInt16(System.IFormatProvider)",
            "System.Object System.IConvertible.ToType(System.Type, System.IFormatProvider)",
            "System.Decimal System.IConvertible.ToDecimal(System.IFormatProvider)",
            "System.DateTime System.IConvertible.ToDateTime(System.IFormatProvider)",
            "System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()",
            "Single System.IConvertible.ToSingle(System.IFormatProvider)",
            "SByte System.IConvertible.ToSByte(System.IFormatProvider)",
            "Int64 System.IConvertible.ToInt64(System.IFormatProvider)",
            "Int32 System.IConvertible.ToInt32(System.IFormatProvider)",
            "Int32 System.Collections.IList.IndexOf(System.Object)",
            "Int32 System.Collections.IList.Add(System.Object)",
            "Int16 System.IConvertible.ToInt16(System.IFormatProvider)",
            "Char System.IConvertible.ToChar(System.IFormatProvider)",
            "Double System.IConvertible.ToDouble(System.IFormatProvider) ",
            "Byte System.IConvertible.ToByte(System.IFormatProvider)",
            "Boolean System.IConvertible.ToBoolean(System.IFormatProvider)",
            "Boolean System.Collections.IList.get_IsReadOnly()",
            "Boolean System.Collections.IList.get_IsFixedSize()",
            "Boolean System.Collections.IList.Contains(System.Object)",
            "Boolean System.Collections.ICollection.get_IsSynchronized()",
            "Boolean System.Collections.Generic.ICollection<T>.get_IsReadOnly()",
            
        });

        static void Main(string[] args)
        {
            StreamWriter w = new StreamWriter("ca-out.txt");
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);
            foreach (String dllName in args)
            {
                Assembly dll = Assembly.LoadFile(dllName);
                foreach (Type type in dll.GetExportedTypes())
                {
                    MethodInfo[] props = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
                    foreach (MethodInfo inf in props)
                    {
                        //Console.WriteLine(inf + " | " + type.FullName);
                        //if (!exclude.Contains(inf.ToString()))
                        {
                            w.WriteLine(inf + " | " + type.FullName);
                        }
                    }
                }
                w.WriteLine();
            }
            w.Close();
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");
            if (!File.Exists(assemblyPath)) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }

    }
}
