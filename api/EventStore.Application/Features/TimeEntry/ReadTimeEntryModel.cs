using System;
using EventStore.Core.Domains.TimeEntry;

namespace EventStore.Application.Features.TimeEntry;

public class ReadTimeEntryUserModel(string id, string userName)
{
    public string Id { get; } = id;
    public string UserName { get; } = userName;

    public static ReadTimeEntryUserModel Create(string userId, string userName) =>
        new ReadTimeEntryUserModel(userId, userName);
}

public class ReadTimeEntryProjectModel(string id, string code)
{
    public string Id { get; }= id;
    public string Code { get; } = code;
    
    public static ReadTimeEntryProjectModel Create(string projectId, string code) => new ReadTimeEntryProjectModel(projectId, code);
}

public class ReadTimeEntryModel
{
    public string AggregateRootId { get; set; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset Until { get; set; }
    public ReadTimeEntryUserModel User { get; set; }
    public ReadTimeEntryProjectModel Project { get; set; }
    public ActivityTypes ActivityType { get; set; }
    public string Comment { get; set; }
    public bool IsRemoved { get; set; }
    public int Version { get; set; }

    public static ReadTimeEntryModel CreateNewReadTimeEntry(
        string aggregateRootId,
        DateTimeOffset from,
        DateTimeOffset until,
        string userId,
        string userName,
        string projectId,
        string projectCode,
        ActivityTypes activityType,
        string comment,
        bool isRemoved,
        int version)
    {
        return new ReadTimeEntryModel
        {
            AggregateRootId = aggregateRootId,
            From = from,
            Until = until,
            User = ReadTimeEntryUserModel.Create(userId, userName),
            Project = ReadTimeEntryProjectModel.Create(projectId, projectCode),
            ActivityType = activityType,
            Comment = comment,
            IsRemoved = isRemoved,
            Version = version
        };
    }
}