using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace VIP_14
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test5();
            AddUser();
            Console.ReadKey();
        }


        public static void AddClassInfo()
        {
            string username = "admin";
            string password = "123456";
            string sql = @"select userid,username from userinfos where username = @username and passwork =@passwork";
            //参数化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@username",username),
                new SqlParameter("@password",password)
            };//初始化器

            object obj = DBHelper.ExecuteScalar(sql, paras);


        }


        public static void AddUser()
        {
            string sql = @"insert into Students(stuNo,stuName,sex,remark,birthday,createtime,age)
values(1004,'wangliu',NULL,NULL,NULL,NULL,NULL)";
            int result = DBHelper.ExecuteNonQuery(sql);
            if (result > 0)
            {
                Console.WriteLine("添加成功！！");
            }
            else
            {
                Console.WriteLine("添加失败！！");
            }
        }

        public static void Test6()
        {
            string connStr = "server=.;uid=sa;pwd=sa;database=ZLSchoolDB";//不区分大小写
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                Console.WriteLine("打开成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                conn.Close();
                Console.WriteLine("释放资源成功！");
            }
        }
        /// <summary>
        /// 查询返回 多行 多列
        /// </summary>
        public static void Test5_1()
        {
            List<ClassInfos> ClassInfoList = new List<ClassInfos>();
            string sql = @"select classid,classname,remark from classinfos";
            using (SqlDataReader reader = DBHelper.ExecuteReader(sql))
            {
                //如果是多条数据 用while
                while (reader.Read())
                {
                    ClassInfos classInfos = new ClassInfos();
                    classInfos.ClassId = (int)reader["calassid"];
                    classInfos.ClassName = reader["classname"].ToString();
                    classInfos.Remark = reader["remark"].ToString();
                    ClassInfoList.Add(classInfos);
                }
            }
        }

        /// <summary>
        /// 多行多列
        /// </summary>
        public static void Test5()
        {
            List<ClassInfos> ClassInfoList = new List<ClassInfos>();

            string connStr = "server=.;uid=sa;pwd=sa;database=ZLSchoolDB";//不区分大小写
            //步骤一:创建数据库连接对象
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //不论程序操作有多复杂， （添加  删除  修改）     查询
                //步骤二 :  打开
                conn.Open();
                //步骤三:创建执行脚本对象  第一个参数:cmdText  支持存储过程，支持SQL语句,支持表名

                string sql = @"select classid,classname,remark from classinfos";
                SqlCommand command = new SqlCommand(sql, conn);

                //步骤四:返回多行多列  返回的是个游标  
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    //如果是多条数据 用while
                    while (reader.Read())
                    {
                        ClassInfos classInfos = new ClassInfos();
                        classInfos.ClassId = (int)reader["calassid"];
                        classInfos.ClassName = reader["classname"].ToString();
                        classInfos.Remark = reader["remark"].ToString();
                        ClassInfoList.Add(classInfos);
                    }

                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 查询返回 1行 多列
        /// </summary>
        public static void Test4_1()
        {
            string username = "admin";
            string password = "123456";
            string sql = @"select userid,username from userinfos where username = @username and passwork =@passwork";
            //参数化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@username",username),
                new SqlParameter("@password",password)
            };//初始化器

            using (SqlDataReader reader = DBHelper.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    UserInfos userInfos = new UserInfos();
                    userInfos.UserId = (int)reader["userid"];
                    userInfos.UserName = (string)reader["username"].ToString();
                }
            }
        }


        /// <summary>
        /// 查询返回 1行 多列
        /// </summary>
        public static void Test4()
        {
            string connStr = "server=.;uid=sa;pwd=sa;database=ZLSchoolDB";//不区分大小写
            //步骤一:创建数据库连接对象
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //不论程序操作有多复杂， （添加  删除  修改）     查询
                //步骤二 :  打开
                conn.Open();
                //步骤三:创建执行脚本对象  第一个参数:cmdText  支持存储过程，支持SQL语句,支持表名

                string sql = @"select userid,username from userinfos where username = 'admin' and passwork =123456";
                SqlCommand command = new SqlCommand(sql, conn);

                //步骤四:返回多行多列  返回的是个游标  
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    //如果是多条数据  用while
                    //while (reader.Read())
                    //{ 

                    //}
                    //如果是一条数据  用if
                    if (reader.Read())
                    {
                        UserInfos userInfos = new UserInfos();
                        userInfos.UserId = (int)reader["userid"];
                        userInfos.UserName = (string)reader["username"].ToString();
                    }
                }
            }
            Console.ReadKey();
        }



        /// <summary>
        /// 查询返回 第一行 第一列
        /// </summary>
        public static void Test3()
        {
            string connStr = "server=.;uid=sa;pwd=sa;database=ZLSchoolDB";//不区分大小写
            //步骤一:创建数据库连接对象
            SqlConnection conn = new SqlConnection(connStr);
            //不论程序操作有多复杂， （添加  删除  修改）     查询
            //步骤二 :  打开
            conn.Open();
            //步骤三:创建执行脚本对象  第一个参数:cmdText  支持存储过程，支持SQL语句,支持表名
            string sql = @"select count(1) from Students";
            //string sql1 = @"select count(1) from userinfos where username = 'admin' and passwork =123456";
            SqlCommand command = new SqlCommand(sql, conn);

            //步骤四:返回 第一行 第一列  其他的行和列 都会忽略掉 所以类型的基类都是object
            object obj = command.ExecuteScalar();
            if (obj != null)
            {
                int result = (int)obj;
                if (result > 0)
                {
                    Console.WriteLine("登录成功！！");
                }
                else
                {
                    Console.WriteLine("登录失败！！");
                }
            }
            //第五步:关闭数据库
            conn.Close();
            Console.ReadKey();

        }

        /// <summary>
        /// （添加  删除  修改）  
        /// </summary>

        public static void Test2()
        {
            string connStr = "server=.;uid=sa;pwd=sa;database=ZLSchoolDB";//不区分大小写
            //步骤一:创建数据库连接对象
            SqlConnection conn = new SqlConnection(connStr);
            //不论程序操作有多复杂， （添加  删除  修改）     查询
            //步骤二 :  打开
            conn.Open();
            //步骤三:创建执行脚本对象  第一个参数:cmdText  支持存储过程，支持SQL语句,支持表名
            string sql = @"insert into Students(stuNo,stuName,sex,remark,birthday,createtime,age)
values(1003,'wangliu',NULL,NULL,NULL,NULL,NULL)";
            string sql1 = @"delete Students
where stuNo = 1002";
            SqlCommand command = new SqlCommand(sql, conn);

            //步骤四:执行返回受影响行数
            int result = command.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("添加成功！！");

            }
            else
            {
                Console.WriteLine("添加失败！！");
            }
            //第五步:关闭数据库
            conn.Close();
            Console.ReadKey();

        }

        public static void Test1()
        {
            //访问数据库的步骤
            //步骤一:创建数据库连接对象
            string connString = "Data Source=.;Initial Catalog = ZLSchoolDB;User ID=sa;pwd=sa";
            //sever=.;uid=sa;pwd=sa;databse=ZLSchoolDB;
            string connStr = "server=.;uid=sa;pwd=sa;database=ZLSchoolDB";//不区分大小写
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();//打开数据库 引发异常
            Console.WriteLine("链接数据库成功！");


            conn.Close();//关闭数据库
            Console.WriteLine("关闭数据库成功！");
            Console.ReadKey();
        }
    }



}
