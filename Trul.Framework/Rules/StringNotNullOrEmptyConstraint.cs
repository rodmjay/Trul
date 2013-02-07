namespace Trul.Framework.Rules
{
    public class StringNotNullOrEmptyConstraint : IConstraint
    {
        public bool SatisfiedBy(IField value)
        {
            return value.Value != null && value.Value.ToString() != string.Empty;
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}