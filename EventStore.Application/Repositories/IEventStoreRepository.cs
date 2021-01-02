using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.DddSeedwork;

namespace EventStore.Application.Repositories
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(IEntityId aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId, CancellationToken cancellationToken);
    }
}