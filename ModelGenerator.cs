using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class ModelGenerator
    {
        public string NameSpace { get; set; }
        public string ConStr { get; set; }
        public string FolderPath { get; set; }
        public string TableName { get; set; }
        public string ProcessedTableName { get; set; }

        public void GenerateModel2010()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + NameSpace + ".Model");
            sb.AppendLine("{");

            sb.AppendLine("public partial class " + ProcessedTableName);
            sb.AppendLine("{");
            string cmdText = "select * from INFORMATION_SCHEMA.columns where table_name = @name";
            DataTable dt = SQLHelper.ExecuteDataTable(ConStr, cmdText, new SqlParameter("name", TableName));
            foreach (DataRow dr in dt.Rows)
            {
                string type = Helper.ChangeSQLTypeToCSharpType
                    (dr["data_type"].ToString(), dr["is_nullable"].ToString() == "YES" ? true : false);
                sb.AppendLine("public " + type + " " + dr["column_name"].ToString() + "{get;set;}");
            }
            sb.AppendLine("}");

            sb.AppendLine("}");

            WriteModelToFile(sb);
        }

        private void WriteModelToFile(StringBuilder sb)
        {
            string dir = Path.Combine(FolderPath, "Model");
            Directory.CreateDirectory(dir);//如果没有路径，就创建路径(创建model文件夹)
            string path = Path.Combine(dir, ProcessedTableName + ".cs");
            File.WriteAllText(path, sb.ToString());
        }
    }
}