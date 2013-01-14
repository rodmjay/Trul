using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.DTO;
using Trul.Application.UI.Core.Models;

namespace Trul.Application.UI.Core.Tasks
{
    public interface ICountryTask
    {
        CountryViewModel Index();

        IList<CountryDTO> GetCountries();
    }
}
