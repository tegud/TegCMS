using System.Web;
using System.Web.Mvc;
using TegCMS.Pages.Models;

namespace TegCMS.Controllers
{
    public class HtmlController : Controller
    {
        public ContentResult Index(HtmlConfiguration configuration)
        {
            return Content(new HtmlString(configuration.Html).ToHtmlString());
        }
    }
}