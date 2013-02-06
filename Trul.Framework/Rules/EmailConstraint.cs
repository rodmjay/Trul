namespace Trul.Framework.Rules
{
    public class EmailConstraint : RegularExpressionConstraint
    {
        private const string EMAIL_PATTERN = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        public EmailConstraint() : base(EMAIL_PATTERN)
        {
        }

        public override void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
