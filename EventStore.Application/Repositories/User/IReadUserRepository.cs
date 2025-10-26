using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User;

namespace EventStore.Application.Repositories.User
{
    public interface IReadUserRepository
    {
        /// <summary>
        /// Save or update ReadUser model
        /// </summary>
        /// <param name="readUser">Model to save</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> SaveOrUpdateUserAsync(ReadUserModel readUser, CancellationToken cancellationToken = default);

        /// <summary>
        /// Load all users from our system
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation</param>
        /// <returns></returns>
        Task<IEnumerable<ReadUserModel>> LoadUsersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Load a given user from our system
        /// </summary>
        /// <param name="userName">Username of the user we wish to return</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ReadUserModel> LoadUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Load a given user from our system
        /// </summary>
        /// <param name="aggregateRootId">Id of the user.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ReadUserModel> LoadUserByAggregateRootIdAsync(string aggregateRootId, CancellationToken cancellationToken = default);
    }
}