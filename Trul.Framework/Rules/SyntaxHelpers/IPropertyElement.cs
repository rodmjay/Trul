namespace Trul.Framework.Rules.SyntaxHelpers
{
    public interface IPropertyElement<T>
    {
        IValidator<T> SatisfiedAs(IConstraint constraint);
    }
}