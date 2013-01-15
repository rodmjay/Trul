using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Trul.WebUI.Helper
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string icon)
        {
            return ActionButton(htmlHelper, linkText, icon, null);
        }

        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string icon, object htmlAttributes)
        {
            return ActionButton(htmlHelper, linkText, icon, string.Empty, string.Empty, null, htmlAttributes, null);
        }

        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string icon, object htmlAttributes, object iconHtmlAttributes)
        {
            return ActionButton(htmlHelper, linkText, icon, string.Empty, string.Empty, null, htmlAttributes, iconHtmlAttributes);
        }

        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, object htmlAttributes, object iconHtmlAttributes)
        {
            RouteValueDictionary tmpAttributes = new RouteValueDictionary(htmlAttributes);
            RouteValueDictionary attributes = new RouteValueDictionary(tmpAttributes.Where(a => !a.Key.StartsWith("data_")));
            foreach (var item in tmpAttributes.Where(a => a.Key.StartsWith("data_")))
            {
                attributes.Add(item.Key.Replace("data_", "data-"), item.Value);
            }

            TagBuilder linkTag = new TagBuilder("a");

            RouteValueDictionary iconAttributes = new RouteValueDictionary(iconHtmlAttributes);
            TagBuilder iTag = new TagBuilder("i");
            iTag.AddCssClass(icon);
            iTag.MergeAttributes(iconAttributes);

            linkTag.AddCssClass("btn");
            linkTag.MergeAttributes(attributes);

            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var href = "#";
            if (!string.IsNullOrEmpty(actionName))
            {
                href = url.Action(actionName, controllerName);
            }
            linkTag.Attributes.Add("href", href);
            linkTag.InnerHtml = string.Format("{0} {1}", iTag.ToString(TagRenderMode.Normal), linkText);

            return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithSpan(this HtmlHelper html,
                    string linkText,
                        string actionName,
                        string controllerName,
                        object htmlAttributes)
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            TagBuilder linkTag = new TagBuilder("a");
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.SetInnerText(linkText);

            // Merge Attributes on the Tag you wish the htmlAttributes to be rendered on.
            //linkTag.MergeAttributes(attributes);
            spanTag.MergeAttributes(attributes);

            UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);

            linkTag.Attributes.Add("href", url.Action(actionName, controllerName));
            linkTag.InnerHtml = spanTag.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));
        }
    }
}