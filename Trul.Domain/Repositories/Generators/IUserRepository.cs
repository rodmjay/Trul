/*
	Automatik generate edilen repository interface. Burada değişiklik yapmayın!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;
using Trul.Domain.Core;

namespace Trul.Domain.Repositories
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public partial interface IUserRepository : IRepository<User, Int32>
    {
    }
}
