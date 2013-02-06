using System;

namespace Trul.Framework.Rules
{
    public class Rule : IRule
    {
        public IConstraint Constraint { get; private set; }
        public Severity Severity { get; set; }
        public string Message { get; set; }

        public Rule(IConstraint constraint)
        {
            Constraint = constraint;
            Severity = Severity.Error;
            Message = String.Empty;
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}