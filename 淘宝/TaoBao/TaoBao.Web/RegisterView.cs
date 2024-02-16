using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaoBao.BLL;
using TaoBao.Models;

namespace TaoBao.Web
{
    public class RegisterView
    {
        public void Register()
        {
            UserInfoModel userInfoModel = new UserInfoModel();
            UserInfoManager userInfoManager = new UserInfoManager();

            Console.WriteLine("请输入账号:");
            userInfoModel.UserName = Console.ReadLine();
            Console.WriteLine("请输入密码:");
            userInfoModel.Password = Console.ReadLine();


            if (userInfoManager.Register(userInfoModel))
            {
                Console.WriteLine("注册成功!");
            }
            else
            {
                Console.WriteLine("注册失败!");
            }
        }
    }
}
