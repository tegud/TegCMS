using NUnit.Framework;
using TegCMS.ErrorHandling;
using TegCMS.Pages.Data.ObjectPage;

namespace TegCMS.Pages.Tests.Data.ObjectPage
{
    [TestFixture]
    public class ObjectPageRepositoryTests
    {
        [Test]
        public void GetForRouteNameAndHostNameSetsSiteNameToTegudForLocalhost()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName("Home", "localhost").SiteName, Is.EqualTo("tegud"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsSiteNameToTegudForTegudNet()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName("Home", "www.tegud.net").SiteName, Is.EqualTo("tegud"));
        }

        [Test]
        public void GetForRouteNameAndHostNameThrowsUnknownHostExceptionForAnotherHostname()
        {
            Assert.Throws<UnknownHostException>(() => new ObjectPageRepository().GetForRouteNameAndHostName(null, "AnotherHostname"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsLayoutTo2ColumnForRouteNameHome()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName("Home", "www.tegud.net").Layout, Is.EqualTo("2Column"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsLayoutTo1ColumnForRouteNameAbout()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName("About", "www.tegud.net").Layout, Is.EqualTo("1Column"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsHeadRegionForHome()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName("Home", "www.tegud.net").Regions.ContainsKey("Head"), Is.True);
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsFooterRegionForAbout()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName("About", "www.tegud.net").Regions.ContainsKey("Footer"), Is.True);
        }
    }
}
