using System;
using EventStore.Core.Domains.TimeEntry;

namespace EventStore.Infrastructure.Persistence.Entities.TimeEntry;

public class ReadTimeEntry
{
    public string AggregateRootId { get; set; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset Until { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string ProjectId { get; set; }
    public string ProjectCode { get; set; }
    public ActivityTypes ActivityType { get; set; }
    public string Comment { get; set; }
    public bool IsRemoved { get; set; }
    public int Version { get; set; }
}