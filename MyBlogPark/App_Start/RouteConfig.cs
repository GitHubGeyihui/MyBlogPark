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

            routes.MapRoute(
              name: "Blog",
              url: "{blog}",
              defaults: new { controller = "Blog", action = "Index" }
          );//博客的路由

            routes.MapRoute(
            name: "BlogArticle",
            url: "{blog}/p/{id}.html",
            defaults: new { controller = "Blog", action = "Article" },
            constraints: new { id="\\d+"}
        );//博文的路由

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
