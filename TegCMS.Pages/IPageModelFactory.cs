namespace TegCMS.Pages
{
    public interface IPageModelFactory
    {
        PageModel Build(string viewName);
    }
}