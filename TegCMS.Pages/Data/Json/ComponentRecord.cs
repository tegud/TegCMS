using TegCMS.Pages.Models;

namespace TegCMS.Pages.Data.Json
{
    public class ComponentRecord
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public HtmlConfiguration Configuration { get; set; }
    }
}