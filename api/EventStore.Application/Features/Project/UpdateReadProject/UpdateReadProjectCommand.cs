using MediatR;

namespace EventStore.Application.Features.Project.UpdateReadProject;

public class UpdateReadProjectCommand : IRequest
{
    public string AggregateRootId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool Billable { get; set; }
    public bool IsRemoved { get; set; }
    public int Version { get; set; }

    private UpdateReadProjectCommand(string aggregateRootId, string name, string code, bool billable, bool isRemoved, int version)
    {
        AggregateRootId = aggregateRootId;
        Name = name;
        Code = code;
        Billable = billable;
        IsRemoved = isRemoved;
        Version = version;
    }

    public static UpdateReadProjectCommand CreateCommand(string aggregateRootId, string name, string code, bool billable, bool isRemoved, int version)
    {
        return new UpdateReadProjectCommand(aggregateRootId, name, code, billable, isRemoved, version);
    }
}