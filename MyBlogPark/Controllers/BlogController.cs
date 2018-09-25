﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Blog    
        public ActionResult Index(string blog)
        {
            // ViewBag.BlogName = blog;//传递数据的动态类型
            //在博客前台首页这里读取博客的实体，根据博客的标识符在前台呈现
            var model = dbContext.blog.Where(m => m.Identity.ToLower() == blog.ToLower()).FirstOrDefault();
            if (model == null)
            {
                return Content("用户不存在");
            }
            return View(model);
        }
    }
}