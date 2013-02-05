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
    /// RoleService
    /// </summary>
    public partial class RoleService : RepositoryService<Role, RoleDTO, Int32>, IRoleService
    {
        public RoleService(IRoleRepository repository)
            : base(repository)
        {

        }
    }
}
