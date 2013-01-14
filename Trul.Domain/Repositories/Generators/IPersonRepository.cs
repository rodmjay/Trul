

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
    /// IPersonRepository
    /// </summary>
    public partial interface IPersonRepository : IRepository<Person, Int32>
    {
    }
}
