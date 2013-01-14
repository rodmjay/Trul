

/*
	Automatik generate edilen repository interface. Burada değişiklik yapmayın!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;
using Trul.Data.Core;

namespace Trul.Data.Repositories
{
    /// <summary>
    /// IPersonRepository
    /// </summary>
    public interface IPersonRepository : IRepository<Person, Int32>
    {
    }
}
