﻿using System.Text.RegularExpressions;

namespace Trul.Framework.Rules
{
    public abstract class RegularExpressionConstraint : IConstraint
    {
        public string Pattern { get; private set; }

        protected RegularExpressionConstraint(string pattern)
        {
            Pattern = pattern;
        }

        public bool SatisfiedBy(object value)
        {
            return Regex.IsMatch(value.ToString(), Pattern);
        }

        public abstract void Accept(IValidatorVisitor visitor);
    }
}
