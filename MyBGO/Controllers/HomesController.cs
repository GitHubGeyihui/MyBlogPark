using System.Linq;
using System.Web.Mvc;

using MyBGO.Models;

namespace MyBGO.Controllers
{
    public class HomesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminUserLogin info)
        {
            if (ModelState.IsValid)
            {
                var dbAdminUser = dbContext.Admin.Where(m => m.Name.ToLower() == info.Name.ToLower()).FirstOrDefault();
                if (dbAdminUser != null && dbAdminUser.Pwd == info.Pwd)
                {
                    Session["AdminloginUser"] = dbAdminUser;
                    return Redirect("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Name", "账号或密码不正确！");
            }


            return View(info);

        }

        public ActionResult LogOff()
        {
            Session["AdminLoginUser"] = null;
            Session.Abandon();//取消当前会话
            Session.RemoveAll();
            Session.Clear();
            return Redirect("homes/index");
        }
    }
}