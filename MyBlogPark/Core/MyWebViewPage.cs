using MyBGO.Framework.Core;
using MyBGO.Framework.Models;
using System.Web.Mvc;

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