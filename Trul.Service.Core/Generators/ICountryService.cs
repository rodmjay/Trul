/*
	Automatik generate edilen service interface. Burada değişiklik yapmayın!
*/

using System;
using Trul.Application.DTO;
using Trul.Service;

namespace Trul.Service.Core
{
    /// <summary>
    /// ICountryService
    /// </summary>
    public partial interface ICountryService : IRepositoryService<CountryDTO, Int32>
    {

    }
}
