using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TriggerGener
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = args[0];
            string table = args[1];
            string pk = args[2];
            int opts = int.Parse(args[3]);
            var me = new Program();
            me.run(connStr, table, pk, opts);
            Console.ReadKey();
        }

        private void run(string connStr, string table, string pk, int opts)
        {
            var cols = new List<ColDef>();
            if ((opts & 2) != 0)
            {
                var conn = new SqlConnection(connStr);
                conn.Open();
                var cmd = new SqlCommand("SELECT COLUMN_NAME,data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@1", conn);
                cmd.Parameters.AddWithValue("@1", table);
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var col = new ColDef() { name = r.GetString(0), type = r.GetString(1) };
                        cols.Add(col);
                    }
                }
            }

            wr("CREATE TRIGGER " + table + "Trigger ON " + table + " AFTER INSERT, UPDATE");
            wr("AS");
            wr("BEGIN");
            if ((opts & 1) != 0)
            {
                wr("  UPDATE " + table);
                wr("  SET ModifBy=cast(CONTEXT_INFO() as varchar(20)), ModifDate=GETDATE()");
                wr("  FROM " + table + " x");
                wr("  JOIN inserted i ON x." + pk + "=i." + pk);
            }
            wr("");
            if ((opts & 2) != 0)
            {
                string colVals = columnVals(cols);
                wr("  declare @t0 nvarchar(MAX) = (select top 1 " + pk + " from inserted);");
                wr("  declare @t1 nvarchar(MAX) = (select top 1 " + colVals + " from deleted);");
                wr("  declare @t2 nvarchar(MAX) = (select top 1 " + colVals + " from inserted);");
                wr("  insert into PersInfoLog (userId,tableName,identity_,oldValue,newValue,dateAndTime) values (cast(CONTEXT_INFO() as varchar(20)),'" + table + "',@t0,@t1,@t2,GETDATE());");
            }
            wr("END");
        }

        private string columnVals(List<ColDef> cols)
        {
            string res = "";
            foreach (var c in cols)
            {
                var t = c.type.ToUpper();
                if (t == "TEXT" || t == "NTEXT" || t == "IMAGE")
                {
                    continue;
                }
                res += (res.Length > 0 ? "+'," : "'{") + "''" + c.name + "''='''+";
                if (t != "NVARCHAR" && t != "VARCHAR")
                {
                    res += "ISNULL(cast(" + c.name + " as nvarchar(MAX)),'NULL')";
                }
                else
                {
                    res += "ISNULL(" + c.name + ",'NULL')";
                }
                res += "+''''+char(13)+char(10)";
            }
            return res + "+'}'";
        }

        private void wr(string s)
        {
            Console.WriteLine(s);
        }
    }

    internal class ColDef
    {
        internal string name;
        internal string type;
    }
}
