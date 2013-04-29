using System.Web.Mvc;
using TegCMS.Pages;

namespace TegCMS.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageModelFactory _pageModelFactory;

        public PageController(IPageModelFactory pageModelFactory)
        {
            _pageModelFactory = pageModelFactory;
        }

        public ViewResult Index()
        {
            var pageModel = _pageModelFactory.Build();

            return View(pageModel.ViewName, pageModel.ViewModel);
        }
    }
}