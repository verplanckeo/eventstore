using MediatR;

namespace EventStore.Application.Features.Project.LoadAllProjects;

/// <summary>
/// Query to load all registered projects.
/// </summary>
public class LoadAllProjectsMediatorQuery : IRequest<LoadAllProjectsMediatorQueryResult>
{
    /// <summary>
    /// Create new instance of <see cref="LoadAllProjectsMediatorQuery"/>
    /// </summary>
    /// <returns></returns>
    public static LoadAllProjectsMediatorQuery CreateQuery() => new LoadAllProjectsMediatorQuery();
}