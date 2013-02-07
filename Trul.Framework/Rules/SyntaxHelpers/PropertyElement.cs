using System;
using System.Linq.Expressions;

namespace Trul.Framework.Rules.SyntaxHelpers
{
    internal class PropertyElement<T> : IPropertyElement<T>
    {
        private readonly IValidator<T> _validator;
        private readonly Expression<Func<T, object>> _prop;
        private readonly Expression<Func<T, object>> _propRight;

        public PropertyElement(Expression<Func<T, object>> prop, IValidator<T> validator)
        {
            _validator = validator;
            _prop = prop;
        }

        public PropertyElement(Expression<Func<T, object>> prop, Expression<Func<T, object>> propRight, IValidator<T> validator)
        {
            _validator = validator;
            _prop = prop;
            _propRight = propRight;
        }

        public IValidator<T> SatisfiedAs(IConstraint constraint)
        {
            _validator.AddRule(new Rule(new PropertyValueConstraint<T>(_prop, constraint)));
            return _validator;
        }

        public IValidator<T> SatisfiedAs(ICompareConstraint constraint)
        {
            _validator.AddRule(new Rule(new PropertiesValueConstraint<T>(_prop, _propRight, constraint)));
            return _validator;
        }
    }
}