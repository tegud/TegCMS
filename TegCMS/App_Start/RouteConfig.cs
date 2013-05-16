using System.Web.Mvc;
using System.Web.Routing;

namespace TegCMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Component",
                url: "{controller}/{action}"
            );
        }
    }
}