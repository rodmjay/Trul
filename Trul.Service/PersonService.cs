using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application;
using Trul.Application.DTO;
using Trul.Domain.Core;
using Trul.Domain.Specifications;

namespace Trul.Service
{
    public partial class PersonService
    {
        public IList<PersonDTO> GetNameLike(string name)
        {
            return ((IEnumerable<IEntityWithTypedId<int>>)Repository.AllMatching(PersonSpecifications.NameFullText(name))).ProjectedAsCollection<PersonDTO, int>();
        }
    }
}
