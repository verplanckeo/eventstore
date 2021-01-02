using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Core.DddSeedwork;

namespace EventStore.Infrastructure.Persistence.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public Task SaveAsync(IEntityId aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName = "Default Aggregate")
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId)
        {
            throw new System.NotImplementedException();
        }
    }
}