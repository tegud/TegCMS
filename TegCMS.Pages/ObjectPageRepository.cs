using System.Collections.Generic;
using System.Linq;
using TegCMS.ErrorHandling;
using TegCMS.Pages.Data;

namespace TegCMS.Pages
{
    public class ObjectPageRepository : IPageRepository
    {
        private readonly IEnumerable<SiteInformation> _siteInformation = new List<SiteInformation>
            {
                new SiteInformation(@"(tegud\.net)|(localhost)", "tegud", new List<PageRecord>
                    {
                        new PageRecord { RouteName = "Home", Layout = "2Column" },
                        new PageRecord { RouteName = "About", Layout = "1Column" }
                    })
            };

        public PageInformation GetForRouteNameAndHostName(string routeName, string hostName)
        {
            var siteInformation =
                _siteInformation.FirstOrDefault(si => si.HostNameMatches(hostName));

            if (siteInformation == null)
            {
                throw new UnknownHostException();
            }

            return new PageInformation
                {
                    SiteName = siteInformation.SiteName,
                    Layout = siteInformation.GetLayout(routeName)
                };
        }
    }
}