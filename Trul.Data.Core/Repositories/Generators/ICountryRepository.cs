/*
	Automatik generate edilen repository interface. Burada değişiklik yapmayın!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;
using Trul.Data.Core;

namespace Trul.Data.Repositories
{
    /// <summary>
    /// ICountryRepository
    /// </summary>
    public interface ICountryRepository : IRepository<Country, Int32>
    {
    }
}
