using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TegCMS.ErrorHandling;

namespace TegCMS.Pages.Data
{
    public class SiteInformation
    {
        private readonly string _siteName;
        private readonly IEnumerable<PageRecord> _pageRecords;
        private readonly Regex _regex;

        public string SiteName { get { return _siteName; } }

        public SiteInformation(string pattern, string siteName, IEnumerable<PageRecord> pageRecords)
        {
            _siteName = siteName;
            _pageRecords = pageRecords;
            _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public bool HostNameMatches(string hostName)
        {
            return _regex.IsMatch(hostName);
        }

        public LayoutAndRegions GetLayoutAndRegions(string routeName)
        {
            var pageRecord = _pageRecords.FirstOrDefault(p => p.RouteName == routeName);

            if (pageRecord == null)
            {
                throw new PageNotFoundException();
            }

            return new LayoutAndRegions
            {
                Layout = pageRecord.Layout,
                Regions = pageRecord.Regions
            };
        }
    }
}