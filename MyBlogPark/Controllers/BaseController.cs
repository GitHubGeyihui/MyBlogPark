﻿using MyBGO.Framework.Core;
using MyBGO.Framework.Models;
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
        protected Catalog LoginCatalog
        {
            get
            {
                return Session["loginCatalog"] as Catalog;
            }
        }

        protected DottextCount dbContext = new DottextCount();//封装EF的使用

    }
}