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
    /// MenuService
    /// </summary>
    public partial class MenuService : RepositoryService<Menu, MenuDTO, Int32>, IMenuService
    {
        public MenuService(IMenuRepository repository)
            : base(repository)
        {

        }
    }
}
