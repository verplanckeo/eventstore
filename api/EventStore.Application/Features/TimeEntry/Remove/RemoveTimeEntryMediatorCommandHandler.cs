using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.TimeEntry.UpdateReadTimeEntry;
using EventStore.Application.Repositories.TimeEntry;
using EventStore.Core.Domains.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.Remove;

public class RemoveTimeEntryMediatorCommandHandler : IRequestHandler<RemoveTimeEntryMediatorCommand, RemoveTimeEntryMediatorCommandResponse>
{
    private readonly ITimeEntryRepository _repository;
    private readonly IMediator _mediator;

    public RemoveTimeEntryMediatorCommandHandler(ITimeEntryRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<RemoveTimeEntryMediatorCommandResponse> Handle(RemoveTimeEntryMediatorCommand request, CancellationToken cancellationToken)
    {
        var timeEntryId = await _repository.LoadAsync(request.TimeEntryId, cancellationToken);

        if (timeEntryId == null)
            throw new System.Exception($"TimeEntry with id {request.TimeEntryId} not found.");

        timeEntryId.RemoveTimeEntry();

        await _repository.SaveAsync(timeEntryId, cancellationToken);

        // Mark as removed in read model
        var markAsRemovedCommand = MarkReadTimeEntryAsRemovedCommand.Create(
            timeEntryId.ToString(),
            timeEntryId.Version);

        await _mediator.Send(markAsRemovedCommand, cancellationToken);

        return RemoveTimeEntryMediatorCommandResponse.CreateResponse(timeEntryId.ToString());
    }
}