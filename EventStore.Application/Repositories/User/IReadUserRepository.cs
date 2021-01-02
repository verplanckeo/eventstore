using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Application.Entities.User;

namespace EventStore.Application.Repositories.User
{
    public interface IReadUserRepository
    {
        Task<string> SaveUserAsync(ReadUser readUser);

        Task<IEnumerable<ReadUser>> LoadUsersAsync();
    }
}