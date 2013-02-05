/*
	Automatik generate edilen repository class. Burada değişiklik yapmayın!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Data.Core;
using Trul.Domain.Entities;
using Trul.Domain.Repositories;

namespace Trul.Data.EntityFramework.Repositories
{
    public partial class UserRepository : DelRepository<User, Int32>, IUserRepository
    {
        /// <summary>
        /// UserRepository
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
