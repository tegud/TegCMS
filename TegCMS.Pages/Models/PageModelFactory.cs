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
                    AreaName = pageInformation.SiteName
                };
        }
    }
}