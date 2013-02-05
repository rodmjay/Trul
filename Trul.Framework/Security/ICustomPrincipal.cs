using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Trul.Framework.Security
{
    public interface ICustomPrincipal : IPrincipal
    {
        int UserID { get; set; }

        string UserName { get; set; }

        string FirstName { get; set; }
        
        string LastName { get; set; }
    }
}
