using NUnit.Framework;
using TegCMS.Pages.Data;

namespace TegCMS.Pages.Tests.Data
{
    [TestFixture]
    public class SiteInformationTests
    {
        [Test]
        public void HostNameMatchesReturnsTrueForMatchingPattern()
        {
            Assert.That(new SiteInformation(@"tegud\.net", null).HostNameMatches("www.tegud.net"), Is.True);
        }

        [Test]
        public void HostNameMatchesReturnsFalseForNonMatchingPattern()
        {
            Assert.That(new SiteInformation(@"tegud\.net", null).HostNameMatches("localhost"), Is.False);
        }
    }
}