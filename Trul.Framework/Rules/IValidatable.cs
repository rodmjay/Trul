using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Framework.Rules
{
    public interface IValidatable
    {
        IEnumerable<IRulesGroup> ValidatorRules();
    }
}
