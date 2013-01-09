using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core;

namespace Trul.Domain.Entities
{
    public class Person : DelEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}
