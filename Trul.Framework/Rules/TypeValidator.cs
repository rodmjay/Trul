using System.Collections.Generic;
using System.Linq;

namespace Trul.Framework.Rules
{
    public class TypeValidator<T> : IValidator<T>
    {
        private readonly IList<IRule> _rules = new List<IRule>();

        public IRule[] Rules
        {
            get { return _rules.ToArray(); }
        }

        public void AddRule(IRule rule)
        {
            _rules.Add(rule);
        }

        public bool IsValid(T entity)
        {
            foreach (var rule in _rules.Where(r => r.Severity == Severity.Error))
                if(!rule.Constraint.SatisfiedBy(new Field(entity)))
                    return false;

            return true;
        }

        public IEnumerable<IRule> GetBrokenRules(T entity)
        {
            return _rules.Where(r => !r.Constraint.SatisfiedBy(new Field(entity)));
        }
    }
}