using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.LoadAllEntries;

public class LoadAllTimeEntriesMediatorQueryHandler : IRequestHandler<LoadAllTimeEntriesMediatorQuery, LoadAllTimeEntriesMediatorQueryResult>
{
    private readonly IReadTimeEntryRepository _repository;

    public LoadAllTimeEntriesMediatorQueryHandler(IReadTimeEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoadAllTimeEntriesMediatorQueryResult> Handle(LoadAllTimeEntriesMediatorQuery request, CancellationToken cancellationToken)
    {
        List<ReadTimeEntryModel> timeEntries;
        if(!string.IsNullOrEmpty(request.UserId))
            timeEntries = await _repository.LoadAllActiveTimeEntriesForUserAsync(new Core.Domains.User.UserId(request.UserId), cancellationToken);
        else
            timeEntries = await _repository.LoadAllActiveTimeEntriesAsync(cancellationToken); ;

        return LoadAllTimeEntriesMediatorQueryResult.CreateResult(timeEntries.ToList());
    }
}