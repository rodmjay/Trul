using System;
using System.Linq.Expressions;

namespace Trul.Framework.Rules
{
    public class PropertyValueConstraint<T> : IConstraint
    {
        private readonly Expression<Func<T, object>> _field;
        private readonly Func<T, object> _getFieldVal;

        private readonly Func<object, T> _castToT;
        private readonly IConstraint _innerConstraint;

        public IConstraint InnerConstraint
        {
            get { return _innerConstraint; }
        }

        public Expression<Func<T, object>> FieldExpression
        {
            get { return _field; }
        }

        public PropertyValueConstraint(Expression<Func<T, object>> field, IConstraint innerConstraint)
        {
            _field = field;
            _getFieldVal = field.Compile();
            _innerConstraint = innerConstraint;

            _castToT = val => (T) val;
        }

        public bool SatisfiedBy(object value)
        {
            return _innerConstraint.SatisfiedBy(_getFieldVal(_castToT(value)));
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}