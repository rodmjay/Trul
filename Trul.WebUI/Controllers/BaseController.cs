using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trul.Framework.Security;
using Trul.Infrastructure.Crosscutting.Security;
using Trul.WebUI.Infrastructure;

namespace Trul.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new ICustomPrincipal User
        {
            get { return AuthenticationFactory.CreateAuthentication().GetUser(); }
        }
    }
}