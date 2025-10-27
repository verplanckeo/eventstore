using MediatR;

namespace EventStore.Application.Features.TimeEntry.LoadAllEntries;

public class LoadAllTimeEntriesMediatorQuery : IRequest<LoadAllTimeEntriesMediatorQueryResult>
{
    public string UserId { get; set; }
    
    public static LoadAllTimeEntriesMediatorQuery Create(string userId) => new() { UserId = userId };
}