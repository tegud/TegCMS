namespace TegCMS.Pages.Models
{
    public interface IPageModelFactory
    {
        PageModel Build(string routeName, string getUrlHostName);
    }
}