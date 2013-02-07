using System;
using System.Linq.Expressions;
namespace Trul.Framework.Rules.SyntaxHelpers
{
    public static class Should
    {
        public static readonly IConstraint NotBeNullOrEmpty = new StringNotNullOrEmptyConstraint();

        public static IConstraint MatchEmailTemplate
        {
            get { return new EmailConstraint(); }
        }

        public static IConstraint NotBeLongerThan(int maxLength)
        {
            return new StringMaxLengthConstraint(maxLength);
        }

        public static ICompareConstraint EqualTo
        {
            get { return new EqualToConstraint(); }
        }
    }
}
