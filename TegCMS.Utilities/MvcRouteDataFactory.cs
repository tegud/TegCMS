using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace TegCMS.Utilities
{
    public interface IMvcRouteDataFactory
    {
        MvcRouteData Build(RouteData routeData);
    }

    public class MvcRouteDataFactory : IMvcRouteDataFactory
    {
        public MvcRouteData Build(RouteData routeData)
        {
            return new MvcRouteData(routeData);
        }
    }
}
