using MyBlogPark.Core;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
          protected User LoginUser
          {
            get
            {
                return Session["loginUser"] as User;
            }
          }

        protected Blog LoginBlog
        {
            get
            {
                return Session["loginBlog"] as Blog;
            }
        }
        protected DottextCount dbContext = new DottextCount();//封装EF的使用

    }
}