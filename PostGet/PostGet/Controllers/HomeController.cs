using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYHelper;

namespace PostGet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(
            int? page
            )
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to ASP.NET MVC!";


            int currentPage = page.HasValue ? page.Value : 1;

            ViewData["page"] = ReflectionHelper.PageHtml(currentPage, "Index","Home", Request);

            return View();
        }

        public class JsonData
        {
            public bool success { get; set; }
            public string msg { get; set; }
            public object data { get; set; }
        }



        public JsonResult DoAjax(
            string psw,
            string Name,
            int? Age,
            DateTime? UpdateTime

            )
        {
            UpdateTime = DateTime.Now;
            int f= Request.Form.Count;
            int r= Request.QueryString.Count;

            string psw1 = Request.Form["psw"];
            string psw2 = Request.QueryString["psw"];
            string psw3 = Request["psw"];
            string psw4 = psw;

            PostGet.Models.UserInfo model=new  PostGet.Models.UserInfo();
            ReflectionHelper.UpdateModel(model, Request);


            string[] keyQ = Request.QueryString.AllKeys;
            string[] keyf = Request.Form.AllKeys;

            List<string> Keys = new List<string>();
            Keys.AddRange(Request.QueryString.AllKeys);
            Keys.AddRange(Request.Form.AllKeys);
            Keys = Keys.Distinct().ToList();

            string rurl=  Request.RawUrl;

            

            return Json(new JsonData { msg="来自后台"});
        }


        public ActionResult HelloWord()
        {

            return View();
        }


        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }
    }
}
