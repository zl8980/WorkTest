using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaoBao.Models
{
    public class UserInfoModel
    {
        public int UserNo { get; set; }//自动增长
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CardID { get; set; }
    }
}
