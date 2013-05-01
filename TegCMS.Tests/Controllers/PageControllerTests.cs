using System;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using TegCMS.Controllers;
using TegCMS.Pages.Models;
using TegCMS.Utilities;

namespace TegCMS.Tests.Controllers
{
    [TestFixture]
    public class PageControllerTests : IPageModelFactory, IMvcRouteDataFactory, IMvcRequestFactory
    {
        private const string LOCALHOST = "http://localhost";
        private string _expectedViewName = "expectedView";
        private object _expectedViewModel;
        private RouteData _routeData;
        private string _url;
        private string _expectedAreaName;

        [Test]
        public void IndexReturnsSpecifiedLayout()
        {
            _expectedViewName = "expectedView";
            _routeData = new RouteData();
            _url = LOCALHOST;

            var result = new PageController(this, this, this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        [Test]
        public void IndexReturnsDifferentLayoutForADifferentRouteName()
        {
            _expectedViewName = "differentView";

            _routeData = new RouteData();
            _routeData.DataTokens.Add("__RouteName", "ADifferentRouteName");
            _url = LOCALHOST;

            var result = new PageController(this, this, this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        [Test]
        public void IndexReturnsDifferentLayoutForADifferentHostName()
        {
            _expectedViewName = "differentLayoutForADifferentHostName";

            _routeData = new RouteData();
            _url = "http://www.tegud.net";

            var result = new PageController(this, this, this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        [Test]
        public void IndexReturnsSpecifiedViewModel()
        {
            _expectedViewModel = new Object();

            _url = LOCALHOST;
            _routeData = new RouteData();

            var result = new PageController(this, this, this).Index();

            Assert.That(result.ViewData.Model, Is.EqualTo(_expectedViewModel));
        }

        [Test]
        public void IndexSetsSiteNameOnMvcRouteData()
        {
            _url = LOCALHOST;
            _routeData = new RouteData();
            _expectedAreaName = "tegud";

            new PageController(this, this, this).Index();

            Assert.That(_routeData.DataTokens["area"], Is.EqualTo(_expectedAreaName));
        }

        public PageModel Build(string routeName, string hostName)
        {
            string viewName;
            if (routeName == "ADifferentRouteName")
                viewName = "differentView";
            else
                viewName = "expectedView";

            if (hostName == "www.tegud.net")
                viewName = "differentLayoutForADifferentHostName";

            return new PageModel
                {
                    ViewName = viewName,
                    ViewModel = _expectedViewModel,
                    AreaName = _expectedAreaName
                };
        }

        public MvcRouteData Build(RouteData routeData)
        {
            return new MvcRouteData(_routeData);
        }

        public MvcRequest Build(HttpRequestBase httpContext)
        {
            return new MvcRequest(new HttpRequestWrapper(new HttpRequest(string.Empty, _url, string.Empty)));
        }
    }
}
