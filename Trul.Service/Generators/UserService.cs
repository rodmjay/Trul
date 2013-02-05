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
    /// UserService
    /// </summary>
    public partial class UserService : RepositoryService<User, UserDTO, Int32>, IUserService
    {
        public UserService(IUserRepository repository)
            : base(repository)
        {

        }
    }
}
