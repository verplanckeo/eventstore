using System.Threading.Tasks;
using EventStore.Core.Domains.User;
using EventStore.Infrastructure.Persistence.Repositories;

namespace EventStore.Application.Repositories.User
{
    public class UserRepository : Core.Domains.User.Repositories.IUserRepository
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public UserRepository(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Core.Domains.User.User> LoadUserAsync(string id)
        {
            var userId = new UserId(id);
            var events = await _eventStoreRepository.LoadAsync(userId);
            return new Core.Domains.User.User(events);
        }

        public async Task<UserId> SaveUserAsync(Core.Domains.User.User user)
        {
            await _eventStoreRepository.SaveAsync(user.Id, user.Version, user.DomainEvents, "UserAggregateRoot");
            return user.Id;
        }
    }
}