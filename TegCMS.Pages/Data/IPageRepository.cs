using TegCMS.Pages.Models;

namespace TegCMS.Pages.Data
{
    public interface IPageRepository
    {
        PageInformation GetForRouteNameAndHostName(string routeName, string hostName);
        CmsRouteCollection GetAllRoutes();
    }
}
