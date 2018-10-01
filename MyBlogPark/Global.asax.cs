using AutoMapper;
using MyBGO.Framework.Core;
using MyBGO.Framework.Models;
using MyBlogPark.Areas.Admin.ViewModels;
using MyBlogPark.Core;
using MyBlogPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
 
namespace MyBlogPark
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            EFInit.Setting();
            //配置AutoMap 
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CatalogAdd, Catalog>();
                cfg.CreateMap<BlogApply, Blog>();
                cfg.CreateMap<ArticleAdd, Article>();
            }
            );
        }
    }
}
