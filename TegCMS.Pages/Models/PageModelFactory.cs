using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Newtonsoft.Json.Linq;
using TegCMS.Pages.Data;

namespace TegCMS.Pages.Models
{
    public class PageModelFactory : IPageModelFactory
    {
        private readonly IPageRepository _pageRepository;

        public PageModelFactory(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public PageModel Build(string routeName, string hostName)
        {
            var pageInformation = _pageRepository.GetForRouteNameAndHostName(routeName, hostName);

            return new PageModel
                {
                    ViewName = pageInformation.Layout,
                    AreaName = pageInformation.SiteName,
                    ViewModel = BuildViewModel(pageInformation.Regions)
                };
        }

        private static PageViewModel BuildViewModel(Dictionary<string, PageRegionInformation> regions)
        {
            return new PageViewModel
                {
                    Regions = regions.ToDictionary(kvp => kvp.Key, kvp => new PageRegionViewModel
                        {
                            Components = kvp.Value.Components.Select(c => new PageComponentViewModel
                                {
                                    Action = c.ControllerAction.Action,
                                    Controller = c.ControllerAction.Controller,
                                    Configuration = c.Configuration
                                })
                        })
                };
        }
    }

    public class PageViewModel
    {
        public Dictionary<string, PageRegionViewModel> Regions { get; set; }
    }

    public class PageRegionViewModel
    {
        public IEnumerable<PageComponentViewModel> Components { get; set; }
    }

    public class PageComponentViewModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public JObject Configuration { get; set; }
    }

    public class HtmlConfiguration
    {
        public string Html { get; set; }
    }
}