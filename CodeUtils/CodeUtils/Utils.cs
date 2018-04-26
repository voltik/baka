using System;

namespace CodeUtils
{
    internal static class Utils
    {
        internal static String after(String str, String what)
        {
            int pos = str.IndexOf(what);
            if (pos < 0)
            {
                throw new Exception("after failed");
            }
            return str.Substring(pos + what.Length);
        }

        internal static String before(String str, String what)
        {
            int pos = str.IndexOf(what);
            if (pos < 0)
            {
                throw new Exception("before failed");
            }
            return str.Substring(0, pos);
        }

        internal static String inQuotes(String str)
        {
            if (!str.StartsWith("\""))
            {
                throw new Exception("inQuotes failed: 1");
            }
            int pos = str.IndexOf('"', 1);
            if (pos < 0)
            {
                throw new Exception("inQuotes failed: 2");
            }
            return str.Substring(0, pos + 1);
        }
    }
}
