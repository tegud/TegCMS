using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using NUnit.Framework;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Tests.Models
{
    [TestFixture]
    public class CmsRouteCollectionTests
    {
        [Test]
        public void AddRoutesToRouteTableSetsRouteName()
        {
            var routeCollection = new RouteCollection();

            var context = new StubHttpContextForRouting();

            var cmsRouteCollection = new CmsRouteCollection(new List<CmsRoute> { new CmsRoute { Name = "One", Url = "" } });
            cmsRouteCollection.AddRoutesToRouteTable(routeCollection);

            Assert.That(routeCollection.First().GetRouteData(context).DataTokens["__RouteName"], Is.EqualTo("One"));
        }

        [Test]
        public void AddRoutesToRouteTableSetsRouteUrl()
        {
            var routeCollection = new RouteCollection();

            var context = new StubHttpContextForRouting(requestUrl: "~/About");

            var cmsRouteCollection = new CmsRouteCollection(new List<CmsRoute> { new CmsRoute { Name = "One", Url = "About/" } });
            cmsRouteCollection.AddRoutesToRouteTable(routeCollection);

            Assert.That(routeCollection.First().GetRouteData(context), Is.Not.Null);
        }

        [Test]
        public void AddRoutesToRouteTableSetsRouteHandlerToMvcHandler()
        {
            var routeCollection = new RouteCollection();

            var context = new StubHttpContextForRouting();

            var cmsRouteCollection = new CmsRouteCollection(new List<CmsRoute> { new CmsRoute { Name = "One", Url = "" } });
            cmsRouteCollection.AddRoutesToRouteTable(routeCollection);

            Assert.That(routeCollection.First().GetRouteData(context).RouteHandler, Is.TypeOf<MvcRouteHandler>());
        }

        [Test]
        public void AddRoutesToRouteTableSetsRouteControllerToPage()
        {
            var routeCollection = new RouteCollection();

            var context = new StubHttpContextForRouting();

            var cmsRouteCollection = new CmsRouteCollection(new List<CmsRoute> { new CmsRoute { Name = "One", Url = "" } });
            cmsRouteCollection.AddRoutesToRouteTable(routeCollection);

            Assert.That(routeCollection.First().GetRouteData(context).Values["Controller"], Is.EqualTo("Page"));
        }

        [Test]
        public void AddRoutesToRouteTableSetsRouteActionToIndex()
        {
            var routeCollection = new RouteCollection();

            var context = new StubHttpContextForRouting();

            var cmsRouteCollection = new CmsRouteCollection(new List<CmsRoute> { new CmsRoute { Name = "One", Url = "" } });
            cmsRouteCollection.AddRoutesToRouteTable(routeCollection);

            Assert.That(routeCollection.First().GetRouteData(context).Values["Action"], Is.EqualTo("Index"));
        }

    }
}
