namespace EventStore.Application.Features.Project.Register;

public class RegisterProjectMediatorCommandResponse
{
    public string Id { get; private set; }

    private RegisterProjectMediatorCommandResponse(string id)
    {
        Id = id;
    }

    public static RegisterProjectMediatorCommandResponse CreateResponse(string id)
    {
        return new RegisterProjectMediatorCommandResponse(id);
    }
}