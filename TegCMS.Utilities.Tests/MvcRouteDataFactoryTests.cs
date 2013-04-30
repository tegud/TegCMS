using System.Web;
using System.Web.Routing;
using NUnit.Framework;

namespace TegCMS.Utilities.Tests
{
    [TestFixture]
    public class MvcRequestFactoryTests
    {
        [Test]
        public void MvcRequestIsCreatedWithProvidedRequest()
        {
            var mvcRequest = new MvcRequestFactory().Build(new HttpRequestWrapper(new HttpRequest(string.Empty, "http://localhost", string.Empty)));
            Assert.That(mvcRequest.GetUrlHostName(), Is.EqualTo("localhost"));
        }
    }

    [TestFixture]
    public class MvcRequestTests
    {
        [Test]
        public void GetUrlHostNameReturnsHostname()
        {
            var mvcRequest = new MvcRequest(new HttpRequestWrapper(new HttpRequest(string.Empty, "http://localhost", string.Empty)));
            Assert.That(mvcRequest.GetUrlHostName(), Is.EqualTo("localhost"));
        }
    }

    [TestFixture]
    public class MvcRouteDataFactoryTests
    {
        [Test]
        public void MvcRouteDataIsCreatedWithProvidedRouteData()
        {
            var routeData = new RouteData();

            routeData.DataTokens.Add("__RouteName", "RouteName");

            var mvcRouteData = new MvcRouteDataFactory().Build(routeData);

            Assert.That(mvcRouteData.GetRouteName(), Is.EqualTo("RouteName"));
        }
    }

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
