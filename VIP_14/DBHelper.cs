using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace VIP_14
{
    /// <summary>
    /// 数据库帮助类 (工具类)
    /// </summary>
    public static class DBHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;


        /// <summary>
        /// 增 删 改 方法 
        /// </summary>
        /// <param name="sql">SQL脚本</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] paras)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();//打开链接
                    SqlCommand command = new SqlCommand(sql, conn);//执行脚本的对象
                    command.Parameters.AddRange(paras);
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //写控制台日志
                    Console.WriteLine(ex.Message);
                    //文档日志IO
                    //业务日志
                }
            }
            //using 原理类似 Try Catch Finally
            return result;
        }

        /// <summary>
        /// 查询返回 第一行 第一列的 方法
        /// </summary>
        /// <param name="sql">SQL脚本</param>
        /// <param name="paras">默认值 是一个空数组 </param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();//打开链接
                    SqlCommand command = new SqlCommand(sql, conn);//执行脚本的对象
                    command.Parameters.AddRange(paras);
                    result = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //写控制台日志
                    Console.WriteLine(ex.Message);
                    //文档日志IO
                    //业务日志
                }
            }
            //using 原理类似 Try Catch Finally
            return result;
        }

        /// <summary>
        /// 查询返回 SqlDataReader 指向表数据的 读索引器
        /// </summary>
        /// <param name="sql">SQL脚本</param>
        /// <param name="paras">默认值 是一个空数组 </param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] paras)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(connStr);


            try
            {
                conn.Open();//打开链接
                SqlCommand command = new SqlCommand(sql, conn);//执行脚本的对象
                command.Parameters.AddRange(paras);
                //如果关闭reader 的时候   那么 command 相关的conn也会关闭
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //写控制台日志
                Console.WriteLine(ex.Message);
                //文档日志IO
                //业务日志
            }
            return reader;
        }
    }
}
