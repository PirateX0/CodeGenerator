using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RuPengSite.Model;
using System.Data.SqlClient;
using System.Data;

namespace RuPengSite.DAL
{
public partial class PowerDAL
{
public Int64 Insert(Power model)
{
object o = DBNull.Value;
 return (Int64)SQLHelper.ExecuteScalar(
"insert into T_Powers (PowerName,ControllerName,ActionName) output inserted.Id values(@PowerName,@ControllerName,@ActionName)" 
 , new SqlParameter("PowerName", model.PowerName)
 , new SqlParameter("ControllerName", model.ControllerName)
 , new SqlParameter("ActionName", model.ActionName)
 );
}

public int Delete(Int64 id)
{
string sqlText = @"delete from T_Powers  where Id =@id ";
return SQLHelper.ExecuteNonQuery(sqlText, new SqlParameter("id", id)); 
}

        public int Delete(string where)
        {
            string sqlText = "delete FROM T_Powers ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sqlText += "where " + where;
            }
            return SQLHelper.ExecuteNonQuery(sqlText);
        }
public int DeleteList(string idList)
{
SQLHelper.CheckIdList<Int64>(idList);
string sqlText = @"delete from T_Powers where Id in( "+idList+" ) ";
return SQLHelper.ExecuteNonQuery(sqlText);
}

public int Update(Power model)
{
object o = DBNull.Value;
string sqlText = @"update T_Powers set PowerName=@PowerName,ControllerName=@ControllerName,ActionName=@ActionName where Id =@Id ";
return SQLHelper.ExecuteNonQuery(sqlText
, new SqlParameter("Id", model.Id) 
, new SqlParameter("PowerName", model.PowerName) 
, new SqlParameter("ControllerName", model.ControllerName) 
, new SqlParameter("ActionName", model.ActionName) 
);
}

public Power GetModel(Int64 id)
{
string sqlText = @"select * from T_Powers where Id = @id";
 DataTable table = SQLHelper.ExecuteDataTable(sqlText, new SqlParameter("id",id));
if (table.Rows.Count <= 0) { return null; }
else
{
return DataRowToModel(table.Rows[0]);
}
}

        public Power GetModel(string where)
        {
            string sqlText = "select * from T_Powers ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sqlText += "where " + where;
            }
            else
            {
                throw new Exception("where 不能为null或空字符串");
            }
            DataTable table = SQLHelper.ExecuteDataTable(sqlText);
            if (table.Rows.Count <= 0) { return null; }
            else
            {
                return DataRowToModel(table.Rows[0]);
            }
        }
public List<Power> GetModelList()
{
string sqlText = @"select * from T_Powers";
DataTable table = SQLHelper.ExecuteDataTable(sqlText);
List<Power> modelList = new List<Power>();
foreach (DataRow row in table.Rows)
{
modelList.Add(DataRowToModel(row));
}
return modelList;
}

        public List<Power> GetModelList(string where)
        {
            string sqlText = @"select * from T_Powers";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sqlText += " where " + where;
            }
            DataTable table = SQLHelper.ExecuteDataTable(sqlText);
            List<Power> modelList = new List<Power>();
            foreach (DataRow row in table.Rows)
            {
                modelList.Add(DataRowToModel(row));
            }
            return modelList;
        }
        public List<Power> GetModelList(string orderBy, string where, long startRowNum, long endRowNum)
        {
            string sqlText = string.Format(@"select * from
                                            (
                                            select ROW_NUMBER() over(order by t.{0}) rownum,*  from T_Powers t
                                            where {1}
                                            )tt
                                            where tt.rownum>=@startRowNum and tt.rownum<=@endRowNum", orderBy, where);
            DataTable dt = SQLHelper.ExecuteDataTable(sqlText,
                new SqlParameter("startRowNum", startRowNum),
                new SqlParameter("endRowNum", endRowNum)
                );
            List<Power> list = new List<Power>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(DataRowToModel(row));
            }
            return list;
        }
        public Int64 GetRecordCount(string where)
        {
            string sqlText = "select count(*) FROM T_Powers ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sqlText += "where " + where;
            }
            return Convert.ToInt64(SQLHelper.ExecuteScalar(sqlText));
        }
public Power DataRowToModel(DataRow row)
{
Power model = new Power();
model.Id = (Int64)row["Id"];
model.PowerName = (string)row["PowerName"];
model.ControllerName = (string)row["ControllerName"];
model.ActionName = (string)row["ActionName"];
 return model;
}

}
}
