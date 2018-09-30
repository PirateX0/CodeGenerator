using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class SQLColumn
    {
        public string ColumnName { get; set; }
        public bool IsNullable { get; set; }
        public string SQLType { get; set; }
    }
}