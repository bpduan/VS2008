using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostGet.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string psw { get; set; }
        public int page = 1;
        public int pageSize = 25;
        public int id = 0;
        public int Age = 0;
        public DateTime? UpdateTime;
    }
}
