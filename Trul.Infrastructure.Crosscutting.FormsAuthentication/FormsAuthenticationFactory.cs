using Trul.Infrastructure.Crosscutting.Logging;
using Trul.Infrastructure.Crosscutting.Security;
using Trul.Service;
using Trul.Service.Core;

namespace Trul.Infrastructure.Crosscutting.FormsAuthenticationService
{
    public class FormsAuthenticationFactory : IAuthenticationFactory
    {
        public IAuthentication Create()
        {
            return new FormsAuthenticationService();
        }
    }
}
