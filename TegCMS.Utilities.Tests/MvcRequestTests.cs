using System.Web;
using NUnit.Framework;

namespace TegCMS.Utilities.Tests
{
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
}