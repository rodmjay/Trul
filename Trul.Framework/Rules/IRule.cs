namespace Trul.Framework.Rules
{
    public interface IRule : IValidatorElement
    {
        string Message { get; set; }
        Severity Severity { get; set; }
        IConstraint Constraint { get; }
    }
}