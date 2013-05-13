using System.Collections.Generic;

namespace TegCMS.Pages.Data.Json
{
    public class PageRecord
    {
        public RouteRecord Route { get; set; }

        public string Layout { get; set; }

        public Dictionary<string, ComponentRecord[]> Regions { get; set; }
    }
}