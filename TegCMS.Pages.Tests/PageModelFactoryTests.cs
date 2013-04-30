using NUnit.Framework;
using TegCMS.Pages.Data;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Tests
{
    [TestFixture]
    public class PageModelFactoryTests : IPageRepository
    {
        private string _siteName;

        [Test]
        public void BuildSetsViewNameFromLayoutAndSite()
        {
            _siteName = "tegud";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("Home", "localhost").ViewName, Is.EqualTo("tegud/2Column"));
        }

        [Test]
        public void BuildSetsViewNameForDifferentSite()
        {
            _siteName = "anotherSite";

            var factory = new PageModelFactory(this);

            Assert.That(factory.Build("Home", "www.google.com").ViewName, Is.EqualTo("anotherSite/2Column"));
        }

        public PageInformation GetForRouteNameAndHostName(string routeName, string hostName)
        {
            return new PageInformation
                {
                    SiteName = _siteName
                };
        }
    }
}
