using Newtonsoft.Json.Linq;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Data
{
    public class PageComponent
    {
        public PageComponentControllerAction ControllerAction { get; set; }

        public JObject Configuration { get; set; }
    }
}