using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Core.DddSeedwork;
using EventStore.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
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
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId, CancellationToken cancellationToken)
        {
            if(aggregateRootId == null) throw new AggregateException($"{nameof(aggregateRootId)} cannot be null");

            var events = await _dbContext.EventStoreRecords.Where(record => record.AggregateRootId == aggregateRootId.ToString())
                .OrderBy(record => record.Version).ToListAsync(cancellationToken).ConfigureAwait(false);

            return events.Select(Transform).ToList().AsReadOnly();
        }

        private IDomainEvent Transform(EventStoreRecord record)
        {
            var data = JsonConvert.DeserializeObject(record.Data, _jsonSettings);
            var evt = data as IDomainEvent;

            return evt;
        }
    }
}