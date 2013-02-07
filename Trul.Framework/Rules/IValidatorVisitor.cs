using System.Collections.Generic;

namespace Trul.Framework.Rules
{
    public interface IValidatorVisitor
    {
        void Visit(IRule rule);
        void Visit(StringMaxLengthConstraint constraint);
        void Visit(EmailConstraint constraint);
        void Visit(StringNotNullOrEmptyConstraint constraint);
        void Visit(EqualToConstraint constraint);
        void Visit<T>(PropertyValueConstraint<T> constraint);
        void Visit<T>(PropertiesValueConstraint<T> constraint);
    }
}