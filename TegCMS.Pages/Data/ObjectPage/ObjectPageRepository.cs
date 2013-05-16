using System.Collections.Generic;
using System.Linq;
using TegCMS.ErrorHandling;
using TegCMS.Pages.Models;

namespace TegCMS.Pages.Data.ObjectPage
{
    public class ObjectPageRepository : IPageRepository
    {
        private readonly IEnumerable<SiteInformation> _siteInformation = new List<SiteInformation>
            {
                new SiteInformation(@"(tegud\.net)|(localhost)", "tegud", new List<PageRecord>
                    {
                        new PageRecord
                            {
                                RouteName = "", 
                                Layout = "2Column", 
                                Regions = new Dictionary<string, PageRegionInformation>
                                    {
                                        { 
                                            "Head", 
                                            new PageRegionInformation
                                            {
                                                Components = new List<PageComponent> { 
                                                    new PageComponent
                                                    {
                                                        ControllerAction = new PageComponentControllerAction { Controller = "Html" },
                                                        Configuration = "{ Html = \"<h1>Test</h1>\" }"
                                                    }
                                                }
                                            } 
                                        },
                                        { 
                                            "Main", 
                                            new PageRegionInformation
                                            {
                                                Components = new List<PageComponent> { 
                                                    new PageComponent
                                                    {
                                                        ControllerAction = new PageComponentControllerAction { Controller = "Html" },
                                                        Configuration = "{ Html = \"<article><h2>One</h2></article>\" }"
                                                    },
                                                    new PageComponent
                                                    {
                                                        ControllerAction = new PageComponentControllerAction { Controller = "Html" },
                                                        Configuration = "{ Html = \"<article><h2>Two</h2></article>\" }"
                                                    }
                                                }
                                            } 
                                        }
                                    }
                            },
                        new PageRecord
                            {
                                RouteName = "Home", 
                                Layout = "2Column", 
                                Regions = new Dictionary<string, PageRegionInformation>
                                    {
                                        { "Head", new PageRegionInformation
                                            {
                                                Components = new List<PageComponent> { 
                                                    new PageComponent
                                                    {
                                                        ControllerAction = new PageComponentControllerAction { Controller = "Html" },
                                                        Configuration = "{ Html = \"<h1>Test</h1>\" }"
                                                    }
                                                }
                                            } 
                                        }
                                    }
                            },
                        new PageRecord { RouteName = "About", Layout = "1Column", Regions = new Dictionary<string, PageRegionInformation> { { "Footer", new PageRegionInformation() } }  }
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

            var layout = siteInformation.GetLayoutAndRegions(routeName);

            return new PageInformation
                {
                    SiteName = siteInformation.SiteName,
                    Layout = layout.Layout,
                    Regions = layout.Regions
                };
        }

        public CmsRouteCollection GetAllRoutes()
        {
            throw new System.NotImplementedException();
        }
    }
}