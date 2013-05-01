using NUnit.Framework;
using TegCMS.ErrorHandling;

namespace TegCMS.Pages.Tests.Data
{
    [TestFixture]
    public class ObjectPageRepositoryTests
    {
        [Test]
        public void GetForRouteNameAndHostNameSetsSiteNameToTegudForLocalhost()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName(null, "localhost").SiteName, Is.EqualTo("tegud"));
        }

        [Test]
        public void GetForRouteNameAndHostNameSetsSiteNameToTegudForTegudNet()
        {
            Assert.That(new ObjectPageRepository().GetForRouteNameAndHostName(null, "www.tegud.net").SiteName, Is.EqualTo("tegud"));
        }

        [Test]
        public void GetForRouteNameAndHostNameThrowsUnknownHostExceptionForAnotherHostname()
        {
            Assert.Throws<UnknownHostException>(() => new ObjectPageRepository().GetForRouteNameAndHostName(null, "AnotherHostname"));
        }
    }
}
