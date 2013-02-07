using System;
using System.Linq.Expressions;

namespace Trul.Framework.Rules.SyntaxHelpers
{
    public static class DefineValidator
    {
        public static IValidator<T> For<T>()
        {
            return new TypeValidator<T>();
        }

        public static IPropertyElement<T> WhereProperty<T>(this IValidator<T> validator, Expression<Func<T, object>> prop)
        {
            var propElement = new PropertyElement<T>(prop, validator);
            return propElement;
        }

        public static IPropertyElement<T> WhereProperty<T>(this IValidator<T> validator, Expression<Func<T, object>> prop, Expression<Func<T, object>> propRight)
        {
            var propElement = new PropertyElement<T>(prop, propRight, validator);
            return propElement;
        }

        public static IValidator<T> AsError<T>(this IValidator<T> validator)
        {
            validator.LastRule().Severity = Severity.Error;
            return validator;
        }

        public static IValidator<T> AsWarning<T>(this IValidator<T> validator)
        {
            validator.LastRule().Severity = Severity.Warning;
            return validator;
        }

        public static IValidator<T> WithReason<T>(this IValidator<T> validator, string reason)
        {
            validator.LastRule().Message = reason;
            return validator;
        }

        private static IRule LastRule<T>(this IValidator<T> validator)
        {
            return validator.Rules[validator.Rules.Length - 1];
        }
    }
}
