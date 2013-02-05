using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Application.DTO
{
    public class RoleDTO : BaseObjectDTO
    {
        public string RoleName { get; set; }

        public IList<UserDTO> Users { get; set; }
    }
}
