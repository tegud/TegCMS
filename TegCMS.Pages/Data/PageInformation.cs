using System.Collections.Generic;

namespace TegCMS.Pages.Data
{
    public class PageInformation
    {
        public string SiteName { get; set; }

        public string Layout { get; set; }

        public Dictionary<string, PageRegionInformation> Regions { get; set; }
    }

    public class PageRegionInformation
    {
        public IEnumerable<PageComponent> Components { get; set; }
    }
}