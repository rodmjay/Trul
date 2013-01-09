using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trul.Domain.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DelEntityWithTypedId<TId> : EntityWithTypedId<TId>, IDelEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
