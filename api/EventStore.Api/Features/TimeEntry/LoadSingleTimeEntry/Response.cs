using EventStore.Application.Features.TimeEntry;

namespace EventStore.Api.Features.TimeEntry.LoadSingleTimeEntry;

/// <summary>
/// Response returned for fetching a single logged time entry.
/// </summary>
public class Response
{
    /// <summary>
    /// Specific time entry
    /// </summary>
    public ReadTimeEntryModel TimeEntry { get; set; }
}