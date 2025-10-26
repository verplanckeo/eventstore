using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.Project;

namespace EventStore.Application.Repositories.Project;

public interface IProjectRepository
{
    Task<ProjectId> SaveProjectAsync(Core.Domains.Project.Project project, CancellationToken cancellationToken);

    Task<Core.Domains.Project.Project> LoadProjectAsync(string id, CancellationToken cancellationToken);
}