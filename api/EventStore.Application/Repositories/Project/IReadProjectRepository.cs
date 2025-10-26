using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Project;

namespace EventStore.Application.Repositories.Project;

public interface IReadProjectRepository
{
    /// <summary>
    /// Save or update ReadProject model
    /// </summary>
    /// <param name="readProject">Model to save</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> SaveOrUpdateProjectAsync(ReadProjectModel readProject, CancellationToken cancellationToken = default);

    /// <summary>
    /// Load all projects from our system
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation</param>
    /// <returns></returns>
    Task<IEnumerable<ReadProjectModel>> LoadProjectsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Load a given project from our system
    /// </summary>
    /// <param name="aggregateRootId">Id of the project.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ReadProjectModel> LoadProjectByAggregateRootIdAsync(string aggregateRootId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Load a given project by code from our system
    /// </summary>
    /// <param name="code">Code of the project.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ReadProjectModel> LoadProjectByCodeAsync(string code, CancellationToken cancellationToken = default);
}