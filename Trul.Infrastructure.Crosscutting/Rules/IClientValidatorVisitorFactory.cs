using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework.Rules;

namespace Trul.Infrastructure.Crosscutting.Rules
{
    public interface IClientValidatorVisitorFactory
    {
        IClientValidatorVisitor Create();
    }
}
