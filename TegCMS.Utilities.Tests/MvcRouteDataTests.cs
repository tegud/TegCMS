using System.Web.Routing;
using NUnit.Framework;

namespace TegCMS.Utilities.Tests
{
    [TestFixture]
    public class MvcRouteDataTests
    {
        [Test]
        public void GetRouteNameReturnsEmptyStringIfNoValueSet()
        {
            var mvcRouteData = new MvcRouteDataFactory().Build(new RouteData());
            Assert.That(mvcRouteData.GetRouteName(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetRouteNameReturnsRouteNameIfSet()
        {
            var routeData = new RouteData();

            routeData.DataTokens.Add("__RouteName", "RouteName");

            var mvcRouteData = new MvcRouteDataFactory().Build(routeData);
            Assert.That(mvcRouteData.GetRouteName(), Is.EqualTo("RouteName"));
        }
    }
}