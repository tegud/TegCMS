using Newtonsoft.Json.Linq;

namespace TegCMS.Pages.Data.Json
{
    public class ComponentRecord
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public JObject Configuration { get; set; }
    }
}