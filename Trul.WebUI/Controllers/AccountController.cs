using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Trul.Application.UI.Core.Models;
using Trul.Application.UI.Core.Tasks;
using Trul.Framework.Security;
using Trul.WebUI.Infrastructure;

namespace Trul.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private IAccountTask accountTask;

        public AccountController(IAccountTask accountTask)
        {
            this.accountTask = accountTask;
        }

        [AllowAnonymous()]
        public ActionResult LogOn()
        {
            return View(new AccountViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOn(AccountViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                accountTask.Login(model);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }

        [AllowAnonymous()]
        public ActionResult Register()
        {
            return View(new AccountViewModel());
        }

        [HttpPost]
        [AllowAnonymous()]
        public ActionResult Register(AccountViewModel model)
        {
            accountTask.Register(model);
            return RedirectToAction("LogOn");
        }

        public ActionResult LogOut()
        {
            accountTask.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
