using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core;
using Trul.Domain.Resources;

namespace Trul.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Menu : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApplicationCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LinkName { get; set; }
    }
}
