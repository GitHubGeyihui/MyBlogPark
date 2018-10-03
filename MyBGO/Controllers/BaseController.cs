using MyBGO.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBGO.Controllers
{
    public class BaseController : Controller
    {
        protected DottextCount dbContext = new DottextCount();//封装EF的使用
        // GET: Base
        public ActionResult Index()
        {

            return View();
        }
    }
}
