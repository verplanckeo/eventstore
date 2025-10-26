namespace EventStore.Infrastructure.Persistence.Entities.Project;

/// <summary>
/// Basic representation of a project record
/// </summary>
public class ReadProject
{
    public string AggregateRootId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool Billable { get; set; }
    public bool IsRemoved { get; set; }
    public int Version { get; set; }
}