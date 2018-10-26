using AutoMapper;
using MyBGO.Framework.MyModels;
using MyBlogPark.Areas.Admin.ViewModels;
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
