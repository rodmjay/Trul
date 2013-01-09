using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Application.DTO
{
    public class PersonDTO : BaseObjectDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CountryDTO Country { get; set; }
    }
}
