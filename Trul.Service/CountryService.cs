using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.DTO;
using Trul.Data.Core;
using Trul.Domain.Core;
using Trul.Domain.Entities;
using Trul.Application;
using System.Data.Common;
using System.Data.SqlClient;
using Trul.Framework;
using Trul.Domain.Repositories;
using Trul.Data.Mock;

namespace Trul.Service
{
    public partial class CountryService
    {
        public IList<CountryDTO> GetCountriesBySP()
        {
            var countries = ((ICountryRepository)Repository).GetCountriesBySP();
            return ((IEnumerable<IEntityWithTypedId<Int32>>)countries).ProjectedAsCollection<CountryDTO, Int32>();
        }

        /// <summary>
        /// Mock service'ten data dondurur.
        /// </summary>
        /// <returns></returns>
        public IList<CountryDTO> GetCountries()
        {
            // TODO: burayi bir factory ile yonetmek daha mantikli olacaktir.
            var service = new MockRepository();
            var countries = service.GetAll();
            return ((IEnumerable<IEntityWithTypedId<Int32>>)countries).ProjectedAsCollection<CountryDTO, Int32>();
        }
    }
}
