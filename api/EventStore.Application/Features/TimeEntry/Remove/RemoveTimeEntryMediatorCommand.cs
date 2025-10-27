using MediatR;

namespace EventStore.Application.Features.TimeEntry.Remove;

public class RemoveTimeEntryMediatorCommand : IRequest<RemoveTimeEntryMediatorCommandResponse>
{
    public string TimeEntryId { get; set; }
}