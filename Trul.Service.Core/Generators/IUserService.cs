/*
	Automatik generate edilen service interface. Burada değişiklik yapmayın!
*/

using System;
using Trul.Application.DTO;
using Trul.Service;

namespace Trul.Service.Core
{
    /// <summary>
    /// IUserService
    /// </summary>
    public partial interface IUserService : IRepositoryService<UserDTO, Int32>
    {

    }
}
