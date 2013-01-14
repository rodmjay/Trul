using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;

namespace Trul.Domain.Repositories
{
    public partial interface ICountryRepository
    {
        IEnumerable<Country> GetCountriesBySP();
    }
}
