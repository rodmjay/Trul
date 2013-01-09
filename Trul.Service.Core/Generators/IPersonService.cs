

/*
	Automatik generate edilen service interface. Burada değişiklik yapmayın!
*/

using System;
using Trul.Application.DTO;
using Trul.Domain.Entities;
using Trul.Service;

namespace Trul.Service.Core
{
    /// <summary>
    /// IPersonService
    /// </summary>
    public partial interface IPersonService : IRepositoryService<Person, PersonDTO, Int32>
    {

    }
}
