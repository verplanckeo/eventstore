using System;

namespace EventStore.Core.DddSeedwork
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime CreatedAt { get; set; }

        public DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}