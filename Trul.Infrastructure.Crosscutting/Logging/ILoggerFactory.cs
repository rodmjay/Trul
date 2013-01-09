using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Infrastructure.Crosscutting.Logging
{
    /// <summary>
    /// Base contract for Log abstract factory
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create a new ILog
        /// </summary>
        /// <returns>The ILog created</returns>
        ILogger Create();
    }
}
