using Trul.Framework.Rules;
using Trul.Infrastructure.Crosscutting.Logging;
using Trul.Infrastructure.Crosscutting.Rules;

namespace Trul.Infrastructure.Crosscutting.NetFramework.Rules
{
    public class JQueryValidatorVisitorFactory : IClientValidatorVisitorFactory
    {
        public IClientValidatorVisitor Create()
        {
            return new JQueryValidatorVisitor();
        }
    }
}
