using MediatR;

namespace EventStore.Application.Features.TimeEntry.LoadSingleTimeEntry;

public class LoadSingleTimeEntryMediatorQuery : IRequest<LoadSingleTimeEntryMediatorQueryResult>
{
    public string TimeEntryId { get; set; }
}