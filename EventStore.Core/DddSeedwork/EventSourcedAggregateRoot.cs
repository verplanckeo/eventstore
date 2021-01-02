using System;
using System.Collections.Generic;

namespace EventStore.Core.DddSeedwork
{
    public abstract class EventSourcedAggregateRoot<TIdentity> : Entity<TIdentity>, IEventSourcedAggregateRoot<TIdentity> where TIdentity : IEntityId
    {
        public int Version { private set; get; }

        private readonly List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// CTor (default)
        /// </summary>
        protected EventSourcedAggregateRoot()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        /// <summary>
        /// CTor (when loading aggregate from db)
        /// </summary>
        /// <param name="events"></param>
        protected EventSourcedAggregateRoot(IEnumerable<IDomainEvent> events): this()
        {
            if (events == null) return;

            foreach (var domainEvent in events)
            {
                Mutate(domainEvent);
                Version++;
            }
        }

        protected void AddDomainEvent(IDomainEvent evt)
        {
            _domainEvents.Add(evt);
        }

        protected void RemoveDomainEvent(IDomainEvent evt) => _domainEvents.Remove(evt);

        protected void ClearDomainEvents() => _domainEvents.Clear();

        protected void Apply(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events)
            {
                Apply(domainEvent);
            }
        }

        protected void Apply(IDomainEvent evt)
        {
            Mutate(evt);
            AddDomainEvent(evt);
        }

        // This will look for the "On" method(s) within each class deriving from EventSourcedAggregateRoot
        // It's possible to have multiple On methods (one should be created for each DomainEvent that applies a transformation)
        private void Mutate(IDomainEvent evt) => ((dynamic) this).On((dynamic) evt); 
    }
}