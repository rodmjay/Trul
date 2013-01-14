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

namespace Trul.Service
{
    public partial class CountryService
    {
        public IList<CountryDTO> GetCountriesBySP()
        {
            var countries = ((ISql)Repository.UnitOfWork).ExecuteQuery<Country>("Select CountryID ID, Name, IsDeleted From Country Where IsDeleted = @IsDeleted", new DatabaseParameter("@IsDeleted", false));
            return ((IEnumerable<IEntityWithTypedId<Int32>>)countries).ProjectedAsCollection<CountryDTO, Int32>();
        }
    }
}
