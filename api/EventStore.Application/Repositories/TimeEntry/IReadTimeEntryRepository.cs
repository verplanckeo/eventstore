using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.TimeEntry;
using EventStore.Core.Domains.TimeEntry;
using EventStore.Core.Domains.User;

namespace EventStore.Application.Repositories.TimeEntry;

public interface IReadTimeEntryRepository
{
    Task<string> SaveOrUpdateTimeEntryAsync(ReadTimeEntryModel timeEntry, CancellationToken cancellationToken);
    Task<string> MarkTimeEntryAsRemovedAsync(string aggregateRootId, int version, CancellationToken cancellationToken);
    Task<List<ReadTimeEntryModel>> LoadAllActiveTimeEntriesAsync(CancellationToken cancellationToken);
    Task<List<ReadTimeEntryModel>> LoadAllActiveTimeEntriesForUserAsync(UserId userId, CancellationToken cancellationToken);
    Task<ReadTimeEntryModel> LoadTimeEntryByIdAsync(string aggregateRootId, CancellationToken cancellationToken);
    
    Task<ReadTimeEntryModel> LoadTimeEntryByIdAsync(TimeEntryId aggregateRootId, CancellationToken cancellationToken);
}