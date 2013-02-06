using System.Collections.Generic;

namespace Trul.Framework.Rules
{
    public interface IClientValidatorVisitor : IValidatorVisitor
    {
        string VisitValidator(IEnumerable<IRulesGroup> rulesGroups, ValidateSettings settings);
    }
}