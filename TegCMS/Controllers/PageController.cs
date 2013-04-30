using System.Web.Mvc;
using System.Web.Routing;
using TegCMS.Pages;
using TegCMS.Utilities;

namespace TegCMS.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageModelFactory _pageModelFactory;
        private readonly IMvcRouteDataFactory _routeDataFactory;

        public PageController(IPageModelFactory pageModelFactory, IMvcRouteDataFactory routeDataFactory)
        {
            _pageModelFactory = pageModelFactory;
            _routeDataFactory = routeDataFactory;
        }

        public ViewResult Index()
        {
            var mvcRouteData = _routeDataFactory.Build(RouteData);

            var pageModel = _pageModelFactory.Build(mvcRouteData.GetRouteName());

            return View(pageModel.ViewName, pageModel.ViewModel);
        }
    }
}