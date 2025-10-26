using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.Project;
using MediatR;

namespace EventStore.Application.Features.Project.LoadSingleProject;

public class LoadSingleProjectMediatorQueryHandler : IRequestHandler<LoadSingleProjectMediatorQuery, LoadSingleProjectMediatorQueryResult>
{
    private readonly IReadProjectRepository _repository;

    public LoadSingleProjectMediatorQueryHandler(IReadProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoadSingleProjectMediatorQueryResult> Handle(LoadSingleProjectMediatorQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.LoadProjectByAggregateRootIdAsync(request.AggregateRootId, cancellationToken);

        return LoadSingleProjectMediatorQueryResult.CreateResult(project);
    }
}