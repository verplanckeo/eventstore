using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Project.UpdateReadProject;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.Project;
using MediatR;

namespace EventStore.Application.Features.Project.Register;

public class RegisterProjectMediatorCommandHandler : IRequestHandler<RegisterProjectMediatorCommand, RegisterProjectMediatorCommandResponse>
{
    private readonly IProjectRepository _repository;
    private readonly IMediatorFactory _mediatorFactory;

    public RegisterProjectMediatorCommandHandler(IProjectRepository repository, IMediatorFactory mediatorFactory)
    {
        _repository = repository;
        _mediatorFactory = mediatorFactory;
    }

    public async Task<RegisterProjectMediatorCommandResponse> Handle(RegisterProjectMediatorCommand request, CancellationToken cancellationToken)
    {
        var scope = _mediatorFactory.CreateScope();

        var domainProject = Core.Domains.Project.Project.CreateNewProject(request.Name, request.Code, request.Billable);

        var id = await _repository.SaveProjectAsync(domainProject, cancellationToken);

        await scope.SendAsync(UpdateReadProjectCommand.CreateCommand(
                id.ToString(), 
                request.Name, 
                request.Code, 
                request.Billable, 
                false,
                domainProject.Version), 
            cancellationToken);

        return RegisterProjectMediatorCommandResponse.CreateResponse(id.ToString());
    }
}