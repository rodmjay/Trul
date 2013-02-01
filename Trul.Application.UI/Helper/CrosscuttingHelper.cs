using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trul.Infrastructure.Crosscutting.Logging;
using Trul.Infrastructure.Crosscutting.NetFramework.Logging;

namespace Trul.Application.UI.Helper
{
    public class CrosscuttingHelper
    {
        public static void Initialise() {
            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
        }
    }
}