using MediatR;

namespace EventStore.Application.Features.Project.Register;

public class RegisterProjectMediatorCommand : IRequest<RegisterProjectMediatorCommandResponse>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool Billable { get; private set; }

    /// <summary>
    /// DO NOT USE THIS CTOR - It is only here for unit tests
    /// </summary>
    public RegisterProjectMediatorCommand()
    {
    }

    private RegisterProjectMediatorCommand(string name, string code, bool billable)
    {
        Name = name;
        Code = code;
        Billable = billable;
    }

    public static RegisterProjectMediatorCommand CreateCommand(string name, string code, bool billable)
    {
        return new RegisterProjectMediatorCommand(name, code, billable);
    }
}