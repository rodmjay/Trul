/*
	Automatik generate edilen service class. Burada değişiklik yapmayın!
*/

using System;
using Trul.Application.DTO;
using Trul.Domain.Entities;
using Trul.Domain.Repositories;
using Trul.Service.Core;

namespace Trul.Service
{
    /// <summary>
    /// CountryService
    /// </summary>
    public partial class CountryService : RepositoryService<Country, CountryDTO, Int32>, ICountryService
    {
        public CountryService(ICountryRepository repository)
            : base(repository)
        {

        }
    }
}
