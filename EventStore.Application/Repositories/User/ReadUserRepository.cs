using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Application.Entities.User;

namespace EventStore.Application.Repositories.User
{
    public class ReadUserRepository : IReadUserRepository
    {
        public Task<string> SaveUserAsync(ReadUser readUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ReadUser>> LoadUsersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}