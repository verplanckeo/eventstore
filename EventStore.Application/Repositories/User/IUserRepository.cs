using System.Threading.Tasks;
using EventStore.Core.Domains.User;

namespace EventStore.Application.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserId> SaveUserAsync(Core.Domains.User.User user);

        Task<Core.Domains.User.User> LoadUserAsync(string id);
    }
}