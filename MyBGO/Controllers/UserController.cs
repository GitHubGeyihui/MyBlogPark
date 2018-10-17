using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBGO.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index(int p=1)
        {
            ViewBag.ListUser = dbContext.User.OrderBy(m => m.ID).Skip((p - 1) * 10).Take(10).ToList();
            return View();
        }
    }
} 