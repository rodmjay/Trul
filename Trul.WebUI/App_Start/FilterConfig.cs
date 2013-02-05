using System.Web;
using System.Web.Mvc;
using Trul.WebUI.Infrastructure;

namespace Trul.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InternationalizationAttribute());
            filters.Add(new LogonAuthorize());
            //filters.Add(new RequireHttpsAttribute()); 
            filters.Add(new HandleErrorAttribute());
        }
    }
}