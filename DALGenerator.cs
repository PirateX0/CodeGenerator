using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class DALGenerator
    {
        public string NameSpace { get; set; }
        public string ConStr { get; set; }
        public string FolderPath { get; set; }
        public string TableName { get; set; }
        public string ProcessedTableName { get; set; }

        public void GenerateDAL2010()
        {
            StringBuilder sb = new StringBuilder();

            sb = GetDALStringBuilder("2010");

            WriteDALToFile(sb);
        }

        private StringBuilder GetDALStringBuilder(string version)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            if (version == "2010")
            {
                sb.AppendLine("using System.Linq;");
            }
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using " + NameSpace + ".Model;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + NameSpace + ".DAL");
            sb.AppendLine("{");

            sb.AppendLine("public partial class " + ProcessedTableName + "DAL");
            sb.AppendLine("{");

            sb.AppendLine(CreateInsert());
            sb.AppendLine(CreateDelete());
            sb.AppendLine(CreateDeleteByWhere());
            sb.AppendLine(CreateDeleteList());
            sb.AppendLine(CreateUpdate());
            sb.AppendLine(CreateGetModel());
            sb.AppendLine(CreateGetModelByWhere());
            sb.AppendLine(CreateGetModelList());
            sb.AppendLine(CreateGetModelListByWhere());
            sb.AppendLine(CreateGetModelListByRowNum());
            sb.AppendLine(CreateGetRecordCountByWhere());
            sb.AppendLine(CreateDataRowToModel());

            sb.AppendLine("}");

            sb.AppendLine("}");

            return sb;
        }

        private string CreateGetModelByWhere()
        {
            return string.Format(@"        public {2} GetModel(string where)
        {{
            string sqlText = {0}select * from {1} {0};
            if (!string.IsNullOrWhiteSpace(where))
            {{
                sqlText += {0}where {0} + where;
            }}
            else
            {{
                throw new Exception({0}where 不能为null或空字符串{0});
            }}
            DataTable table = SQLHelper.ExecuteDataTable(sqlText);
            if (table.Rows.Count <= 0) {{ return null; }}
            else
            {{
                return DataRowToModel(table.Rows[0]);
            }}
        }}", "\"", TableName, ProcessedTableName);
        }

        private string CreateDeleteByWhere()
        {
            return string.Format(@"        public int Delete(string where)
        {{
            string sqlText = {0}delete FROM {1} {0};
            if (!string.IsNullOrWhiteSpace(where))
            {{
                sqlText += {0}where {0} + where;
            }}
            return SQLHelper.ExecuteNonQuery(sqlText);
        }}", "\"", TableName);
        }

        private void WriteDALToFile(StringBuilder sb)
        {
            string dir = Path.Combine(FolderPath, "DAL");
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, ProcessedTableName + "DAL.cs");
            File.WriteAllText(path, sb.ToString());
        }

        private string CreateInsert()
        {
            DataTable table = GetTable();
            string primaryKey = GetPrimaryKey();
            string primaryKeyType = GetPrimaryKeyType();
            StringBuilder sb = new StringBuilder();
            List<SQLColumn> columnListExceptPK = GetColumnListExceptPK();
            List<string> columnNameList = GetColumnNameList(columnListExceptPK);//声明一个动态数组，保存除id外的字段的集合
            List<string> columnNameParameterList = GetColumnNameParameterList(columnListExceptPK);//声明一个动态数组，保存加@的除id外的字段的集合
            string columnNameListStr = string.Join(",", columnNameList);
            string columnNameParameterListStr = string.Join(",", columnNameParameterList);

            sb.AppendLine("public " + primaryKeyType + " Insert(" + ProcessedTableName + " model)");
            sb.AppendLine("{");
            sb.AppendLine("object o = DBNull.Value;");
            sb.AppendLine(string.Format(" return ({0})SQLHelper.ExecuteScalar(", primaryKeyType));
            sb.AppendLine("\"insert into " + TableName + " (" + columnNameListStr + ") output inserted." + primaryKey + " values(" + columnNameParameterListStr + ")\" ");
            foreach (SQLColumn column in columnListExceptPK)
            {
                if (column.IsNullable)
                {
                    sb.AppendLine(string.Format(" , new SqlParameter(\"{0}\", (model.{0} == null) ? o : model.{0})", column.ColumnName));
                }
                else
                {
                    sb.AppendLine(string.Format(" , new SqlParameter(\"{0}\", model.{0})", column.ColumnName));
                }
            }
            sb.AppendLine(" );");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CreateDelete()
        {
            string primaryKey = GetPrimaryKey();
            string primaryKeyType = GetPrimaryKeyType();
            string primaryKeyCamel = Helper.ChangeFirstLetterToLowercase(primaryKey);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("public int Delete(" + primaryKeyType + " " + primaryKeyCamel + ")");
            sb.AppendLine("{");
            sb.AppendLine("string sqlText = @\"delete from " + TableName + "  where " + primaryKey + " =@" + primaryKeyCamel + " \";");
            sb.AppendLine(string.Format("return SQLHelper.ExecuteNonQuery(sqlText, new SqlParameter(\"{0}\", {0})); ", primaryKeyCamel));

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CreateDeleteList()
        {
            string primaryKey = GetPrimaryKey();
            string primaryKeyType = GetPrimaryKeyType();
            string primaryKeyCamel = Helper.ChangeFirstLetterToLowercase(primaryKey);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("public int DeleteList(string idList)");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("SQLHelper.CheckIdList<{0}>(idList);", primaryKeyType));
            sb.AppendLine(string.Format("string sqlText = @\"delete from {0} where {1} in( \"+idList+\" ) \";", TableName, primaryKey));
            sb.AppendLine(string.Format("return SQLHelper.ExecuteNonQuery(sqlText);"));
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CreateUpdate()
        {
            DataTable table = GetTable();
            string primaryKey = GetPrimaryKey();
            StringBuilder sb = new StringBuilder();
            List<SQLColumn> conlumnList = GetColumnList();
            string setTextListStr = GetSetText();

            sb.AppendLine("public int Update(" + ProcessedTableName + " model)");
            sb.AppendLine("{");
            sb.AppendLine("object o = DBNull.Value;");
            sb.AppendLine("string sqlText = @\"update " + TableName + " set " + setTextListStr + " where " + primaryKey + " =@" + primaryKey + " \";");
            sb.AppendLine("return SQLHelper.ExecuteNonQuery(sqlText");
            foreach (SQLColumn column in conlumnList)
            {
                if (column.IsNullable)
                {
                    sb.AppendLine(string.Format(" , new SqlParameter(\"{0}\", (model.{0} == null) ? o : model.{0})", column.ColumnName));
                }
                else
                {
                    sb.AppendLine(string.Format(", new SqlParameter(\"{0}\", model.{0}) ", column.ColumnName));
                }
            }
            sb.AppendLine(");");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CreateGetModel()
        {
            string primaryKey = GetPrimaryKey();
            string primaryKeyType = GetPrimaryKeyType();
            string primaryKeyCamel = Helper.ChangeFirstLetterToLowercase(primaryKey);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("public " + ProcessedTableName + " GetModel(" + primaryKeyType + " " + primaryKeyCamel + ")");
            sb.AppendLine("{");
            sb.AppendLine("string sqlText = @\"select * from " + TableName + " where " + primaryKey + " = @" + primaryKeyCamel + "\";");
            sb.AppendLine(string.Format(" DataTable table = SQLHelper.ExecuteDataTable(sqlText, new SqlParameter(\"{0}\",{0}));", primaryKeyCamel));
            sb.AppendLine("if (table.Rows.Count <= 0) { return null; }");
            sb.AppendLine("else");
            sb.AppendLine("{");
            sb.AppendLine("return DataRowToModel(table.Rows[0]);");
            sb.AppendLine("}");

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CreateGetModelList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public List<" + ProcessedTableName + "> GetModelList()");
            sb.AppendLine("{");

            sb.AppendLine("string sqlText = @\"select * from " + TableName + "\";");
            sb.AppendLine("DataTable table = SQLHelper.ExecuteDataTable(sqlText);");

            sb.AppendLine("List<" + ProcessedTableName + "> modelList = new List<" + ProcessedTableName + ">();");
            sb.AppendLine("foreach (DataRow row in table.Rows)");
            sb.AppendLine("{");
            sb.AppendLine("modelList.Add(DataRowToModel(row));");
            sb.AppendLine("}");
            sb.AppendLine("return modelList;");

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CreateGetModelListByWhere()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(@"        public List<{1}> GetModelList(string where)
        {{
            string sqlText = @{0}select * from {2}{0};
            if (!string.IsNullOrWhiteSpace(where))
            {{
                sqlText += {0} where {0} + where;
            }}
            DataTable table = SQLHelper.ExecuteDataTable(sqlText);
            List<{1}> modelList = new List<{1}>();
            foreach (DataRow row in table.Rows)
            {{
                modelList.Add(DataRowToModel(row));
            }}
            return modelList;
        }}", "\"", ProcessedTableName, TableName);

            return sb.ToString();
        }

        private string CreateGetModelListByRowNum()
        {
            return string.Format(@"        public List<{1}> GetModelList(string orderBy, string where, long startRowNum, long endRowNum)
        {{
            string sqlText = string.Format(@{0}select * from
                                            (
                                            select ROW_NUMBER() over(order by t.{{0}}) rownum,*  from {2} t
                                            where {{1}}
                                            )tt
                                            where tt.rownum>=@startRowNum and tt.rownum<=@endRowNum{0}, orderBy, where);
            DataTable dt = SQLHelper.ExecuteDataTable(sqlText,
                new SqlParameter({0}startRowNum{0}, startRowNum),
                new SqlParameter({0}endRowNum{0}, endRowNum)
                );
            List<{1}> list = new List<{1}>();
            foreach (DataRow row in dt.Rows)
            {{
                list.Add(DataRowToModel(row));
            }}
            return list;
        }}", "\"", ProcessedTableName, TableName);
        }

        private string CreateGetRecordCountByWhere()
        {
            return string.Format(@"        public {2} GetRecordCount(string where)
        {{
            string sqlText = {0}select count(*) FROM {1} {0};
            if (!string.IsNullOrWhiteSpace(where))
            {{
                sqlText += {0}where {0} + where;
            }}
            return Convert.To{2}(SQLHelper.ExecuteScalar(sqlText));
        }}", "\"", TableName, GetPrimaryKeyType());
        }

        private string CreateDataRowToModel()
        {
            string cmdText = "select * from INFORMATION_SCHEMA.columns where table_name = @name";
            DataTable table = SQLHelper.ExecuteDataTable(ConStr, cmdText, new SqlParameter("name", TableName));
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("public " + ProcessedTableName + " DataRowToModel(DataRow row)");
            sb.AppendLine("{");

            sb.AppendLine("" + ProcessedTableName + " model = new " + ProcessedTableName + "();");
            foreach (DataRow row in table.Rows)
            {
                string sqlType = row["data_type"].ToString();
                bool isNullable = row["is_nullable"].ToString() == "YES" ? true : false;
                string columnName = row["column_name"].ToString();
                string cSharpType = Helper.ChangeSQLTypeToCSharpType(sqlType, isNullable);
                if (isNullable)
                {
                    sb.AppendLine(string.Format(" model.{0} = row.IsNull(\"{0}\") ? null : ({1})row[\"{0}\"];", columnName, cSharpType));
                }
                else
                {
                    sb.AppendLine(string.Format("model.{0} = ({1})row[\"{0}\"];", columnName, cSharpType));
                }
            }
            sb.AppendLine(" return model;");

            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// 得到cmdText中set后面的内容
        /// </summary>
        /// <returns></returns>
        private string GetSetText()
        {
            DataTable table = GetTable();
            List<string> setList = new List<string>();//声明一个动态数组，保存cmdText中set后面的内容的集合
            //假设id是第一个元素，且自动增长，索引从1开始，排除id
            for (int i = 1; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                setList.Add(row["column_name"].ToString() + "=@" + row["column_name"].ToString());
            }
            string setText = string.Join(",", setList);
            return setText;
        }

        private List<SQLColumn> GetColumnListExceptPK()
        {
            List<SQLColumn> columnListExceptPK = GetColumnList();
            columnListExceptPK.RemoveAt(0);//去掉主键列
            return columnListExceptPK;
        }

        private List<SQLColumn> GetColumnList()
        {
            DataTable table = GetTable();
            List<SQLColumn> columnList = new List<SQLColumn>();
            foreach (DataRow row in table.Rows)
            {
                SQLColumn column = new SQLColumn();
                column.ColumnName = row["column_name"].ToString();
                column.IsNullable = row["is_nullable"].ToString() == "YES" ? true : false;
                columnList.Add(column);
            }
            return columnList;
        }

        private List<string> GetColumnNameList(List<SQLColumn> columnList)
        {
            List<string> columnNameList = new List<string>();
            foreach (SQLColumn column in columnList)
            {
                columnNameList.Add(column.ColumnName);
            }
            return columnNameList;
        }

        private List<string> GetColumnNameParameterList(List<SQLColumn> columnList)
        {
            List<string> columnNameParameterList = new List<string>();
            foreach (SQLColumn column in columnList)
            {
                columnNameParameterList.Add("@" + column.ColumnName);
            }
            return columnNameParameterList;
        }

        private DataTable GetTable()
        {
            string cmdText = "select * from INFORMATION_SCHEMA.columns where table_name = @name";
            DataTable table = SQLHelper.ExecuteDataTable(ConStr, cmdText, new SqlParameter("name", TableName));
            return table;
        }

        private string GetPrimaryKey()
        {
            string cmdText = "select * from INFORMATION_SCHEMA.columns where table_name = @name";
            DataTable table = SQLHelper.ExecuteDataTable(ConStr, cmdText, new SqlParameter("name", TableName));

            DataRow primaryKeyRow = table.Rows[0];//默认primaryKey在表的第一行
            string primaryKey = primaryKeyRow["column_name"].ToString();
            return primaryKey;
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