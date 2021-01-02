using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Core.DddSeedwork;

namespace EventStore.Infrastructure.Persistence.Repositories
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(IEntityId aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName = "Default Aggregate");

        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId);
    }
}