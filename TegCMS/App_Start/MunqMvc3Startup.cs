using System.Web.Mvc;
using Munq.MVC3;
using TegCMS.Pages.Models;
using TegCMS.Utilities;

[assembly: WebActivator.PreApplicationStartMethod(
	typeof(TegCMS.App_Start.MunqMvc3Startup), "PreStart")]

namespace TegCMS.App_Start {
	public static class MunqMvc3Startup {
		public static void PreStart() {
			DependencyResolver.SetResolver(new MunqDependencyResolver());

            var ioc = MunqDependencyResolver.Container;
            ioc.Register<IPageModelFactory>(i => new PageModelFactory(MvcApplication.PageRepository));
            ioc.Register<IMvcRouteDataFactory, MvcRouteDataFactory>();
            ioc.Register<IMvcRequestFactory, MvcRequestFactory>();
		}
	}
}
 

