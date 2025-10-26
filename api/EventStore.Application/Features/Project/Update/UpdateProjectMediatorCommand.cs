using MediatR;

namespace EventStore.Application.Features.Project.Update;

public class UpdateProjectMediatorCommand : IRequest<UpdateProjectMediatorCommandResponse>
{
    public string AggregateRootId { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool Billable { get; private set; }

    /// <summary>
    /// DO NOT USE THIS CTOR - It is only here for unit tests
    /// </summary>
    public UpdateProjectMediatorCommand()
    {
    }

    private UpdateProjectMediatorCommand(string aggregateRootId, string name, string code, bool billable)
    {
        AggregateRootId = aggregateRootId;
        Name = name;
        Code = code;
        Billable = billable;
    }

    public static UpdateProjectMediatorCommand CreateCommand(string aggregateRootId, string name, string code, bool billable)
    {
        return new UpdateProjectMediatorCommand(aggregateRootId, name, code, billable);
    }
}