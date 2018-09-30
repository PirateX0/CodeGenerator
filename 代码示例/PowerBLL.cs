using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RuPengSite.Model;
using RuPengSite.DAL;
using RuPengSite.Utility;

namespace RuPengSite.BLL
{
public partial class PowerBLL
{
        public bool IsExistent(Int64 id)
        {
            return GetModel(id) != null;
        }
        public bool IsExistent(string where)
        {
            return GetModel(where) != null;
        }
public Int64 Insert(Power model)
{
return new PowerDAL().Insert(model);
}

public int Delete(Int64 id)
{
return new PowerDAL().Delete(id);
}

public int Delete(string where)
{
return new PowerDAL().Delete(where);
}

public int DeleteList(string idList)
{
return new PowerDAL().DeleteList(idList);
}

public int Update(Power model)
{
return new PowerDAL().Update(model);
}

public Power GetModel(Int64 id)
{
return new PowerDAL().GetModel(id);
}

public Power GetModel(string where)
{
return new PowerDAL().GetModel(where);
}

        public Power GetModelByCache(Int64 id)
        {
            string cacheKey = "Power-" + id;
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {
                obj = this.GetModel(id);
                if (obj != null)
                {
                    int cacheValidTime = ConfigHelper.GetConfigInt("CacheValidTime");
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }
            }
            return (Power)obj;
        }
        public Power GetModelByCache(string where)
        {
            if (string.IsNullOrWhiteSpace(where))
            {
                 throw new Exception("where 不能为null或空字符串");
            }
            string cacheKey = "Power-" + where;
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {
                obj = this.GetModel(where);
                if (obj != null)
                {
                    int cacheValidTime = ConfigHelper.GetConfigInt("CacheValidTime");
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }
            }
            return (Power)obj;
        }
 public List<Power> GetModelList()
{
return new PowerDAL().GetModelList();
}

 public List<Power> GetModelList(string where)
{
return new PowerDAL().GetModelList(where);
}

        public List<Power> GetModelListByCache()
        {
            string cacheKey = "PowerList-";
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {
                obj = this.GetModelList();
                if (obj != null)
                {
                    int cacheValidTime = ConfigHelper.GetConfigInt("CacheValidTime");
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }
            }
            return (List<Power>)obj;
        }
        public List<Power> GetModelListByCache(string where)
        {
            if (string.IsNullOrWhiteSpace(where))
            {
                 throw new Exception("where 不能为null或空字符串");
            }
            string cacheKey = "PowerList-"+where;
            object obj = CacheHelper.GetCache(cacheKey);
            if (obj == null)
            {
                obj = this.GetModelList(where);
                if (obj != null)
                {
                    int cacheValidTime = ConfigHelper.GetConfigInt("CacheValidTime");
                    CacheHelper.SetCache(cacheKey,
                           obj, DateTime.Now.AddSeconds(cacheValidTime), TimeSpan.Zero);
                }
            }
            return (List<Power>)obj;
        }
 public List<Power> GetModelList(string orderBy, string where, long startRowNum, long endRowNum)
{
return new PowerDAL().GetModelList(orderBy, where, startRowNum, endRowNum);
}

 public Int64 GetRecordCount(string where)
{
return new PowerDAL().GetRecordCount(where);
}

public Power DataRowToModel(DataRow row)
{
return new PowerDAL().DataRowToModel(row);
}

}
}
