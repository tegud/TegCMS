using System.Collections.Generic;
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
                    ViewModel = new PageViewModel
                        {
                            Regions = new Dictionary<string, PageRegionViewModel>
                                {
                                    { "Head", new PageRegionViewModel
                                        {
                                            Components = new List<PageComponentViewModel>
                                                {
                                                    new PageComponentViewModel { Controller = "Html", Action = "Index", Configuration = new HtmlConfiguration {Html =  "<h1>Another test</h1>"} }
                                                }
                                        } 
                                    }
                                }
                        }
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

        public HtmlConfiguration Configuration { get; set; }
    }

    public class HtmlConfiguration
    {
        public string Html { get; set; }
    }
}