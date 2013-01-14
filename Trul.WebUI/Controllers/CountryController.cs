using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Trul.Application.UI.Core.Tasks;
using Trul.WebUI.Infrastructure;

namespace Trul.WebUI.Controllers
{
    public class CountryController : Controller
    {
        private ICountryTask countryTask;

        public CountryController(ICountryTask countryTask)
        {
            this.countryTask = countryTask;
        }

        public ActionResult Index(bool? isLoadData)
        {
            var model = countryTask.Index();
            if (isLoadData.HasValue && isLoadData.Value)
            {
                model.Countries = countryTask.GetCountries();
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
    }
}
