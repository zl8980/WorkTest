using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaoBao.DAL;
using TaoBao.Models;

namespace TaoBao.BLL
{
    /// <summary>
    /// 用户业务逻辑层
    /// </summary>
    public class UserInfoManager
    {
        private UserInfoService userInfoService = new UserInfoService();
        public bool Register(UserInfoModel model)
        {
            //if 特殊字符*#$
            //调用数据访问层?
            return userInfoService.Register(model);
        }

        public UserInfoModel Login(UserInfoModel model)
        {
            //if
            return userInfoService.Login(model);
        }

        public DataSet GetUserInfos()
        {
            return userInfoService.GetUserInfos();
        }
    }
}
