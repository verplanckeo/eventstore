using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.TimeEntry;

namespace EventStore.Application.Repositories.TimeEntry;

public interface ITimeEntryRepository
{
    Task<Core.Domains.TimeEntry.TimeEntry> LoadAsync(string id, CancellationToken cancellationToken);
    Task<TimeEntryId> SaveAsync(Core.Domains.TimeEntry.TimeEntry timeEntry, CancellationToken cancellationToken);
}