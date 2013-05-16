using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TegCMS.ModelBinding;
using TegCMS.Pages.Data;
using TegCMS.Pages.Data.Json;

namespace TegCMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        public static IPageRepository PageRepository { get; set; }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            var razorEngine = new RazorViewEngine
            {
                AreaPartialViewLocationFormats = new[] { "~/Modules/{2}/Views/{1}/{0}.cshtml" },
                AreaViewLocationFormats = new[] { "~/Layouts/{2}/{0}.cshtml" },
                AreaMasterLocationFormats = new[] { "~/Layouts/{2}/{0}.cshtml" }
            };

            ViewEngines.Engines.Add(razorEngine);

            var sitesFile = HostingEnvironment.MapPath("~/sites.json");
            PageRepository = new JsonPageRepository(sitesFile);

            AreaRegistration.RegisterAllAreas();

            PageRepository.GetAllRoutes().AddRoutesToRouteTable(RouteTable.Routes);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinderProviders.BinderProviders.Add(new ChildActionJsonModelBinderProvider());
        }
    }
}