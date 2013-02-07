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

            _castToT = val => (T)val;
        }

        public bool SatisfiedBy(IField value)
        {
            return _innerConstraint.SatisfiedBy(new Field(_getFieldVal(_castToT(value.Value))));
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class PropertiesValueConstraint<T> : IConstraint, ICompareField
    {
        private readonly Expression<Func<T, object>> _fieldLeft;
        private readonly Expression<Func<T, object>> _fieldRight;
        private readonly Func<T, object> _getLeftFieldVal;
        private readonly Func<T, object> _getRightFieldVal;

        private readonly Func<object, T> _castToT;
        private readonly IConstraint _innerConstraint;

        public IConstraint InnerConstraint
        {
            get { return _innerConstraint; }
        }

        public Expression<Func<T, object>> LeftFieldExpression
        {
            get { return _fieldLeft; }
        }

        public Expression<Func<T, object>> RightFieldExpression
        {
            get { return _fieldRight; }
        }

        public PropertiesValueConstraint(Expression<Func<T, object>> fieldLeft, Expression<Func<T, object>> fieldRight, ICompareConstraint innerConstraint)
        {
            _fieldLeft = fieldLeft;
            _fieldRight = fieldRight;
            _getLeftFieldVal = fieldLeft.Compile();
            _getRightFieldVal = fieldRight.Compile();
            _innerConstraint = innerConstraint;

            _castToT = val => (T)val;
        }

        public bool SatisfiedBy(IField value)
        {
            var value1 = (value as ICompareField).Value;
            var value2 = (value as ICompareField).RightValue;
            return _innerConstraint.SatisfiedBy(new CompareField(_getLeftFieldVal(_castToT(value1)), _getRightFieldVal(_castToT(value2))));
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string FieldName
        {
            get
            {
                return ((MemberExpression)LeftFieldExpression.Body).Member.Name;
            }
            set { }
        }

        public string RightFieldName
        {
            get
            {
                return ((MemberExpression)RightFieldExpression.Body).Member.Name;
            }
            set { }
        }

        public object Value { get; set; }

        public object RightValue { get; set; }
    }
}