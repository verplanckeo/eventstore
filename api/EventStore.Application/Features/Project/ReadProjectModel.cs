namespace EventStore.Application.Features.Project;

/// <summary>
/// Basic representation of a project record
/// </summary>
public class ReadProjectModel
{
    public string AggregateRootId { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool Billable { get; private set; }
    public bool IsRemoved { get; private set; }
    public int Version { get; private set; }

    /// <summary>
    /// DO NOT USE THIS CTOR - It is only here for unit tests
    /// </summary>
    public ReadProjectModel() { }

    private ReadProjectModel(string aggregateRootId, string name, string code, bool billable, bool isRemoved, int version)
    {
        AggregateRootId = aggregateRootId;
        Name = name;
        Code = code;
        Billable = billable;
        IsRemoved = isRemoved;
        Version = version;
    }

    public void ChangeProjectModel(ReadProjectModel updated)
    {
        Name = updated.Name;
        Code = updated.Code;
        Billable = updated.Billable;
        IsRemoved = updated.IsRemoved;
        Version++;
    }

    public static ReadProjectModel CreateNewReadProject(string aggregateRootId, string name, string code, bool billable, bool isRemoved, int version)
    {
        return new ReadProjectModel(aggregateRootId, name, code, billable, isRemoved, version);
    }
}