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
    public interface IDelEntity
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
