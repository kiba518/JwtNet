using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Jwt_Net
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register); 
            //webapiFilter
            System.Web.Http.GlobalConfiguration.Configuration.Filters.Add(new HttpPermissionFilter());
            System.Web.Http.GlobalConfiguration.Configuration.Filters.Add(new HttpExceptionFilter());
            //mvcFliter
            System.Web.Mvc.GlobalFilters.Filters.Add(new MvcExceptionFilter());
            System.Web.Mvc.GlobalFilters.Filters.Add(new MvcPermissionFilter());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
