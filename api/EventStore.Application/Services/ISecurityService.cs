using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.User;

namespace EventStore.Application.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Generate a JSON WebToken for the authenticated user.
        /// </summary>
        /// <param name="user">User for which the token is to be generated.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Json WebToken</returns>
        Task<string> GenerateJsonWebTokenAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Verify if the given access token is a valid JWT token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsJsonWebTokenValidAsync(string token, CancellationToken cancellationToken);

        /// <summary>
        /// Generate a hash from the given password. The salt is generated in this method.
        /// </summary>
        /// <param name="password">Original password the user entered in the application.</param>
        /// <returns>Hashed password and salt used to hash.</returns>
        Task<(string hashedPassword, string salt)> GenerateHashedPassword(string password);

        /// <summary>
        /// Generate a hash from a given password with the given salt.
        /// </summary>
        /// <param name="password">Original password the user entered in the application.</param>
        /// <param name="salt">Salt used to generate hash.</param>
        /// <returns>Hashed password.</returns>
        Task<string> GenerateHashedPassword(string password, string salt);
    }
}
