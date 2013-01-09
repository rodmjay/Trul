using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Trul.Domain.Core
{
    [Serializable]
    public abstract class EntityWithTypedId<TId> : IEntityWithTypedId<TId>
    {
        private const int HashMultiplier = 31;

        private int? cachedHashcode;

        public virtual TId ID { get; set; }

        public override int GetHashCode() {
            if(this.cachedHashcode.HasValue) {
                return this.cachedHashcode.Value;
            }

            if(this.IsTransient()) {
                this.cachedHashcode = base.GetHashCode();
            } else {
                unchecked {
                    var hashCode = this.GetType().GetHashCode();
                    this.cachedHashcode = (hashCode * HashMultiplier) ^ this.ID.GetHashCode();
                }
            }

            return this.cachedHashcode.Value;
        }

        public virtual bool IsTransient() {
            return this.ID == null || this.ID.Equals(default(TId));
        }

        private bool HasSameNonDefaultIdAs(EntityWithTypedId<TId> compareTo) {
            return !this.IsTransient() && !compareTo.IsTransient() && this.ID.Equals(compareTo.ID);
        }
    }
}
