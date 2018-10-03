 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBGO.Controllers
{
    public class BlogArticleController : Controller
    {
        // GET: BlogArticle
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Load()
        {
            return Json(new { data =new object[]{ new { name = "谷羽", position = "广元", salary = 123, state_date = "asdfa", office = true, extn = "123" } } },JsonRequestBehavior.AllowGet);
        }
    }
}   