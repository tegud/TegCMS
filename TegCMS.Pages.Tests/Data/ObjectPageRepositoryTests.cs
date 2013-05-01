using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TegCMS.Pages.Data;

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

    public class ObjectPageRepository : IPageRepository
    {
        public PageInformation GetForRouteNameAndHostName(string routeName, string hostName)
        {
            if(hostName != "localhost" && hostName != "www.tegud.net")
            {
                throw new UnknownHostException();
            }

            return new PageInformation
                {
                    SiteName = "tegud"
                };
        }
    }

    public class UnknownHostException : Exception
    {
        
    }
}
