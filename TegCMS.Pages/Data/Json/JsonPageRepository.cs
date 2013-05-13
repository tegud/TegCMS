using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TegCMS.ErrorHandling;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Data.Json
{
    public class JsonPageRepository : IPageRepository
    {
        private SiteRecord[] _siteRecords;

        public JsonPageRepository(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var fileContents = sr.ReadToEnd();

                _siteRecords = JsonConvert.DeserializeObject<SiteRecord[]>(fileContents);
            }
        }

        public PageInformation GetForRouteNameAndHostName(string routeName, string hostName)
        {
            var siteRecord = _siteRecords.FirstOrDefault(r => r.HostNameIsMatch(hostName));

            if (siteRecord == null)
            {
                throw new UnknownHostException();
            }

            var pageRecord = siteRecord.Pages.FirstOrDefault(p => p.Route.Name == routeName);

            if (pageRecord == null)
            {
                throw new PageNotFoundException();
            }

            return new PageInformation
                {
                    SiteName = siteRecord.SiteName,
                    Layout = pageRecord.Layout,
                    Regions = pageRecord.Regions.ToDictionary(kvp => kvp.Key, kvp => new PageRegionInformation
                        {
                            Components = kvp.Value.Select(c => new PageComponent
                                {
                                    ControllerAction = new PageComponentControllerAction
                                        {
                                            Controller = c.Controller,
                                            Action = c.Action
                                        },
                                    Configuration = c.Configuration
                                })
                        })
                };
        }

        public CmsRouteCollection GetAllRoutes()
        {
            return new CmsRouteCollection();
        }
    }
}