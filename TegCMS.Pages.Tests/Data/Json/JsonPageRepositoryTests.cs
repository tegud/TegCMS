﻿using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TegCMS.ErrorHandling;
using TegCMS.Pages.Data.Json;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Tests.Data.Json
{
    [TestFixture]
    public class JsonPageRepositoryTests
    {
        [Test]
        public void GetForRouteNameAndHostNameSetsSiteName()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").SiteName, Is.EqualTo("Tegud"));
        }

        [Test]
        public void GetForRouteNameAndHostNameThrowsSiteNotFoundExceptionForUnknownSite()
        {
            Assert.Throws<UnknownHostException>(
                () => new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "Unknown Site"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsLayout()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").Layout, Is.EqualTo("2Column"));
        }

        [Test]
        public void GetForRouteNameAndHostNameThrowsPageNotFoundExceptionForUnknowRouteName()
        {
            Assert.Throws<PageNotFoundException>(
                () =>
                new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Unknown Route Name", "www.tegud.net"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsRegion()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").Regions.Keys.First(), Is.EqualTo("Head"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsComponentController()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").Regions["Head"].Components.First().ControllerAction.Controller, Is.EqualTo("Html"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsComponentAction()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").Regions["Head"].Components.First().ControllerAction.Action, Is.EqualTo("Index"));
        }

        [Test]
        public void GetForRouteNameAndHostNameComponentActionDefaultsToIndex()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").Regions["Head"].Components.ElementAt(1).ControllerAction.Action, Is.EqualTo("Index"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsComponentJsonConfiguration()
        {
            Assert.That(new JsonPageRepository("sites.json").GetForRouteNameAndHostName("Home", "www.tegud.net").Regions["Head"].Components.ElementAt(2).Configuration, Is.EqualTo(JObject.FromObject(new { Markdown = "One\r\n===" })));
        }

        [Test]
        public void GetAllRoutesReturnsCmsRouteCollection()
        {
            Assert.That(new JsonPageRepository("sites.json").GetAllRoutes(), Is.TypeOf<CmsRouteCollection>());
        }

        [Test]
        public void GetAllRoutesSetsFirstRouteName()
        {
            Assert.That(new JsonPageRepository("sites.json").GetAllRoutes().Routes.First().Name, Is.EqualTo("Home"));
        }

        [Test]
        public void GetAllRoutesSetsFirstRouteUrl()
        {
            Assert.That(new JsonPageRepository("sites.json").GetAllRoutes().Routes.First().Url, Is.EqualTo("/"));
        }
    }
}
