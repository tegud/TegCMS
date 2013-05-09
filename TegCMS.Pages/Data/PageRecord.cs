using System.Collections.Generic;

namespace TegCMS.Pages.Data
{
    public class PageRecord
    {
        public string RouteName { get; set; }

        public string Layout { get; set; }

        public Dictionary<string, PageRegionInformation> Regions { get; set; }
    }
}