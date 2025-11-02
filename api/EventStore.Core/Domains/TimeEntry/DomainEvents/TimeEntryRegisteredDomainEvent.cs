using System;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.TimeEntry.DomainEvents;

public class TimeEntryRegisteredDomainEvent : DomainEvent
{
    public string TimeEntryId { get; set; }
    public DateTimeOffset From { get; private set; }
    public DateTimeOffset Until { get; private set; }
    public string UserId { get; private set; }
    public string ProjectId { get; private set; }
    public ActivityTypes ActivityType { get; private set; }
    public string Comment { get; private set; }

    public TimeEntryRegisteredDomainEvent(
        string aggregateRootId,
        DateTimeOffset from,
        DateTimeOffset until,
        string userId,
        string projectId,
        ActivityTypes activityType,
        string comment) : base()
    {
        TimeEntryId = aggregateRootId;
        From = from;
        Until = until;
        UserId = userId;
        ProjectId = projectId;
        ActivityType = activityType;
        Comment = comment;
    }

    public static TimeEntryRegisteredDomainEvent CreateEvent(
        string aggregateRootId,
        DateTimeOffset from,
        DateTimeOffset until,
        string userId,
        string projectId,
        ActivityTypes activityType,
        string comment)
    {
        return new TimeEntryRegisteredDomainEvent(
            aggregateRootId,
            from,
            until,
            userId,
            projectId,
            activityType,
            comment);
    }
}