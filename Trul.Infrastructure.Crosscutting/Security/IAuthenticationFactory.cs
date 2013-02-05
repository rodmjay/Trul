using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Infrastructure.Crosscutting.Security
{
    public interface IAuthenticationFactory
    {
        IAuthentication Create();
    }
}
