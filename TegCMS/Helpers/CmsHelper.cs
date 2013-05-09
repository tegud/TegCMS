using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using TegCMS.Pages.Models;

namespace TegCMS.Helpers
{
    public static class CmsHelper
    {
        public static IHtmlString CmsRegion(this HtmlHelper helper, Dictionary<string, PageRegionViewModel> regions, string region)
        {
            var allHtml = new StringBuilder();

            foreach(var component in regions[region].Components)
            {
                allHtml.Append(helper.Action(component.Action,
                                             component.Controller,
                                             new RouteValueDictionary
                                                 {
                                                     {"configuration", component.Configuration}
                                                 }).ToHtmlString());
            }

            return new HtmlString(allHtml.ToString());
        }


        //public IHtmlString RenderAction(string region)
        //{

        //    foreach (var component in Regions[region].Components)
        //    {
        //        return ChildActionExtensions.Action(new HtmlHelper(), component.Action, component.Controller,
        //                                            new RouteValueDictionary { { "configuration", component.Configuration } });
        //    }
        //}

    }
}