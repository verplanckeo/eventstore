using System.Collections.Generic;
using System.Linq;

namespace EventStore.Core.DddSeedwork
{
    public abstract class ValueObject
    {
        /// <summary>
        /// This is needed as salt for index. If only index was used, there is a chance that i ^i+some_low_number produces the same value.
        /// source: github.com/aneshas/tactical-ddd/blob/master/tactical.DDD/valueobject.cs
        /// </summary>
        private const int Prime = 77477; //Palindromic prime value (This means all digits except the middle digit are equal.)

        /// <summary>
        /// Override GetAtomicValues in order to implement structural equality for your value objects.
        /// </summary>
        /// <returns>Enumerable of properties to participate in equality comparison.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select((x, i) => (x != null ? x.GetHashCode() : 0) + (Prime * i))
                .Aggregate((x, y) => x ^ y);
        }

        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }

        public bool Equals(ValueObject obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            return GetHashCode() == obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;

            return Equals((ValueObject) obj);
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left?.Equals(right) ?? ReferenceEquals(right, null);
        }

        public static bool operator != (ValueObject left, ValueObject right) => !(left == right);
    }
}