using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TegCMS.Pages.Data;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Tests.Models
{
    [TestFixture]
    public class PageModelFactoryTests : IPageRepository
    {
        private string _siteName;
        private string _layout;

        [Test]
        public void BuildSetsViewNameFromLayout()
        {
            _siteName = "tegud";
            _layout = "2Column";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("Home", "localhost").ViewName, Is.EqualTo("2Column"));
        }

        [Test]
        public void BuildSetsAreaNameForFromSite()
        {
            _siteName = "anotherSite";
            _layout = "Default";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("Home", "www.google.com").AreaName, Is.EqualTo("anotherSite"));
        }

        [Test]
        public void BuildSetsViewNameForDifferentRouteName()
        {
            _siteName = "tegud";
            _layout = "1Column";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("About", "localhost").ViewName, Is.EqualTo("1Column"));
        }

        [Test]
        public void BuildSetsViewModelRegions()
        {
            _siteName = "tegud";
            _layout = "1Column";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("About", "localhost").ViewModel.Regions.Keys.First(), Is.EqualTo("Head"));
        }

        [Test]
        public void BuildSetsViewModelRegionComponentController()
        {
            _siteName = "tegud";
            _layout = "1Column";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("About", "localhost").ViewModel.Regions["Head"].Components.First().Controller, Is.EqualTo("Html"));
        }

        [Test]
        public void BuildSetsViewModelRegionComponentAction()
        {
            _siteName = "tegud";
            _layout = "1Column";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("About", "localhost").ViewModel.Regions["Head"].Components.First().Action, Is.EqualTo("Index"));
        }

        [Test]
        public void BuildSetsViewModelRegionComponentConfigurationHtml()
        {
            _siteName = "tegud";
            _layout = "1Column";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("About", "localhost").ViewModel.Regions["Head"].Components.First().Configuration, Is.EqualTo("<h1>Another test</h1>"));
        }

        public PageInformation GetForRouteNameAndHostName(string routeName, string hostName)
        {
            return new PageInformation
                {
                    SiteName = _siteName,
                    Layout = _layout,
                    Regions = new Dictionary<string, PageRegionInformation>
                        {
                            {
                                "Head", new PageRegionInformation
                                    {
                                        Components = new List<PageComponent>
                                            {
                                                new PageComponent
                                                    {
                                                        ControllerAction = new PageComponentControllerAction
                                                            {
                                                                Controller = "Html"
                                                            },
                                                        Configuration = "<h1>Another test</h1>"
                                                    }
                                            }
                                    }
                            }
                        }
                };
        }
    }
}
