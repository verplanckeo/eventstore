using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Application.Repositories.User;
using EventStore.Core.Domains.User;

namespace EventStore.Infrastructure.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public UserRepository(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Core.Domains.User.User> LoadUserAsync(string id, CancellationToken cancellationToken)
        {
            var userId = new UserId(id);
            return await LoadUserAsync(userId, cancellationToken);
        }

        public async Task<Core.Domains.User.User> LoadUserAsync(UserId id, CancellationToken cancellationToken)
        {
            var events = await _eventStoreRepository.LoadAsync(id, cancellationToken);
            return new Core.Domains.User.User(events);
        }

        public async Task<UserId> SaveUserAsync(Core.Domains.User.User user, CancellationToken cancellationToken)
        {
            await _eventStoreRepository.SaveAsync(user.Id, user.Version, user.DomainEvents, "UserAggregateRoot", cancellationToken);
            return user.Id;
        }
    }
}