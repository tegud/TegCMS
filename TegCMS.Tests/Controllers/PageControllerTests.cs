using System.Web.Mvc;
using NUnit.Framework;
using TegCMS.Controllers;
using TegCMS.Pages;

namespace TegCMS.Tests.Controllers
{
    [TestFixture]
    public class PageControllerTests : IPageModelFactory
    {
        private string _expectedViewName = "expectedView";

        [Test]
        public void IndexReturnsSpecifiedLayout()
        {
            _expectedViewName = "expectedView";

            var result = new PageController(this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        public PageModel Build()
        {
            return new PageModel()
                {
                    ViewName = _expectedViewName
                };
        }
    }
}
