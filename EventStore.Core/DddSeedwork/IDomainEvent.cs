using System;

namespace EventStore.Core.DddSeedwork
{
    public interface IDomainEvent
    {
        DateTime CreatedAt { get; set; }
    }
}