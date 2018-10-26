using MyBGO.Framework.MyModels;
using MyBlogPark.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogPark.Areas.Admin.Controllers
{
    public class AdminBaseController : BaseController //用户后台的基类控制器
    { // 摘要:
        //     在执行由操作方法返回的操作结果前调用。
        //
        // 参数:
        //   filterContext:
        //     有关当前请求和操作结果的信息。
        ///protected virtual void OnResultExecuting(ResultExecutingContext filterContext); 
        ///----重写这个事件，将事件中所有action执行之前先执行的代码
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (LoginUser==null)
            {
                filterContext.HttpContext.Response.Redirect("/");
            }
            //后台必须先登录才能访问，若未登录则跳回首页
            
            
        }
   
        public new DB_MyBlogEntities3 dbContext = new DB_MyBlogEntities3();
    }
}