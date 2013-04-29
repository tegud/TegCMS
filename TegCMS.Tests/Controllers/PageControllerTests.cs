using System;
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
        private object _expectedViewModel;

        [Test]
        public void IndexReturnsSpecifiedLayout()
        {
            _expectedViewName = "expectedView";

            var result = new PageController(this).Index();

            Assert.That(result.ViewName, Is.EqualTo(_expectedViewName));
        }

        [Test]
        public void IndexReturnsSpecifiedViewModel()
        {
            _expectedViewModel = new Object();

            var result = new PageController(this).Index();

            Assert.That(result.ViewData.Model, Is.EqualTo(_expectedViewModel));
        }

        public PageModel Build()
        {
            return new PageModel
                {
                    ViewName = _expectedViewName,
                    ViewModel = _expectedViewModel
                };
        }
    }
}
