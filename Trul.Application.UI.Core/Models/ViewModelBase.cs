using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework.Rules;
using Trul.Infrastructure.Crosscutting.Rules;

namespace Trul.Application.UI.Core.Models
{
    [Serializable]
    public abstract class ViewModelBase : IValidatable
    {
        public virtual string ToJS()
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }

        public virtual IEnumerable<IRulesGroup> ValidatorRules()
        {
            return null;
        }

        public virtual string Validate(ValidateSettings setttings)
        {
            IClientValidatorVisitor visitor = ClientValidatorVisitorFactory.Create();

            return visitor.VisitValidator(ValidatorRules(), setttings);
        }
    }
}
