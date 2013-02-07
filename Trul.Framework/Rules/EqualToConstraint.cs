using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Trul.Framework.Rules
{
    public class EqualToConstraint : ICompareConstraint
    {
        public bool SatisfiedBy(IField value)
        {
            return (value as ICompareField).Value.Equals((value as ICompareField).RightValue);
        }

        public void Accept(IValidatorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
