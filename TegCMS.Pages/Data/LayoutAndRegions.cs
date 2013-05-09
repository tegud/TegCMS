using System.Collections.Generic;

namespace TegCMS.Pages.Data
{
    public class LayoutAndRegions
    {
        public string Layout { get; set; }

        public Dictionary<string, PageRegionInformation> Regions { get; set; }
    }
}