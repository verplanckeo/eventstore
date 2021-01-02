using System.Collections;
using System.Collections.Generic;

namespace EventStore.Core.DddSeedwork
{
    public abstract class EntityId : ValueObject, IEntityId
    {
        /// <summary>
        /// Return string representation of this value object.
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ToString();
        }
    }
}