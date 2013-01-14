using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Trul.Data.Core;
using Trul.Domain.Entities;

namespace Trul.Data.EntityFramework.Repositories
{
    public partial class CountryRepository
    {
        public IEnumerable<Country> GetCountriesBySP()
        {
           return ((ISql)this.UnitOfWork).ExecuteQuery<Country>("Select CountryID ID, Name, IsDeleted From Country Where IsDeleted = @IsDeleted", new SqlParameter("@IsDeleted", false));
        }
    }
}
