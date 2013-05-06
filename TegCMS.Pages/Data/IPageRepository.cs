namespace TegCMS.Pages.Data
{
    public interface IPageRepository
    {
        PageInformation GetForRouteNameAndHostName(string routeName, string hostName);
    }
}
