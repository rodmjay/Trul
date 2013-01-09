using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Application
{
    [Serializable]
    public class BaseObjectDTO : BaseObjectDTOWithTypeId<int>
    { 
    
    }

    [Serializable]
    public abstract class BaseObjectDTOWithTypeId<TId> : IBaseObjectWithTypeId<TId>
    {
        public virtual TId ID { get; set; }
    }

    public interface IBaseObjectWithTypeId<TId>
    {
        TId ID { get; set; }
    }
}
