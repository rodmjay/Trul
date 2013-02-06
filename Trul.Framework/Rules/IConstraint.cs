namespace Trul.Framework.Rules
{
    public interface IConstraint : IValidatorElement
    {
        bool SatisfiedBy(object value);
    }
}