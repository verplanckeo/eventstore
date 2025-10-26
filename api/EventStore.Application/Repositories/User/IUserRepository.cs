using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.User;

namespace EventStore.Application.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserId> SaveUserAsync(Core.Domains.User.User user, CancellationToken cancellationToken);

        Task<Core.Domains.User.User> LoadUserAsync(string id, CancellationToken cancellationToken);
    }
}