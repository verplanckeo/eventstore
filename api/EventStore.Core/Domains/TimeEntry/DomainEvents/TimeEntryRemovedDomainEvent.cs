using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.TimeEntry.DomainEvents;

public class TimeEntryRemovedDomainEvent : DomainEvent
{
    private TimeEntryRemovedDomainEvent() : base()
    {
    }

    public static TimeEntryRemovedDomainEvent CreateEvent()
    {
        return new TimeEntryRemovedDomainEvent();
    }
}