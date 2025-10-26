using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Project.UpdateReadProject;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.Project;
using MediatR;

namespace EventStore.Application.Features.Project.Remove;

public class RemoveProjectMediatorCommandHandler : IRequestHandler<RemoveProjectMediatorCommand, RemoveProjectMediatorCommandResponse>
{
    private readonly IProjectRepository _repository;
    private readonly IMediatorFactory _mediatorFactory;

    public RemoveProjectMediatorCommandHandler(IProjectRepository repository, IMediatorFactory mediatorFactory)
    {
        _repository = repository;
        _mediatorFactory = mediatorFactory;
    }

    public async Task<RemoveProjectMediatorCommandResponse> Handle(RemoveProjectMediatorCommand request, CancellationToken cancellationToken)
    {
        var scope = _mediatorFactory.CreateScope();

        var domainProject = await _repository.LoadProjectAsync(request.AggregateRootId, cancellationToken);
        domainProject.RemoveProject();

        var id = await _repository.SaveProjectAsync(domainProject, cancellationToken);

        await scope.SendAsync(UpdateReadProjectCommand.CreateCommand(
                id.ToString(), 
                domainProject.Name, 
                domainProject.Code, 
                domainProject.Billable,
                domainProject.IsRemoved,
                domainProject.Version), 
            cancellationToken);

        return RemoveProjectMediatorCommandResponse.CreateResponse(id.ToString());
    }
}