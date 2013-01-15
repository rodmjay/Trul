using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trul.WebUI.Helper
{
    public static class ResourceHelper
    {
        public static string GetLocalResource(this WebViewPage page, string key)
        {
            return page.ViewContext.HttpContext.GetLocalResourceObject(page.VirtualPath, key) as string;
        }

        public static string GetGlobalResource(this HtmlHelper htmlHelper, string resourceKey)
        {
            return htmlHelper.ViewContext.HttpContext.GetGlobalResourceObject("Resource", resourceKey) as string; 
        }
    }
}