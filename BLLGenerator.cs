using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class BLLGenerator
    {
        public string NameSpace { get; set; }
        public string ConStr { get; set; }
        public string FolderPath { get; set; }
        public string TableName { get; set; }
        public string ProcessedTableName { get; set; }

        public void GenerateBLL2010()
        {
            StringBuilder sb = new StringBuilder();

            sb = GetBLLStringBuilder("2010");

            WriteBLLToFile(sb);
        }

        private void WriteBLLToFile(StringBuilder sb)
        {
            string dir = Path.Combine(FolderPath, "BLL");
            Directory.CreateDirectory(dir);//如果没有路径，就创建路径(创建model文件夹)
            string path = Path.Combine(dir, ProcessedTableName + "BLL.cs");
            File.WriteAllText(path, sb.ToString());
        }

        private StringBuilder GetBLLStringBuilder(string version)
        {
            string primaryKey = GetPrimaryKey();
            string primaryKeyType = GetPrimaryKeyType();
            string primaryKeyCamel = Helper.ChangeFirstLetterToLowercase(primaryKey);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");

            if (version == "2010")
            {
                sb.AppendLine("using System.Linq;");
            }

            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using " + NameSpace + ".Model;");
            sb.AppendLine("using " + NameSpace + ".DAL;");
            sb.AppendLine("using " + NameSpace + ".Utility;");
            sb.AppendLine("");

            sb.AppendLine("namespace " + NameSpace + ".BLL");
            sb.AppendLine("{");

            sb.AppendLine("public partial class " + ProcessedTableName + "BLL");
            sb.AppendLine("{");

            sb.AppendLine(CreateIsExistent());
            sb.AppendLine(CreateIsExistentByWhere());
            sb.AppendLine(CreateInsert());
            sb.AppendLine(CreateDelete());
            sb.AppendLine(CreateDeleteByWhere());
            sb.AppendLine(CreateDeleteList());
            sb.AppendLine(CreateUpdate());
            sb.AppendLine(CreateGetModel());
            sb.AppendLine(CreateGetModelByWhere());
            sb.AppendLine(CreateGetModelByCache());
            sb.AppendLine(CreateGetModelByWhereAndCache());
            sb.AppendLine(CreateGetModelList());
            sb.AppendLine(CreateGetModelListByWhere());
            sb.AppendLine(CreateGetModelListByCache());
            sb.AppendLine(CreateGetModelListByWhereAndCache());
            sb.AppendLine(CreateGetModelList4Page());
            sb.AppendLine(CreateGetRecordCount());
            sb.AppendLine(CreateDataRowToModel());

            sb.AppendLine("}");

            sb.AppendLine("}");

            return sb;
        }

        private string CreateIsExistentByWhere()
        {
            return string.Format(@"        public bool IsExistent(string where)
        {{
            return GetModel(where) != null;
        }}");
        }

        private string CreateIsExistent()
        {
            return string.Format(@"        public bool IsExistent({0} {1})
        {{
            return GetModel({1}) != null;
        }}", GetPrimaryKeyType(), GetPrimaryKeyCamel());
        }

        private string CreateGetModelListByWhereAndCache()
        {
            return string.Format(@"        public List<{1}> GetModelListByCache(string where)
        {{
            if (string.IsNullOrWhiteSpace(where))
            {{
                 throw new Exception({0}where 不能为null或空字符串{0});
            }}
            string cacheKey = {0}{1}List-{0}+where;
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {{
                obj = this.GetModelList(where);
                if (obj != null)
                {{
                    int cacheValidTime = ConfigHelper.GetConfigInt({0}CacheValidTime{0});
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }}
            }}
            return (List<{1}>)obj;
        }}", "\"", ProcessedTableName);
        }

        private string CreateGetModelListByCache()
        {
            return string.Format(@"        public List<{1}> GetModelListByCache()
        {{
            string cacheKey = {0}{1}List-{0};
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {{
                obj = this.GetModelList();
                if (obj != null)
                {{
                    int cacheValidTime = ConfigHelper.GetConfigInt({0}CacheValidTime{0});
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }}
            }}
            return (List<{1}>)obj;
        }}", "\"", ProcessedTableName);
        }

        private string CreateGetModelByWhereAndCache()
        {
            return string.Format(@"        public {1} GetModelByCache(string where)
        {{
            if (string.IsNullOrWhiteSpace(where))
            {{
                 throw new Exception({0}where 不能为null或空字符串{0});
            }}
            string cacheKey = {0}{1}-{0} + where;
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {{
                obj = this.GetModel(where);
                if (obj != null)
                {{
                    int cacheValidTime = ConfigHelper.GetConfigInt({0}CacheValidTime{0});
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }}
            }}
            return ({1})obj;
        }}", "\"", ProcessedTableName);
        }

        private string CreateGetModelByCache()
        {
            return string.Format(@"        public {1} GetModelByCache({2} {3})
        {{
            string cacheKey = {0}{1}-{0} + {3};
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {{
                obj = this.GetModel({3});
                if (obj != null)
                {{
                    int cacheValidTime = ConfigHelper.GetConfigInt({0}CacheValidTime{0});
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }}
            }}
            return ({1})obj;
        }}", "\"", ProcessedTableName, GetPrimaryKeyType(), GetPrimaryKeyCamel());
        }

        private string CreateDataRowToModel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public " + ProcessedTableName + " DataRowToModel(DataRow row)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().DataRowToModel(row);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateGetRecordCount()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" public " + GetPrimaryKeyType() + " GetRecordCount(string where)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().GetRecordCount(where);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateGetModelList4Page()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" public List<" + ProcessedTableName + "> GetModelList(string orderBy, string where, long startRowNum, long endRowNum)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().GetModelList(orderBy, where, startRowNum, endRowNum);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateGetModelListByWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" public List<" + ProcessedTableName + "> GetModelList(string where)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().GetModelList(where);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateGetModelList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" public List<" + ProcessedTableName + "> GetModelList()");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().GetModelList();");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateGetModelByWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public " + ProcessedTableName + " GetModel(string where)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().GetModel(where);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateGetModel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public " + ProcessedTableName + " GetModel(" + GetPrimaryKeyType() + " " + GetPrimaryKeyCamel() + ")");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().GetModel(" + GetPrimaryKeyCamel() + ");");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateUpdate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public int Update(" + ProcessedTableName + " model)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().Update(model);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateDeleteList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public int DeleteList(string idList)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().DeleteList(idList);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateDeleteByWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public int Delete(string where)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().Delete(where);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateDelete()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public int Delete(" + GetPrimaryKeyType() + " " + GetPrimaryKeyCamel() + ")");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().Delete(" + GetPrimaryKeyCamel() + ");");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string CreateInsert()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public " + GetPrimaryKeyType() + " Insert(" + ProcessedTableName + " model)");
            sb.AppendLine("{");
            sb.AppendLine("return new " + ProcessedTableName + "DAL().Insert(model);");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string GetPrimaryKey()
        {
            string cmdText = "select * from INFORMATION_SCHEMA.columns where table_name = @name";
            DataTable table = SQLHelper.ExecuteDataTable(ConStr, cmdText, new SqlParameter("name", TableName));

            DataRow idRow = table.Rows[0];//保存id
            string id = idRow["column_name"].ToString();
            return id;
        }

        private string GetPrimaryKeyCamel()
        {
            string primaryKey = GetPrimaryKey();
            return Helper.ChangeFirstLetterToLowercase(primaryKey);
        }

        private string GetPrimaryKeyType()
        {
            string cmdText = "select * from INFORMATION_SCHEMA.columns where table_name = @name";
            DataTable table = SQLHelper.ExecuteDataTable(ConStr, cmdText, new SqlParameter("name", TableName));
            DataRow primaryKeyRow = table.Rows[0];//默认primaryKey在表的第一行
            string primaryKeySqlType = primaryKeyRow["data_type"].ToString();
            string primaryKeyCSharpType = Helper.ChangeSQLTypeToCSharpType(primaryKeySqlType, false);
            return primaryKeyCSharpType;
        }
    }
}