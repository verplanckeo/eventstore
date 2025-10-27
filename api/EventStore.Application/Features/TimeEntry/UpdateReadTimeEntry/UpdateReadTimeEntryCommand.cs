using System;
using EventStore.Core.Domains.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.UpdateReadTimeEntry;

public class UpdateReadTimeEntryCommand : IRequest
{
    public string AggregateRootId { get; set; }
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string ProjectId { get; set; }
    public string ProjectCode { get; set; }
    public ActivityTypes ActivityType { get; set; }
    public string Comment { get; set; }
    
    public bool IsRemoved { get; set; }
    
    public int Version { get; set; }
    
    public UpdateReadTimeEntryCommand(string aggregateRootId, DateTime from, DateTime until, string userId, string userName, string projectId, string projectCode, ActivityTypes activityType, string comment, bool isRemoved, int version)
    {
        AggregateRootId = aggregateRootId;
        From = from;
        Until = until;
        UserId = userId;
        UserName = userName;
        ProjectId = projectId;
        ProjectCode = projectCode;
        ActivityType = activityType;
        Comment = comment;
        IsRemoved = isRemoved;
        Version = version;
    }
    
    public static UpdateReadTimeEntryCommand CreateCommand(string aggregateRootId, DateTime from, DateTime until, string userId, string userName, string projectId, string projectCode, ActivityTypes activityType, string comment, bool isRemoved, int version)
    {
        return new UpdateReadTimeEntryCommand(aggregateRootId, from, until, userId, userName, projectId, projectCode, activityType, comment, isRemoved, version);
    }
}