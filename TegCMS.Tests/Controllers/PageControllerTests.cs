using System.Web.Mvc;
using NUnit.Framework;
using TegCMS.Controllers;

namespace TegCMS.Tests.Controllers
{
    [TestFixture]
    public class PageControllerTests
    {
        [Test]
        public void IndexReturnsSpecifiedLayout()
        {
            const string expectedViewName = "expectedView";

            var result = new PageController().Index();

            Assert.That(result.ViewName, Is.EqualTo(expectedViewName));
        }
    }
}
