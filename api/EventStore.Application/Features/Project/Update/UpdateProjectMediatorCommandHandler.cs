using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Project.UpdateReadProject;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.Project;
using MediatR;

namespace EventStore.Application.Features.Project.Update;

public class UpdateProjectMediatorCommandHandler : IRequestHandler<UpdateProjectMediatorCommand, UpdateProjectMediatorCommandResponse>
{
    private readonly IProjectRepository _repository;
    private readonly IMediatorFactory _mediatorFactory;

    public UpdateProjectMediatorCommandHandler(IProjectRepository repository, IMediatorFactory mediatorFactory)
    {
        _repository = repository;
        _mediatorFactory = mediatorFactory;
    }

    public async Task<UpdateProjectMediatorCommandResponse> Handle(UpdateProjectMediatorCommand request, CancellationToken cancellationToken)
    {
        var scope = _mediatorFactory.CreateScope();

        var domainProject = await _repository.LoadProjectAsync(request.AggregateRootId, cancellationToken);
        domainProject.ChangeProject(request.Name, request.Code, request.Billable);

        var id = await _repository.SaveProjectAsync(domainProject, cancellationToken);

        await scope.SendAsync(UpdateReadProjectCommand.CreateCommand(
                id.ToString(), 
                request.Name, 
                request.Code, 
                request.Billable,
                domainProject.IsRemoved,
                domainProject.Version), 
            cancellationToken);

        return UpdateProjectMediatorCommandResponse.CreateResponse(id.ToString());
    }
}