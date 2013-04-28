namespace TegCMS.Pages
{
    public class PageModelFactory : IPageModelFactory
    {
        public PageModel Build()
        {
            return new PageModel();
        }
    }
}