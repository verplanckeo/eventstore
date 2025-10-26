using MediatR;

namespace EventStore.Application.Features.Project.LoadSingleProject;

/// <summary>
/// Query to retrieve data of a single project.
/// </summary>
public class LoadSingleProjectMediatorQuery : IRequest<LoadSingleProjectMediatorQueryResult>
{
    /// <summary>
    /// AggregateRootId
    /// </summary>
    public string AggregateRootId { get; set; }

    /// <summary>
    /// Create instance of <see cref="LoadSingleProjectMediatorQuery"/>
    /// </summary>
    /// <param name="aggregateRootId"></param>
    /// <returns></returns>
    public static LoadSingleProjectMediatorQuery CreateQuery(string aggregateRootId) => new LoadSingleProjectMediatorQuery
        { AggregateRootId = aggregateRootId };
}