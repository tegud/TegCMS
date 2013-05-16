using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace TegCMS.Pages.Models
{
    public class CmsRouteCollection
    {
        private readonly IEnumerable<CmsRoute> _routes;

        public CmsRouteCollection(IEnumerable<CmsRoute> cmsRoutes)
        {
            _routes = cmsRoutes;
        }

        public IEnumerable<CmsRoute> Routes
        {
            get { return _routes; }
        }

        public void AddRoutesToRouteTable(RouteCollection routeCollection)
        {
            foreach (var cmsRoute in _routes)
            {
                var routeValueDictionary = new RouteValueDictionary { { "__RouteName", cmsRoute.Name } };
                var defaults = new RouteValueDictionary
                    {
                        { "controller", "Page" },
                        { "action", "Index" }
                    };

                var route = new Route(cmsRoute.Url, defaults, new RouteValueDictionary(), routeValueDictionary,
                                      new MvcRouteHandler());

                routeCollection.Add(route);
            }
        }
    }

    public class CmsRoute
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}