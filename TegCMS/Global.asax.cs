using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace TegCMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            var razorEngine = new RazorViewEngine
            {
                AreaPartialViewLocationFormats = new[] { "~/Modules/{2}/Views/{1}/{0}.cshtml" },
                AreaViewLocationFormats = new[] { "~/Layouts/{2}/{1}/{0}.cshtml" },
                AreaMasterLocationFormats = new[] { "~/Layouts/{2}/{1}/{0}.cshtml" }
            };

            ViewEngines.Engines.Add(razorEngine);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}