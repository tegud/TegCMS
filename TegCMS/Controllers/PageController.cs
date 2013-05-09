using System.Web;
using System.Web.Mvc;
using TegCMS.Pages.Data;
using TegCMS.Pages.Models;
using TegCMS.Utilities;

namespace TegCMS.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageModelFactory _pageModelFactory;
        private readonly IMvcRouteDataFactory _routeDataFactory;
        private readonly IMvcRequestFactory _mvcRequestFactory;

        public PageController(IPageModelFactory pageModelFactory, 
            IMvcRouteDataFactory routeDataFactory,
            IMvcRequestFactory mvcRequestFactory)
        {
            _pageModelFactory = pageModelFactory;
            _routeDataFactory = routeDataFactory;
            _mvcRequestFactory = mvcRequestFactory;
        }

        public PageController()
        {
            _pageModelFactory = new PageModelFactory(MvcApplication.PageRepository);
            _routeDataFactory = new MvcRouteDataFactory();
            _mvcRequestFactory = new MvcRequestFactory();
        }

        public ViewResult Index()
        {
            var mvcRouteData = _routeDataFactory.Build(RouteData);
            HttpRequestBase httpRequestBase = null;

            if(HttpContext != null)
            {
                httpRequestBase = HttpContext.Request;
            }

            var mvcRequest = _mvcRequestFactory.Build(httpRequestBase);

            var pageModel = _pageModelFactory.Build(mvcRouteData.GetRouteName(), mvcRequest.GetUrlHostName());

            mvcRouteData.SetAreaName(pageModel.AreaName);

            return View(pageModel.ViewName, pageModel.ViewModel);
        }
    }
}