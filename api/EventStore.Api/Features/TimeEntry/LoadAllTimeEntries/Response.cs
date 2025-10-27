using System.Collections.Generic;
using EventStore.Application.Features.TimeEntry;

namespace EventStore.Api.Features.TimeEntry.LoadAllTimeEntries;

/// <summary>
/// 
/// </summary>
public class Response
{
    /// <summary>
    /// All logged time entries
    /// </summary>
    public List<ReadTimeEntryModel> TimeEntries { get; set; }
}