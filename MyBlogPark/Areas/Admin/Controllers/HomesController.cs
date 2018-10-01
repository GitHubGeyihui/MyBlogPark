using AutoMapper;
using MyBGO.Framework.Models;
using MyBlogPark.Areas.Admin.ViewModels;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class HomesController : AdminBaseController
    {
        // GET: Admin/Homes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Apply()
        {
            return View();
        }
        /// <summary>
        /// 添加一个扩展方法来使用AutoMapper
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Apply(BlogApply info)
        {
            if (ModelState.IsValid)
            {
               var model= Mapper.Map<Blog>(info);
                model.AddTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                model.Status = true;
                model.UserID = LoginUser.ID;
                dbContext.blog.Add(model);
                int res = dbContext.SaveChanges();
                //ToDo: 这里放到事务里面（这里有些不懂的，明天早上再来看看{已经解决}）
                if (res > 0)
                {
                    LoginUser.BlogID = model.ID;//取到ID
                    //将要修改的实体附加到上下文中
                    dbContext.user.Attach(LoginUser);
                    //修改实体的状态，改为“修改”状态
                    dbContext.Entry<User>(LoginUser).State = System.Data.Entity.EntityState.Modified;
                    res = dbContext.SaveChanges();

                    Session["LoginBlog"] = model;
                    return Content("申请成功"); 
                } 
            }
            return View(info);
        }

    }
}