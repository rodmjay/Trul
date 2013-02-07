using System;
using System.Linq.Expressions;
namespace Trul.Framework.Rules.SyntaxHelpers
{
    public interface IPropertyElement<T>
    {
        IValidator<T> SatisfiedAs(IConstraint constraint);
        IValidator<T> SatisfiedAs(ICompareConstraint constraint);
    }
}