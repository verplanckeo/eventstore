using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Application.Repositories.Project;
using EventStore.Core.Domains.Project;

namespace EventStore.Infrastructure.Persistence.Repositories.Project;

public class ProjectRepository : IProjectRepository
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public ProjectRepository(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task<Core.Domains.Project.Project> LoadProjectAsync(string id, CancellationToken cancellationToken)
    {
        var projectId = new ProjectId(id);
        var events = await _eventStoreRepository.LoadAsync(projectId, cancellationToken);
        return new Core.Domains.Project.Project(events);
    }

    public async Task<ProjectId> SaveProjectAsync(Core.Domains.Project.Project project, CancellationToken cancellationToken)
    {
        await _eventStoreRepository.SaveAsync(project.Id, project.Version, project.DomainEvents, "ProjectAggregateRoot", cancellationToken);
        return project.Id;
    }
}