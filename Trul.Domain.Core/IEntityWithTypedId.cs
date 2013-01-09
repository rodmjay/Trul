using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Trul.Domain.Core
{
    public interface IEntityWithTypedId<TId>
    {
        TId ID { get; set; }

        bool IsTransient();
    }
}
