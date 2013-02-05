using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trul.WebUI.Infrastructure;

namespace Trul.WebUI.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
