namespace EventStore.Application.Features.TimeEntry.UpdateReadTimeEntry;

public class MarkReadTimeEntryAsRemovedCommand(string aggregateRootId, int version)
{
    public int Version { get; set; } = version;
    public string AggregateRootId { get; set; } = aggregateRootId;
    
    public static MarkReadTimeEntryAsRemovedCommand Create(string aggregateRootId, int version) => new MarkReadTimeEntryAsRemovedCommand(aggregateRootId, version);
}