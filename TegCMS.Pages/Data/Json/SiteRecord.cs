using System.Text.RegularExpressions;

namespace TegCMS.Pages.Data.Json
{
    public class SiteRecord
    {
        public string SiteName { get; set; }

        private string _regex;
        private Regex _regEx;

        public string Regex
        {
            get { return _regex; }
            set
            {
                _regex = value;
                _regEx = new Regex(_regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
        }

        public PageRecord[] Pages { get; set; }

        public bool HostNameIsMatch(string hostName)
        {
            return _regEx.IsMatch(hostName);
        }
    }
}