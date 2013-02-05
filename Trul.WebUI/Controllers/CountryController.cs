using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Trul.Application.UI.Core.Tasks;
using Trul.WebUI.Infrastructure;
using Trul.WebUI.Controllers;
using Trul.Application.UI.Core.Models;
using Trul.Application.DTO;

namespace Trul.WebUI.Controllers
{
    public class CountryController : BaseController
    {
        private ICountryTask countryTask;

        public CountryController(ICountryTask countryTask)
        {
            this.countryTask = countryTask;
        }

        [LogonAuthorize(Roles = "2")]
        public ActionResult Index()
        {
            var model = countryTask.Index();
            if (TempData["Countries"] != null)
            {
                model.Countries = (IList<CountryDTO>)TempData["Countries"];
            }
            return View(model);
        }

        [HttpGet]
        public CustomJsonResult GetCountries()
        {
            var result = new CustomJsonResult();
            result.Data = countryTask.GetCountries();
            return result;
        }

        public ActionResult GetCountriesOnServer()
        {
            TempData["Countries"] = countryTask.GetCountries();
            return RedirectToAction("Index");
        }
    }
}
