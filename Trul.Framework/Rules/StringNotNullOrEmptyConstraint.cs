namespace Trul.Framework.Rules
{
    public class StringNotNullOrEmptyConstraint : IConstraint
    {
        public bool SatisfiedBy(object value)
        {
            return value != null && value.ToString() != string.Empty;
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}