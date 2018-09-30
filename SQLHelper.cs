using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace 大龙的代码生成器
{
    internal class SQLHelper
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public static SqlConnection GetConAndOpen()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            return con;
        }

        public static SqlConnection GetConAndOpen(string conStr)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            return con;
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameterArray)
        {
            using (SqlConnection con = GetConAndOpen())
            {
                return ExecuteNonQuery(con, cmdText, parameterArray);
            }
        }

        public static int ExecuteNonQuery(SqlConnection con, string cmdText, params SqlParameter[] parameterArray)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            cmd.Parameters.AddRange(parameterArray);
            return cmd.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(SqlConnection con, SqlTransaction tran, string cmdText, params SqlParameter[] parameterArray)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            cmd.Parameters.AddRange(parameterArray);
            cmd.Transaction = tran;
            return cmd.ExecuteNonQuery();
        }

        public static object ExecuteScalar(string cmdText, params SqlParameter[] parameterArray)
        {
            using (SqlConnection con = GetConAndOpen())
            {
                return ExecuteScalar(con, cmdText, parameterArray);
            }
        }

        public static object ExecuteScalar(SqlConnection con, string cmdText, params SqlParameter[] parameterArray)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            cmd.Parameters.AddRange(parameterArray);
            return cmd.ExecuteScalar();
        }

        public static object ExecuteScalar(SqlConnection con, SqlTransaction tran, string cmdText, params SqlParameter[] parameterArray)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            cmd.Parameters.AddRange(parameterArray);
            cmd.Transaction = tran;
            return cmd.ExecuteScalar();
        }

        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameterArray)
        {
            using (SqlConnection con = GetConAndOpen())
            {
                return ExecuteDataTable(con, cmdText, parameterArray);
            }
        }

        public static DataTable ExecuteDataTable(string conStr, string cmdText, params SqlParameter[] parameterArray)
        {
            using (SqlConnection con = GetConAndOpen(conStr))
            {
                return ExecuteDataTable(con, cmdText, parameterArray);
            }
        }

        public static DataTable ExecuteDataTable(SqlConnection con, string cmdText, params SqlParameter[] parameterArray)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            cmd.Parameters.AddRange(parameterArray);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                table.Load(reader);
            }
            return table;
        }
    }
}