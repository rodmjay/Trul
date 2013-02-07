namespace Trul.Framework.Rules
{
    public interface IConstraint : IValidatorElement
    {
        bool SatisfiedBy(IField value);
    }

    public interface ICompareConstraint : IConstraint
    {

    }

    public interface IField
    {
        string FieldName { get; set; }
        object Value { get; set; }
    }

    public interface ICompareField : IField
    {
        string RightFieldName { get; set; }
        object RightValue { get; set; }
    }
}