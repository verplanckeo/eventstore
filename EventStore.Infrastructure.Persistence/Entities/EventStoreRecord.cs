using System;
using System.ComponentModel;

namespace EventStore.Infrastructure.Persistence.Entities
{
    public class EventStoreRecord
    {
        /// <summary>
        /// Id of event store record
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// JSon serialized payload of the event
        /// </summary>
        public string Data { get; set; }
        
        /// <summary>
        /// Version of AggregateRoot
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// When was the event executed
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Sequence how records were added in db (Don't touch this property, database auto increments this value)
        /// </summary>
        [Description("Don't touch this property, database auto increments this value")]
        public int Sequence { get; set; }

        /// <summary>
        /// Name of DomainEvent
        /// </summary>
        public string DomainEventName { get; set; }

        /// <summary>
        /// Name of the aggregate root
        /// </summary>
        public string AggregateName { get; set; }

        /// <summary>
        /// Identifier of AggregateRoot
        /// </summary>
        public string AggregateRootId { get; set; }
    }
}