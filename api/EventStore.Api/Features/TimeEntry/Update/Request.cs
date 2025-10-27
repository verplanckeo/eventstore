using System;
using EventStore.Core.Domains.TimeEntry;

namespace EventStore.Api.Features.TimeEntry.Update;

/// <summary>
/// Request to modify existing time entry
/// </summary>
public class Request
{
    /// <summary>
    /// From when the given activity started
    /// </summary>
    public DateTime From { get; set; }
    
    /// <summary>
    /// Until when the given activity was executed
    /// </summary>
    public DateTime Until { get; set; }
    
    /// <summary>
    /// User for whom the time entry is applicable to.
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// For which project
    /// </summary>
    public string ProjectId { get; set; }
    
    /// <summary>
    /// What type of activity.
    /// <see cref="ActivityTypes"/>
    /// </summary>
    public ActivityTypes ActivityType { get; set; }
    
    /// <summary>
    /// Comment of the user to give more context.
    /// </summary>
    public string Comment { get; set; }
}