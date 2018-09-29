using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class SettingController : AdminBaseController
    {
        // GET: Admin/Setting
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Password()
        {
            return View();
        }
    }
}