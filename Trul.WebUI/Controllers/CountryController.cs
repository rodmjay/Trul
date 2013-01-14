using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trul.Application.UI.Core.Tasks;

namespace Trul.WebUI.Controllers
{
    public class CountryController : Controller
    {
        private ICountryTask countryTask;

        public CountryController(ICountryTask countryTask)
        {
            this.countryTask = countryTask;
        }

        public ActionResult Index()
        {
            var model = countryTask.Index();
            return View(model);
        }
    }
}
