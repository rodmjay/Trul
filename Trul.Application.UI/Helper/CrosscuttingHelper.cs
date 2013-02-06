using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trul.Infrastructure.Crosscutting.FormsAuthenticationService;
using Trul.Infrastructure.Crosscutting.Logging;
using Trul.Infrastructure.Crosscutting.NetFramework.Logging;
using Trul.Infrastructure.Crosscutting.NetFramework.Rules;
using Trul.Infrastructure.Crosscutting.Rules;
using Trul.Infrastructure.Crosscutting.Security;
using Trul.Service;
using Trul.Service.Core;

namespace Trul.Application.UI.Helper
{
    public class CrosscuttingHelper
    {
        public static void Initialise() {
            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            AuthenticationFactory.SetCurrent(new FormsAuthenticationFactory());
            ClientValidatorVisitorFactory.SetCurrent(new JQueryValidatorVisitorFactory());
        }
    }
}