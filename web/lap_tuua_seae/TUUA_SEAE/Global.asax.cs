using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Serilog;

namespace TUUA_SEAE
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Web.Helpers.AntiForgeryConfig.SuppressXFrameOptionsHeader = true;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string rutaLog = ConfigurationManager.AppSettings["rutaLog"];
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"{rutaLog}/TUUA_SEAE_LOG-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
    
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            
        }
    }
}
