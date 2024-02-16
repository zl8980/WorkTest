using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaoBao.Models;

namespace TaoBao.DAL
{
    public class UserInfoService
    {
        public bool Register(UserInfoModel model)
        {
            string sql = @"insert into UserInfos( UserName, Password)
                        values( @UserName, @Password)";
            SqlParameter[] paras =
            {
                new SqlParameter("@UserName",model.UserName),
                new SqlParameter("@Password", model.PassWord)
            };
            int result = DBHelper.ExecuteNonQuery(sql, paras);
            return result > 0;
        }


        /// <summary>
        /// 登录功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserInfoModel Login(UserInfoModel model)
        {
            UserInfoModel userInfoModel = null;
            string sql = @"select UserName, Password from UserInfos
                           where UserName=@UserName and Password=@Password";
            SqlParameter[] paras =
                {
                    new SqlParameter("@UserName",model.UserName),
                    new SqlParameter("@Password",model.Password)
                };
            DataRow row = DBHelper.GetDataRow(sql, paras);
            if (row != null)
            {
                userInfoModel = new UserInfoModel();
                userInfoModel.UserName = row["UserName"].ToString();
                userInfoModel.Password = row["Password"].ToString();
            }
            return userInfoModel;//true  false
        }



        public DataSet GetUserInfos()
        {
            string sql = @"select UserId, UserName, Password from UserInfos
                            select stuNo, stuName, classId, sex, telephone, birthday, createtime
                            from Students";
            DataSet ds = DBHelper.GetDataSet(sql);
            return ds;
        }
    }
}
