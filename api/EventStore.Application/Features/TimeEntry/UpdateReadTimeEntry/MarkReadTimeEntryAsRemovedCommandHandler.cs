using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.UpdateReadTimeEntry;

public class MarkReadTimeEntryAsRemovedCommandHandler(IReadTimeEntryRepository repository)
    : IRequestHandler<UpdateReadTimeEntryCommand>
{
    public async Task Handle(UpdateReadTimeEntryCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.MarkTimeEntryAsRemovedAsync(request.AggregateRootId, request.Version, cancellationToken);

        if (string.IsNullOrEmpty(result))
        {
            //could not save / update read model
        }
    }
}