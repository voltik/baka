using System;

namespace CodeUtils
{
    internal static class Assignments
    {
        internal static String processIfThenElse(String[] lines)
        {
            if (lines.Length >= 5 && lines[0].Trim().StartsWith("If") && lines[0].Trim().EndsWith("Then") &&
                lines[2].Trim() == "Else" && lines[4].Trim() == "End If")
            {
                String var = Utils.before(lines[1], "=").Trim();
                if (lines[3].Trim().StartsWith(var))
                {
                    String cond = Utils.before(Utils.after(lines[0], "If"), "Then").Trim();
                    return var + " = IIf(" + cond + ", " + Utils.after(lines[1], "=").Trim() + ", " + Utils.after(lines[3], "=").Trim() + ")\r\n";
                }
            }
            return "";
        }
    }
}
