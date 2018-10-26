using MyBGO.Framework.MyModels;
using MyBlogPark.Areas.Admin.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class SettingController : AdminBaseController
    {
        // GET: Admin/Setting
        public ActionResult Info()
        {
            var info = new UserInfo
            {
                Email = LoginUser.Email
            };
            return View(info);
        }
        [HttpPost]
        public ActionResult Info(UserInfo info)
        {
            LoginUser.Email = info.Email;
            dbContext.User.Add(LoginUser);
            dbContext.Entry(LoginUser).State = System.Data.Entity.EntityState.Modified;
            int res = dbContext.SaveChanges();
            if (res > 0)
            {
                return Redirect("/Admin/Setting/info");
            }
            else
            {
                return Content("保存失败");
            }
        }
        public ActionResult Password()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Password(UserPassword info)
        {
            if (ModelState.IsValid)
            {
                if (info.Pwd != LoginUser.Pwd)
                {
                    ModelState.AddModelError("Pwd","密码错误!");
                }
                var u = dbContext.User.Find(LoginUser.ID);
                u.Pwd = info.NewPwd;
                
                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    Session["loginUser"] = null;
                    Session["loginBlog"] = null;
                    Session.Abandon();//取消当前会话
                    Session.RemoveAll();
                    Session.Clear();
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Pwd","保存失败");
                }
            }
            return View(info);
        }

        public ActionResult Config()
        {
            var model = dbContext.Blog.FirstOrDefault(m => m.ID == LoginBlog.ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Config(Blog info)
        {
            var model = dbContext.Blog.FirstOrDefault(m => m.ID == LoginBlog.ID);
            model.NiName = info.NiName;
            model.Title = info.Title;
            model.Signal = info.Signal;
            model.Discription = info.Discription;
            model.ThemeID = info.ThemeID;
            dbContext.Blog.Add(model);
            dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            int res = dbContext.SaveChanges();
            if (res > 0)
            {
                return Redirect("/Admin/Setting/Config");
            }
            else
            {
                return Content("保存失败");
            }

        }

    }
}