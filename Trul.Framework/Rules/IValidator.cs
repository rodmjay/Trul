using System.Collections.Generic;
using Trul.Framework.Rules;

namespace Trul.Framework.Rules
{
    public interface IValidator<T> : IRulesGroup
    {
        bool IsValid(T entity);
        IEnumerable<IRule> GetBrokenRules(T entity);
    }
}