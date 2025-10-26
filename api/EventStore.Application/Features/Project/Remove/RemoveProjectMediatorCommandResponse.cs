namespace EventStore.Application.Features.Project.Remove;

public class RemoveProjectMediatorCommandResponse
{
    public string Id { get; private set; }

    private RemoveProjectMediatorCommandResponse(string id)
    {
        Id = id;
    }

    public static RemoveProjectMediatorCommandResponse CreateResponse(string id)
    {
        return new RemoveProjectMediatorCommandResponse(id);
    }
}