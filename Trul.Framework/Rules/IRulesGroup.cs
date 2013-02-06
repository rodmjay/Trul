using Trul.Framework.Rules;

namespace Trul.Framework.Rules
{
    public interface IRulesGroup
    {
        IRule[] Rules { get; }
        void AddRule(IRule rule);
    }
}