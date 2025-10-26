using MediatR;

namespace EventStore.Application.Features.User.LoadSingleUser;

/// <summary>
/// Query to retrieve data of a single user.
/// </summary>
public class LoadSingleUserMediatorQuery : IRequest<LoadSingleUserMediatorQueryResult>
{
    /// <summary>
    /// Username
    /// </summary>
    public string AggregateRootId { get; set; }

    /// <summary>
    /// Create instance of <see cref="LoadSingleUserMediatorQuery"/>
    /// </summary>
    /// <param name="aggregateRootId"></param>
    /// <returns></returns>
    public static LoadSingleUserMediatorQuery CreateQuery(string aggregateRootId) => new LoadSingleUserMediatorQuery
        { AggregateRootId = aggregateRootId };
}