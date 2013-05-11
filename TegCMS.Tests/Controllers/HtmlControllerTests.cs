using System;
using NUnit.Framework;
using TegCMS.Controllers;
using TegCMS.Pages.Models;

namespace TegCMS.Tests.Controllers
{
    [TestFixture]
    public class HtmlControllerTests
    {
        [Test]
        public void ReturnsHtmlSetByConfiguration()
        {
            var content = new HtmlController().Index(new HtmlConfiguration {Html = "<h1>Test</h1>"}).Content;
            Assert.That(content,Is.EqualTo("<h1>Test</h1>"));
        }
    }

    [TestFixture]
    public class MarkdownControllerTests
    {
        [Test]
        public void ReturnsHtmlOfMarkdownSetByConfiguration()
        {
            var content = new MarkdownController().Index(new MarkdownConfiguration { Markdown = string.Format("Test{0}====", Environment.NewLine) }).Content;
            Assert.That(content, Is.EqualTo("<h1>Test</h1>\n"));
        }
    }
}
