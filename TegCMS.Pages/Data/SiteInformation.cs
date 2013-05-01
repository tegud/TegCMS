using System.Text.RegularExpressions;

namespace TegCMS.Pages.Data
{
    public class SiteInformation
    {
        private readonly string _siteName;
        private readonly Regex _regex;

        public string SiteName { get { return _siteName; } }

        public SiteInformation(string pattern, string siteName)
        {
            _siteName = siteName;
            _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public bool HostNameMatches(string hostName)
        {
            return _regex.IsMatch(hostName);
        }
    }
}