﻿using System.Web.Routing;
using NUnit.Framework;

namespace TegCMS.Utilities.Tests
{
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
}
