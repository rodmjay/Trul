

/*
	Automatik generate edilen service interface. Burada değişiklik yapmayın!
*/

using System;
using Trul.Application.DTO;
using Trul.Service;

namespace Trul.Service.Core
{
    /// <summary>
    /// IPersonService
    /// </summary>
    public partial interface IPersonService : IRepositoryService<PersonDTO, Int32>
    {

    }
}
