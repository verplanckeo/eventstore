using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User;

namespace EventStore.Application.Repositories.User
{
    public interface IReadUserRepository
    {
        Task<string> SaveOrUpdateUserAsync(ReadUserModel readUser, CancellationToken cancellationToken = default);

        Task<IEnumerable<ReadUserModel>> LoadUsersAsync(CancellationToken cancellationToken = default);
    }
}