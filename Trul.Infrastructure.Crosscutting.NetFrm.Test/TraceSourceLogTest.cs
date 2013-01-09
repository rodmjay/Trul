using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trul.Infrastructure.Crosscutting.Logging;
using Trul.Infrastructure.Crosscutting.NetFramework.Logging;

namespace Trul.Infrastructure.Crosscutting.NetFrm.Test
{
    [TestClass]
    public partial class TraceSourceLogTest
    {

        [ClassInitialize()]
        public static void ClassInitialze(TestContext context) {
            // Initialize default log factory
            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
        }


        [TestMethod]
        public void TraceSourceLogDebugTest() {
            ILogger log = LoggerFactory.CreateLog();
            log.Debug("debug test");
        }
    }
}
