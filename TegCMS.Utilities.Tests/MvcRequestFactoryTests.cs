using System.Web;
using NUnit.Framework;

namespace TegCMS.Utilities.Tests
{
    [TestFixture]
    public class MvcRequestFactoryTests
    {
        [Test]
        public void MvcRequestIsCreatedWithProvidedRequest()
        {
            var mvcRequest = new MvcRequestFactory().Build(new HttpRequestWrapper(new HttpRequest(string.Empty, "http://localhost", string.Empty)));
            Assert.That(mvcRequest.GetUrlHostName(), Is.EqualTo("localhost"));
        }
    }
}