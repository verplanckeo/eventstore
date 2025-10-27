using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Application.Repositories.TimeEntry;
using EventStore.Core.Domains.TimeEntry;

namespace EventStore.Infrastructure.Persistence.Repositories.TimeEntry;

public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public TimeEntryRepository(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Core.Domains.TimeEntry.TimeEntry> LoadAsync(string id, CancellationToken cancellationToken)
        {
            var timeEntryId = new TimeEntryId(id);
            var events = await _eventStoreRepository.LoadAsync(timeEntryId, cancellationToken);
            return new Core.Domains.TimeEntry.TimeEntry(events);
        }

        public async Task<TimeEntryId> SaveAsync(Core.Domains.TimeEntry.TimeEntry timeEntry, CancellationToken cancellationToken)
        {
            await _eventStoreRepository.SaveAsync(timeEntry.Id, timeEntry.Version, timeEntry.DomainEvents, "TimeEntryAggregateRoot", cancellationToken);
            return timeEntry.Id;
        }
    }