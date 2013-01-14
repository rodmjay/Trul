using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.UI.Core.Models;
using Trul.Application.UI.Core.Tasks;
using Trul.Service.Core;

namespace Trul.Application.UI.Tasks
{
    public class CountryTask : TaskBase, ICountryTask
    {
        private ICountryService countryService;

        public CountryTask(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        public CountryViewModel Index()
        {
            var countries = countryService.GetCountriesBySP();
            var countryViewModel = new CountryViewModel();
            countryViewModel.Countries = countries;//countryService.GetAll();
            return countryViewModel;
        }
    }
}
