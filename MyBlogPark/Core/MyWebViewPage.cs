using MyBGO.Framework.Core;
using MyBGO.Framework.Models;
using MyBlogPark.Core;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
 
namespace MyBlogPark.Core
{
    public abstract class MyWebViewPage<T> : WebViewPage<T>
    {

        protected User LoginUser
        { 
            get
            {
                return Session["loginUser"] as User;
            }
        }
        protected DottextCount dbContext = new DottextCount();//封装EF的使用
        protected Blog LoginBlog
        {
            get
            {
                return Session["loginBlog"] as Blog;
            }
        }
 

    }
}