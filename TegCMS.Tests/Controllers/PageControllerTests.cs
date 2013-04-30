using System;
using System.Web.Routing;
using NUnit.Framework;
using TegCMS.Controllers;
using TegCMS.Pages;
using TegCMS.Utilities;

namespace TegCMS.Tests.Controllers
{
    [TestFixture]
    public class PageControllerTests : IPageModelFactory, IMvcRouteDataFactory
    {
        private string _expectedViewName = "expectedView";
        private object _expectedViewModel;
        private RouteData _routeData = new RouteData();

        [Test]
        public void IndexReturnsSpecifiedLayout()
        {
            _expectedViewName = "expectedView";
            _routeData = new RouteData();

            var result = new PageController(this, this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        [Test]
        public void IndexReturnsDifferentLayoutForADifferentRouteName()
        {
            _expectedViewName = "differentView";

            _routeData = new RouteData();
            _routeData.DataTokens.Add("__RouteName", "ADifferentRouteName");

            var result = new PageController(this, this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        [Test]
        public void IndexReturnsSpecifiedViewModel()
        {
            _expectedViewModel = new Object();

            var result = new PageController(this, this).Index();

            Assert.That(result.ViewData.Model, Is.EqualTo(_expectedViewModel));
        }

        public PageModel Build(string routeName)
        {
            string viewName;
            if (routeName == "ADifferentRouteName")
                viewName = "differentView";
            else
                viewName = "expectedView";

            return new PageModel
                {
                    ViewName = viewName,
                    ViewModel = _expectedViewModel
                };
        }

        public MvcRouteData Build(RouteData routeData)
        {
            return new MvcRouteData(_routeData);
        }
    }
}
