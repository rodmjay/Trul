namespace Trul.Framework.Rules
{
    public class StringMaxLengthConstraint : RegularExpressionConstraint
    {
        private const string MEX_LENGTH_PATTERN = @"[\s\S]{{0,{0}}}";

        public int MaxLength { get; private set; }

        public StringMaxLengthConstraint(int maxLength)
            : base(string.Format(MEX_LENGTH_PATTERN, maxLength))
        {
            MaxLength = maxLength;
        }

        public override void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}