using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework.Security;

namespace Trul.Infrastructure.Crosscutting.Security
{
    public interface IAuthentication
    {
        void Login(string userName, string password, bool isPersistent, string customData);

        void Logout();

        ICustomPrincipal GetUser();

        void PostAuthenticateRequest();
    }
}
