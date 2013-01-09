using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.DTO;

namespace Trul.Service.Core
{
    public partial interface IPersonService
    {
        IList<PersonDTO> GetNameLike(string name);
    }
}
