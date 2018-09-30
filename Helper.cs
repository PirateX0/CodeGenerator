using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class Helper
    {
        public static string ChangeSQLTypeToCSharpType(string sqlType, bool isNullable)
        {
            #region 使用数组的实现方法

            //SQLType_CSharpType[] SQLType_CSharpType__Array = new SQLType_CSharpType[]
            //{
            //    #region 纯手打，已吐血

            //    new SQLType_CSharpType{SQLType="int",IsNullable=false,CSharpType="Int32"},
            //    new SQLType_CSharpType{SQLType="int",IsNullable=true, CSharpType="Int32?"},
            //    new SQLType_CSharpType{SQLType="text",IsNullable=false, CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="text",IsNullable=true, CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="bigint",IsNullable=false, CSharpType="Int64"},
            //    new SQLType_CSharpType{SQLType="bigint",IsNullable=true, CSharpType="Int64?"},
            //    new SQLType_CSharpType{SQLType="binary",IsNullable=false, CSharpType="System.Byte[]"},
            //    new SQLType_CSharpType{SQLType="binary",IsNullable=true, CSharpType="System.Byte[]"},
            //    new SQLType_CSharpType{SQLType="bit",IsNullable=false, CSharpType="bool"},
            //    new SQLType_CSharpType{SQLType="bit",IsNullable=true, CSharpType="bool?"},
            //    new SQLType_CSharpType{SQLType="char",IsNullable=false, CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="char",IsNullable=true,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="datetime",IsNullable=false,CSharpType="System.DateTime"},
            //    new SQLType_CSharpType{SQLType="datetime",IsNullable=true,CSharpType="System.DateTime?"},
            //    new SQLType_CSharpType{SQLType="decimal",IsNullable=false,CSharpType="System.Decimal"},
            //    new SQLType_CSharpType{SQLType="decimal",IsNullable=true,CSharpType="System.Decimal?"},
            //    new SQLType_CSharpType{SQLType="float",IsNullable=false,CSharpType="System.Double"},
            //    new SQLType_CSharpType{SQLType="float",IsNullable=true,CSharpType="System.Double?"},
            //    new SQLType_CSharpType{SQLType="image",IsNullable=false,CSharpType="System.Byte[]"},
            //    new SQLType_CSharpType{SQLType="image",IsNullable=true,CSharpType="System.Byte[]"},
            //    new SQLType_CSharpType{SQLType="money",IsNullable=false,CSharpType="System.Decimal"},
            //    new SQLType_CSharpType{SQLType="money",IsNullable=true,CSharpType="System.Decimal?"},
            //    new SQLType_CSharpType{SQLType="nchar",IsNullable=false,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="nchar",IsNullable=true,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="ntext",IsNullable=false,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="ntext",IsNullable=true,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="numeric",IsNullable=false,CSharpType="System.Decimal?"},
            //    new SQLType_CSharpType{SQLType="numeric",IsNullable=true,CSharpType="System.Decimal??"},
            //    new SQLType_CSharpType{SQLType="nvarchar",IsNullable=false,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="nvarchar",IsNullable=true,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="real",IsNullable=false,CSharpType="System.Single"},
            //    new SQLType_CSharpType{SQLType="real",IsNullable=true,CSharpType="System.Single?"},
            //    new SQLType_CSharpType{SQLType="smalldatetime",IsNullable=false,CSharpType="System.DateTime"},
            //    new SQLType_CSharpType{SQLType="smalldatetime",IsNullable=true,CSharpType="System.DateTime?"},
            //    new SQLType_CSharpType{SQLType="smallint",IsNullable=false,CSharpType="Int16"},
            //    new SQLType_CSharpType{SQLType="smallint",IsNullable=true,CSharpType="Int16?"},
            //    new SQLType_CSharpType{SQLType="smallmoney",IsNullable=false,CSharpType="System.Decimal"},
            //    new SQLType_CSharpType{SQLType="smallmoney",IsNullable=true,CSharpType="System.Decimal?"},
            //    new SQLType_CSharpType{SQLType="timestamp",IsNullable=false,CSharpType="System.DateTime"},
            //    new SQLType_CSharpType{SQLType="timestamp",IsNullable=true,CSharpType="System.DateTime?"},
            //    new SQLType_CSharpType{SQLType="tinyint",IsNullable=false,CSharpType="System.Byte"},
            //    new SQLType_CSharpType{SQLType="tinyint",IsNullable=true,CSharpType="System.Byte?"},
            //    new SQLType_CSharpType{SQLType="uniqueidentifier",IsNullable=false,CSharpType="System.Guid"},
            //    new SQLType_CSharpType{SQLType="uniqueidentifier",IsNullable=true,CSharpType="System.Guid?"},
            //    new SQLType_CSharpType{SQLType="varbinary",IsNullable=false,CSharpType="System.Byte[]"},
            //    new SQLType_CSharpType{SQLType="varbinary",IsNullable=true,CSharpType="System.Byte[]"},
            //    new SQLType_CSharpType{SQLType="varchar",IsNullable=false,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="varchar",IsNullable=true,CSharpType="string"},
            //    new SQLType_CSharpType{SQLType="variant",IsNullable=false,CSharpType="object"},
            //    new SQLType_CSharpType{SQLType="variant",IsNullable=true, CSharpType="object"}

            //     #endregion 纯手打，已吐血
            //};

            //foreach (var item in SQLType_CSharpType__Array)
            //{
            //    if (item.SQLType == sqlType && item.IsNullable == isNullable)
            //    {
            //        return item.CSharpType;
            //    }
            //}
            //return "object";

            #endregion 使用数组的实现方法

            #region 使用字典的实现方法

            //两种方法都差不多。。
            //类作为key需要重写SQLType_CSharpType类的GetHashCode和Equals方法
            //其实可以把key,value写成字符串的形式。。
            Dictionary<SQLType_CSharpType, SQLType_CSharpType> SQLType_CSharpType__Dictionary = new Dictionary<SQLType_CSharpType, SQLType_CSharpType>
            {
                #region 纯手打，已吐血

                {new SQLType_CSharpType{SQLType="int",IsNullable=false},new SQLType_CSharpType{CSharpType="Int32"}},
                {new SQLType_CSharpType{SQLType="int",IsNullable=true},new SQLType_CSharpType{CSharpType="Int32?"}},
                {new SQLType_CSharpType{SQLType="text",IsNullable=false},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="text",IsNullable=true},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="bigint",IsNullable=false},new SQLType_CSharpType{CSharpType="Int64"}},
                {new SQLType_CSharpType{SQLType="bigint",IsNullable=true},new SQLType_CSharpType{CSharpType="Int64?"}},
                {new SQLType_CSharpType{SQLType="binary",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Byte[]"}},
                {new SQLType_CSharpType{SQLType="binary",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Byte[]"}},
                {new SQLType_CSharpType{SQLType="bit",IsNullable=false},new SQLType_CSharpType{CSharpType="bool"}},
                {new SQLType_CSharpType{SQLType="bit",IsNullable=true},new SQLType_CSharpType{CSharpType="bool?"}},
                {new SQLType_CSharpType{SQLType="char",IsNullable=false},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="char",IsNullable=true},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="datetime",IsNullable=false},new SQLType_CSharpType{CSharpType="System.DateTime"}},
                {new SQLType_CSharpType{SQLType="datetime",IsNullable=true},new SQLType_CSharpType{CSharpType="System.DateTime?"}},
                {new SQLType_CSharpType{SQLType="decimal",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Decimal"}},
                {new SQLType_CSharpType{SQLType="decimal",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Decimal?"}},
                {new SQLType_CSharpType{SQLType="float",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Double"}},
                {new SQLType_CSharpType{SQLType="float",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Double?"}},
                {new SQLType_CSharpType{SQLType="image",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Byte[]"}},
                {new SQLType_CSharpType{SQLType="image",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Byte[]"}},
                {new SQLType_CSharpType{SQLType="money",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Decimal"}},
                {new SQLType_CSharpType{SQLType="money",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Decimal?"}},
                {new SQLType_CSharpType{SQLType="nchar",IsNullable=false},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="nchar",IsNullable=true},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="ntext",IsNullable=false},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="ntext",IsNullable=true},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="numeric",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Decimal?"}},
                {new SQLType_CSharpType{SQLType="numeric",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Decimal??"}},
                {new SQLType_CSharpType{SQLType="nvarchar",IsNullable=false},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="nvarchar",IsNullable=true},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="real",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Single"}},
                {new SQLType_CSharpType{SQLType="real",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Single?"}},
                {new SQLType_CSharpType{SQLType="smalldatetime",IsNullable=false},new SQLType_CSharpType{CSharpType="System.DateTime"}},
                {new SQLType_CSharpType{SQLType="smalldatetime",IsNullable=true},new SQLType_CSharpType{CSharpType="System.DateTime?"}},
                {new SQLType_CSharpType{SQLType="smallint",IsNullable=false},new SQLType_CSharpType{CSharpType="Int16"}},
                {new SQLType_CSharpType{SQLType="smallint",IsNullable=true},new SQLType_CSharpType{CSharpType="Int16?"}},
                {new SQLType_CSharpType{SQLType="smallmoney",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Decimal"}},
                {new SQLType_CSharpType{SQLType="smallmoney",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Decimal?"}},
                {new SQLType_CSharpType{SQLType="timestamp",IsNullable=false},new SQLType_CSharpType{CSharpType="System.DateTime"}},
                {new SQLType_CSharpType{SQLType="timestamp",IsNullable=true},new SQLType_CSharpType{CSharpType="System.DateTime?"}},
                {new SQLType_CSharpType{SQLType="tinyint",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Byte"}},
                {new SQLType_CSharpType{SQLType="tinyint",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Byte?"}},
                {new SQLType_CSharpType{SQLType="uniqueidentifier",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Guid"}},
                {new SQLType_CSharpType{SQLType="uniqueidentifier",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Guid?"}},
                {new SQLType_CSharpType{SQLType="varbinary",IsNullable=false},new SQLType_CSharpType{CSharpType="System.Byte[]"}},
                {new SQLType_CSharpType{SQLType="varbinary",IsNullable=true},new SQLType_CSharpType{CSharpType="System.Byte[]"}},
                {new SQLType_CSharpType{SQLType="varchar",IsNullable=false},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="varchar",IsNullable=true},new SQLType_CSharpType{CSharpType="string"}},
                {new SQLType_CSharpType{SQLType="variant",IsNullable=false},new SQLType_CSharpType{CSharpType="object"}},
                {new SQLType_CSharpType{SQLType="variant",IsNullable=true},new SQLType_CSharpType{CSharpType="object"}},
                 #endregion 纯手打，已吐血
            };

            SQLType_CSharpType sc = new SQLType_CSharpType { SQLType = sqlType, IsNullable = isNullable };
            if (SQLType_CSharpType__Dictionary.ContainsKey(sc))
            {
                return SQLType_CSharpType__Dictionary[sc].CSharpType;
            }

            return "object";

            #endregion 使用字典的实现方法

            #region 旧写法

            //system.datetime等改大小写
            //int32等改int32?
            //string reval = string.Empty;
            //switch (SQLType.ToLower())
            //{
            //    case "int":
            //        reval = "Int32?";
            //        break;

            //    case "text":
            //        reval = "string";
            //        break;

            //    case "bigint":
            //        reval = "Int64?";
            //        break;

            //    case "binary":
            //        reval = "System.Byte[]";
            //        break;

            //    case "bit":
            //        reval = "bool?";
            //        break;

            //    case "char":
            //        reval = "string";
            //        break;

            //    case "datetime":
            //        reval = "System.DateTime?";
            //        break;

            //    case "decimal":
            //        reval = "System.Decimal?";
            //        break;

            //    case "float":
            //        reval = "System.Double?";
            //        break;

            //    case "image":
            //        reval = "System.Byte[]";
            //        break;

            //    case "money":
            //        reval = "System.Decimal?";
            //        break;

            //    case "nchar":
            //        reval = "string";
            //        break;

            //    case "ntext":
            //        reval = "string";
            //        break;

            //    case "numeric":
            //        reval = "System.Decimal?";
            //        break;

            //    case "nvarchar":
            //        reval = "string";
            //        break;

            //    case "real":
            //        reval = "System.Single";
            //        break;

            //    case "smalldatetime":
            //        reval = "System.DateTime?";
            //        break;

            //    case "smallint":
            //        reval = "Int16?";
            //        break;

            //    case "smallmoney":
            //        reval = "System.Decimal?";
            //        break;

            //    case "timestamp":
            //        reval = "System.DateTime?";
            //        break;

            //    case "tinyint":
            //        reval = "System.Byte?";
            //        break;

            //    case "uniqueidentifier":
            //        reval = "System.Guid";
            //        break;

            //    case "varbinary":
            //        reval = "System.Byte[]";
            //        break;

            //    case "varchar":
            //        reval = "string";
            //        break;

            //    case "variant":
            //        reval = "object";
            //        break;

            //    default:
            //        reval = "object";
            //        break;
            //}
            //return reval;

            #endregion 旧写法
        }

        public static string ChangeToUppercaseOrLowercase(string str)
        {
            if (char.IsLower(str, 0))
            {
                return str.ToUpper();
            }
            else
            {
                return str.ToLower();
            }
        }

        public static string ChangeFirstLetterToLowercase(string str)
        {
            string strUpper = str.ToLower();
            return str = strUpper[0] + str.Substring(1);
        }
    }
}