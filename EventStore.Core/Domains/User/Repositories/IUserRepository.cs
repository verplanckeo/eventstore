using System.Threading.Tasks;

namespace EventStore.Core.Domains.User.Repositories
{
    public interface IUserRepository
    {
        Task<UserId> SaveUserAsync(User user);

        Task<User> LoadUserAsync(string id);
    }
}