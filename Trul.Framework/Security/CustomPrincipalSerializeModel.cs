using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Framework.Security
{
    public class CustomPrincipalSerializeModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string[] Roles { get; set; }
    }
}
