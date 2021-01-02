using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Core.DddSeedwork;
using EventStore.Infrastructure.Persistence.Entities;
using Newtonsoft.Json;

namespace EventStore.Infrastructure.Persistence.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IDatabaseContext _dbContext;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };

        public EventStoreRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(IEntityId aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName, CancellationToken cancellationToken)
        {
            if (!events.Any()) return;

            var records = events.Select(evt => EventStoreRecord.CreateRecord(
                recordId : Guid.NewGuid(),
                serializedData: JsonConvert.SerializeObject(evt, Formatting.Indented, _jsonSettings),
                version: ++originatingVersion,
                createdAt: evt.CreatedAt,
                domainEventName: evt.GetType().Name,
                aggregateName: aggregateName,
                aggregateRootId: aggregateId.ToString()
            ));

            await _dbContext.EventStoreRecords.AddRangeAsync(records, cancellationToken);
        }

        public Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}