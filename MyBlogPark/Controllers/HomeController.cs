using MyBlogPark.Core;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegister info)
        {
            if (ModelState.IsValid)
            {
                var user = new User {
                    Name = info.Name,
                    Pwd = info.Pwd,
                    Email = info.Email,
                    IP = "127.0.0.1",
                    AddTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    LoginTimes = 1,
                    BlogID = 0,
                    LastLoginTime = DateTime.Now,
                    Status = true
                };
                dbContext.user.Add(user);
                var res = dbContext.SaveChanges();
                if (res > 0)
                {
                    //浏览器-（cookie）-》服务器（去查找cookie对应的session）-cookie对应的session可能有多个，所以通过key来取得
                    Session["loginUser"] = user;
                    return Redirect("/");              
                }
                else
                {
                    return Content("注册失败");
                }
            }
            return View(info);
        }
        public ActionResult Login ()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin info)
        {
            if (ModelState.IsValid)
            {
                var dbuser = dbContext.user.Where(m => m.Name.ToLower() == info.Name.ToLower()).FirstOrDefault();
                if(dbuser!=null && dbuser.Pwd==info.Pwd)
                {
                    Session["loginUser"] = dbuser;
                    //把当前用户的博客记录起来
                  var blog =  dbContext.blog.Where(m => m.UserID == dbuser.ID && m.Status).FirstOrDefault();
                    if (blog != null)
                    {
                        Session["loginBlog"] = blog;
                    }
                    return Redirect("/"); 
                }
            }
            return View(info);
        }
    }
}