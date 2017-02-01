using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JAM.Netduino3.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static  class HtmlHelpers
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static string PageClass(this IHtmlHelper htmlHelper)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

    }
}
