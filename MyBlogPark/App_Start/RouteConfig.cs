using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBlogPark
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Blog",
                url: "{blog}",
                defaults: new { controller = "Blog", action = "Index", p = UrlParameter.Optional },
                constraints: new { blog = "\\w+" }
          );//博客的路由

            routes.MapRoute(
                 name: "BlogArticle",
                 url: "{blog}/p/{id}.html",
                 defaults: new { controller = "Blog", action = "Article" },
                 constraints: new { id = "\\d+" }
             );//博文的路由
            routes.MapRoute(
                 name: "BlogCatalog",
                 url: "{blog}/{id}",
                 defaults: new { controller = "Blog", action = "Catalog" },
                 constraints: new { id = "\\d+" }
             );//博文分类的路由

            // BotDetect requests must not be routed 验证码配置
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MyBlogPark.Controllers" }
            );
        }
    }
}
