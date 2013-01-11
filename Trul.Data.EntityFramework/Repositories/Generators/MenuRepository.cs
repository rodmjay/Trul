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
    public partial class MenuRepository : Repository<Menu, Int32>, IMenuRepository
    {
        /// <summary>
        /// MenuRepository
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MenuRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
