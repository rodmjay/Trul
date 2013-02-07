using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Application.DTO
{
    public class UserDTO : BaseObjectDTO
    {
        public string UserName { get; set; }

        public IList<RoleDTO> Roles { get; set; }
    }
}
