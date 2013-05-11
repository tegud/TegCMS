using System.Web.Mvc;
using TegCMS.Pages.Models;

namespace TegCMS.Controllers
{
    public class MarkdownController : Controller
    {
        public ContentResult Index(MarkdownConfiguration markdownConfiguration)
        {
            var md = new MarkdownDeep.Markdown();

            return Content(md.Transform(markdownConfiguration.Markdown));
        }
    }
}