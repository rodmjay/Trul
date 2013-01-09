using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Infrastructure.Crosscutting.Validator
{
    /// <summary>
    /// Base contract for entity validator abstract factory
    /// </summary>
    public interface IEntityValidatorFactory
    {
        /// <summary>
        /// Create a new IEntityValidator
        /// </summary>
        /// <returns>IEntityValidator</returns>
        IEntityValidator Create();
    }
}
