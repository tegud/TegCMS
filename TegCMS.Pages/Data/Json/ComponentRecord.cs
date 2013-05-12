using TegCMS.Pages.Models;

namespace TegCMS.Pages.Data.Json
{
    public class ComponentRecord
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string Configuration { get; set; }

        public string JsonConfiguration { get; set; }
    }
}