using System;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.TimeEntry.DomainEvents;

public class TimeEntryChangedDomainEvent : DomainEvent
{
    public DateTime From { get; private set; }
    public DateTime Until { get; private set; }
    public string UserId { get; private set; }
    public string ProjectId { get; private set; }
    public ActivityTypes ActivityType { get; private set; }
    public string Comment { get; private set; }

    private TimeEntryChangedDomainEvent(
        string aggregateRootId,
        DateTime from,
        DateTime until,
        string userId,
        string projectId,
        ActivityTypes activityType,
        string comment) : base()
    {
        From = from;
        Until = until;
        UserId = userId;
        ProjectId = projectId;
        ActivityType = activityType;
        Comment = comment;
    }

    public static TimeEntryChangedDomainEvent CreateEvent(
        string aggregateRootId,
        DateTime from,
        DateTime until,
        string userId,
        string projectId,
        ActivityTypes activityType,
        string comment)
    {
        return new TimeEntryChangedDomainEvent(
            aggregateRootId,
            from,
            until,
            userId,
            projectId,
            activityType,
            comment);
    }
}