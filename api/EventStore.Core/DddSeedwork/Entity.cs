using System.Collections.Generic;

namespace EventStore.Core.DddSeedwork
{
    /// <summary>
    /// Class used for entity uniqueness and Equality comparisons and hash code generation.
    /// </summary>
    public abstract class Entity<TIdentity> : IEntity<TIdentity> where TIdentity : IEntityId
    {
        /// <summary>
        /// Id defines entity uniqueness and is used for Equality
        /// comparisons and hash code generation.
        /// </summary>
        public abstract TIdentity Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true; // when this and referenced object have same reference, we're obviously talking about the same object
            if (obj.GetType() != GetType()) return false;

            return EqualityComparer<TIdentity>.Default.Equals(Id, ((Entity<TIdentity>) obj).Id);
        }

        public static bool operator ==(Entity<TIdentity> left, Entity<TIdentity> right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TIdentity> left, Entity<TIdentity> right) => !(left == right);

        public override int GetHashCode()
        {
            if (Id.Equals(default(TIdentity))) return base.GetHashCode();

            return GetType().GetHashCode() ^ Id.GetHashCode();
        }
    }
}