using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core;

namespace Trul.Domain.Entities
{
    public class User : DelEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}
