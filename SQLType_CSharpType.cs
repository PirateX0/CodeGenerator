using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class SQLType_CSharpType
    {
        public string SQLType { get; set; }
        public string CSharpType { get; set; }
        public bool IsNullable { get; set; }

        public override bool Equals(object obj)
        {
            SQLType_CSharpType sc = obj as SQLType_CSharpType;
            if (sc == null)
                return false;
            else
                return sc.SQLType == this.SQLType && sc.IsNullable == this.IsNullable;
        }

        public override int GetHashCode()
        {
            string hashCode = SQLType + (IsNullable == true ? "true" : "false");
            return hashCode.GetHashCode();
        }
    }
}