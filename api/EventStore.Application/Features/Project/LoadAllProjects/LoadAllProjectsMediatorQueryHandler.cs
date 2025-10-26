using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.Project;
using MediatR;

namespace EventStore.Application.Features.Project.LoadAllProjects;

public class LoadAllProjectsMediatorQueryHandler : IRequestHandler<LoadAllProjectsMediatorQuery, LoadAllProjectsMediatorQueryResult>
{
    private readonly IReadProjectRepository _repository;

    public LoadAllProjectsMediatorQueryHandler(IReadProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoadAllProjectsMediatorQueryResult> Handle(LoadAllProjectsMediatorQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.LoadProjectsAsync(cancellationToken);

        return LoadAllProjectsMediatorQueryResult.CreateResult(projects);
    }
}