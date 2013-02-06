using System;
using System.Linq.Expressions;

namespace Trul.Framework.Rules.SyntaxHelpers
{
    internal class PropertyElement<T> : IPropertyElement<T>
    {
        private readonly IValidator<T> _validator;
        private readonly Expression<Func<T, object>> _prop;

        public PropertyElement(Expression<Func<T, object>> prop, IValidator<T> validator)
        {
            _validator = validator;
            _prop = prop;
        }

        public IValidator<T> SatisfiedAs(IConstraint constraint)
        {
            _validator.AddRule(new Rule(new PropertyValueConstraint<T>(_prop, constraint)));
            return _validator;
        }
    }
}