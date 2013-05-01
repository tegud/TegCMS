using System.Web.Routing;

namespace TegCMS.Utilities
{
    public class MvcRouteData
    {
        private readonly RouteData _routeData;

        public MvcRouteData(RouteData routeData)
        {
            _routeData = routeData;
        }

        public string GetRouteName()
        {
            if (_routeData.DataTokens.ContainsKey("__RouteName"))
                return _routeData.DataTokens["__RouteName"] as string;
            
            return string.Empty;
        }

        public void SetAreaName(string areaName)
        {
            _routeData.DataTokens.Add("area", areaName);
        }
    }
}