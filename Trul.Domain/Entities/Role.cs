using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core;

namespace Trul.Domain.Entities
{
    public class Role : DelEntity
    {
        public string RoleName { get; set; }

        public IList<User> Users { get; set; }
    }
}
