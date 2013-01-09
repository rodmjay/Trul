using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Application.UI.Core.Models
{
    [Serializable]
    public abstract class ViewModelBase
    {
        public virtual string ToJS() {
           return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
        }
    }
}
