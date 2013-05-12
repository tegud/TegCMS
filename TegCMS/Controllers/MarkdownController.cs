using System.Web.Http.ValueProviders;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using TegCMS.Pages.Models;

namespace TegCMS.Controllers
{
    public class MarkdownController : Controller
    {
        public ContentResult Index(MarkdownConfiguration configuration)
        {
            var serializer = new JavaScriptSerializer();
            var jsonData = serializer.Deserialize<MarkdownConfiguration>("{ \"Markdown\": \"One\\r\\n===\" }");
            var jsonData2 = JsonConvert.DeserializeObject<MarkdownConfiguration>("{ \"Markdown\": \"One\\r\\n===\" }");

            var md = new MarkdownDeep.Markdown();

            return Content(md.Transform(configuration.Markdown));
        }
    }
}