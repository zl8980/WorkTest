using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace TaoBao.DAL
{
    public static class DBHelper
    {
        private static string ConnStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 执行添加、删除、修改的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] paras)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                try
                {
                    
                    conn.Open();                                    //打开数据库连接                    
                    SqlCommand command = new SqlCommand(sql, conn); //创建执行脚本的对象
                    command.Parameters.AddRange(paras);
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //写控制台日志  文档日志IO 业务日志
                    Console.WriteLine(ex.Message);
                }
                return result;
            }
        }

        /// <summary>
        /// 执行多条Sql语句  添加、删除、修改的方法
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string[] sqls, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                int count = 0;
                try
                {
                    conn.Open();                            //打开数据库连接
                    SqlCommand command = new SqlCommand();  //创建执行脚本的对象
                    command.Parameters.AddRange(paras);
                    for (int i = 0; i < sqls.Length; i++)
                    {
                        command.CommandText = sqls[i];
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            count++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //写控制台日志  文档日志IO 业务日志
                    Console.WriteLine(ex.Message);
                }               
                return count == sqls.Length;
            }
        }


        /// <summary>
        /// 执行SQL并返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)
        {
            object obj = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    obj = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //写控制台日志  文档日志IO 业务日志
                    Console.WriteLine(ex.Message);
                }               
                return obj;
            }
        }


        /// <summary>
        /// 执行SQL返回SqlDataReader对象       连接式读取方式 带异常处理的释放
        ///  //SqlDataReader 不保存数据 一定要和数据库保持连接状态
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] paras)
        {
            SqlDataReader reader = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                try
                {
                    conn.Open();                                    //打开链接
                    SqlCommand command = new SqlCommand(sql, conn); //执行脚本的对象
                    command.Parameters.AddRange(paras);                                 
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);//如果关闭reader 的时候   那么command相关的conn也会关闭
                }
                catch (Exception ex)
                {
                    //写控制台日志  文档日志IO 业务日志
                    Console.WriteLine(ex.Message);
                }
            }
            return reader;
        }


        #region//断开式连接方式
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, params SqlParameter[] paras)
        {
            DataSet ds = null;//数据集            
            using (SqlConnection conn = new SqlConnection(ConnStr))  //没有手动的打开conn.Open
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);//SqlDataAdapter 数据适配器----》数据库连接 自动打开   
                ds = new DataSet();                                  //创建一个 临时数据集
                adapter.Fill(ds);                                    //数据适配器Fill后         ----》数据库连接 自动关闭
            }
            return ds;
        }
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 获取数据表中的一条文档
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(string sql, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }
        #endregion
    }
}
