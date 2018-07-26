using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TwjDataBaseManage.Common
{
    public class SqlHelper
    {
        //数据库连接字符串
        public static readonly string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //打开数据库
        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            return conn;
        }

        //执行不返回结果的sql，用于插入和更新，删除
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                return ExecuteNonQuery(conn, cmdText, parameters);
            }
        }

        //上面方法的重载方法
        public static int ExecuteNonQuery(SqlConnection conn, string cmdText, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        //返回对象类型的sql查询，对象单个数据记录的中的某个值，或者count(*)计算的结果，在分页中会经常用到。
        public static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                return ExecuteScalar(conn, cmdText, parameters);
            }
        }

        //以上方法的重载方法
        public static object ExecuteScalar(SqlConnection conn, string cmdText, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        //获得数据表格的查询
        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                return ExecuteDataTable(conn, cmdText, parameters);
            }
        }

        //以上方法的重载
        public static DataTable ExecuteDataTable(SqlConnection conn, string cmdText, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}