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
        return await LoadProjectAsync(projectId, cancellationToken);
    }

    public async Task<Core.Domains.Project.Project> LoadProjectAsync(ProjectId id, CancellationToken cancellationToken)
    {
        var events = await _eventStoreRepository.LoadAsync(id, cancellationToken);
        return new Core.Domains.Project.Project(events);
    }

    public async Task<ProjectId> SaveProjectAsync(Core.Domains.Project.Project project, CancellationToken cancellationToken)
    {
        await _eventStoreRepository.SaveAsync(project.Id, project.Version, project.DomainEvents, "ProjectAggregateRoot", cancellationToken);
        return project.Id;
    }
}