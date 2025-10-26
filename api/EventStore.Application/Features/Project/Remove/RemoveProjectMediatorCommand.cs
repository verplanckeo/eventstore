using MediatR;

namespace EventStore.Application.Features.Project.Remove;

public class RemoveProjectMediatorCommand : IRequest<RemoveProjectMediatorCommandResponse>
{
    public string AggregateRootId { get; private set; }

    /// <summary>
    /// DO NOT USE THIS CTOR - It is only here for unit tests
    /// </summary>
    public RemoveProjectMediatorCommand()
    {
    }

    private RemoveProjectMediatorCommand(string aggregateRootId)
    {
        AggregateRootId = aggregateRootId;
    }

    public static RemoveProjectMediatorCommand CreateCommand(string aggregateRootId)
    {
        return new RemoveProjectMediatorCommand(aggregateRootId);
    }
}