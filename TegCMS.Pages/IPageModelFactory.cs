namespace TegCMS.Pages
{
    public interface IPageModelFactory
    {
        PageModel Build(string viewName, string getUrlHostName);
    }

    public class PageModelFactory : IPageModelFactory
    {
        public PageModel Build(string viewName, string hostName)
        {
           return new PageModel();
        }
    }
}