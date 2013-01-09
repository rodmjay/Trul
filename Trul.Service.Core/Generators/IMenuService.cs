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
    /// IMenuService
    /// </summary>
    public partial interface IMenuService : IRepositoryService<Menu, MenuDTO, Int32>
    {

    }
}
