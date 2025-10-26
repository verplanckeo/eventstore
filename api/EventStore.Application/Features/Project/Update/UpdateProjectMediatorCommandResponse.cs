namespace EventStore.Application.Features.Project.Update;

public class UpdateProjectMediatorCommandResponse
{
    public string Id { get; private set; }

    private UpdateProjectMediatorCommandResponse(string id)
    {
        Id = id;
    }

    public static UpdateProjectMediatorCommandResponse CreateResponse(string id)
    {
        return new UpdateProjectMediatorCommandResponse(id);
    }
}