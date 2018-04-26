using System;

namespace CodeUtils
{
    internal static class XmlRefactoring
    {
        internal static String processXmlNodes(String[] lines)
        {
            String res = "";
            int i = -1;
            while (true)
            {
                i++;
                if (i >= lines.Length)
                {
                    break;
                }
                if (i + 2 < lines.Length && lines[i].Contains("= xdoc.CreateElement(\"") &&
                    lines[i + 1].Contains(".InnerText =") && lines[i + 2].Contains(".AppendChild("))
                {
                    String tagName = Utils.inQuotes(Utils.after(lines[i], ".CreateElement("));
                    String inner = Utils.after(lines[i + 1], ".InnerText =");
                    String rootElem = Utils.before(lines[i + 2], ".AppendChild(");
                    res += "XmlElemWithInner(xdoc, " + rootElem.Trim() + ", " + tagName.Trim() + ", " + inner.Trim() + ")\r\n";
                    i += 2;
                }
                else if (i + 1 < lines.Length && lines[i].Contains("= xdoc.CreateElement(\"") &&
                     lines[i + 1].Contains(".AppendChild("))
                {
                    String tagName = Utils.inQuotes(Utils.after(lines[i], ".CreateElement("));
                    String varName = Utils.before(lines[i], "=");
                    String rootElem = Utils.before(lines[i + 1], ".AppendChild(");
                    res += varName + " = XmlElem(xdoc, " + rootElem.Trim() + ", " + tagName.Trim() + ")\r\n";
                    i += 1;
                }
                else
                {
                    res += lines[i] + "\r\n";
                }
            }
            return res;
        }
    }
}
