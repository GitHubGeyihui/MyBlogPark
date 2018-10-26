using AutoMapper;
using MyBGO.Framework.MyModels;
using MyBGO.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyBGO
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
                cfg.CreateMap<CatalogAdd, WebCatalog>();
            }
          );
        }

    } 
}

