using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Reflection.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserInfo user = new UserInfo();
            user.Name = "段胜利";
            user.UpdateTime = DateTime.Now;
            user.Age = 38;
            user.aaa = "段胜利你好";

            string jsonStr = JsonConvert.SerializeObject(user);

            UserInfo2 user2 = JsonConvert.DeserializeObject<UserInfo2>(jsonStr);


            return View();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }


        public class UserInfo
        {
            public string Name { get; set; }
            public DateTime UpdateTime { get; set; }
            public int Age { get; set; }
            public string aaa;
        }

        public class UserInfo2
        {
            public string Name { get; set; }
            public DateTime UpdateTime { get; set; }
//            public int Age { get; set; }
            public string aaa;
            public string bbb;
        }
    }
}
