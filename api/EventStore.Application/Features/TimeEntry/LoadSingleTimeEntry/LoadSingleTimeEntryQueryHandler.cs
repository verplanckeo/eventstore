using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.TimeEntry;
using EventStore.Core.Domains.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.LoadSingleTimeEntry;

public class LoadSingleTimeEntryMediatorQueryHandler : IRequestHandler<LoadSingleTimeEntryMediatorQuery, LoadSingleTimeEntryMediatorQueryResult>
{
    private readonly IReadTimeEntryRepository _repository;

    public LoadSingleTimeEntryMediatorQueryHandler(IReadTimeEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoadSingleTimeEntryMediatorQueryResult> Handle(LoadSingleTimeEntryMediatorQuery request, CancellationToken cancellationToken)
    {
        var timeEntry = await _repository.LoadTimeEntryByIdAsync(new TimeEntryId(request.TimeEntryId), cancellationToken);

        if (timeEntry == null)
            return LoadSingleTimeEntryMediatorQueryResult.CreateResult(null);

        var result = ReadTimeEntryModel.CreateNewReadTimeEntry(
            timeEntry.AggregateRootId,
            timeEntry.From,
            timeEntry.Until,
            timeEntry.UserId,
            timeEntry.UserName,
            timeEntry.ProjectId,
            timeEntry.ProjectCode,
            timeEntry.ActivityType,
            timeEntry.Comment,
            timeEntry.IsRemoved,
            timeEntry.Version);

        return LoadSingleTimeEntryMediatorQueryResult.CreateResult(result);
    }
}