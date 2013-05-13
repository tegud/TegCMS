using System.Collections.Generic;

namespace TegCMS.Pages.Models
{
    public class CmsRouteCollection
    {
        private IEnumerable<CmsRoute> _routes;

        public IEnumerable<CmsRoute> Routes
        {
            get { return _routes; }
        }
    }

    public class CmsRoute
    {
        public string Name { get; set; }
    }
}