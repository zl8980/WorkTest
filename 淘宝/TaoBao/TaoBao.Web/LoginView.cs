using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaoBao.BLL;
using TaoBao.Models;

namespace TaoBao.Web
{
    public class LoginView
    {
        public void Login()
        {
            UserInfoModel userInfoModel = new UserInfoModel();
            UserInfoManager userInfoManager = new UserInfoManager();

            Console.WriteLine("请输入账号:");
            userInfoModel.UserName = Console.ReadLine();
            Console.WriteLine("请输入密码:");
            userInfoModel.Password = Console.ReadLine();
            UserInfoModel loginModel = userInfoManager.Login(userInfoModel);
            if (loginModel != null)
            {
                Console.WriteLine($"{loginModel.UserName},登录成功!");
            }
            else
            {
                Console.WriteLine("账号或密码错误!");
            }
        }
             
    }
}
