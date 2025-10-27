namespace EventStore.Api.Features.TimeEntry.Update;

/// <summary>
/// Response returned after modifying a time entry.
/// </summary>
public class Response
{
    /// <summary>
    /// Time entry that was modified.
    /// </summary>
    public string TimeEntryId { get; set; }
}