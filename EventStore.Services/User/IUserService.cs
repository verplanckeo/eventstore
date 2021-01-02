using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Services.User.Models;

namespace EventStore.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userName">Username (i.e. firstname.lastname)</param>
        /// <param name="firstName">First name of user</param>
        /// <param name="lastName">Last name of user</param>
        /// <returns>Id of the generated user</returns>
        Task<string> RegisterUser(string userName, string firstName, string lastName);

        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="userId">Id of the user to return</param>
        /// <returns><see cref="UserModel"/></returns>
        Task<UserModel> LoadUser(string userId);

        /// <summary>
        /// Get a list of all users currently registered
        /// </summary>
        /// <returns>List of <see cref="ReadUserModel"/></returns>
        Task<IEnumerable<ReadUserModel>> LoadUsersOverview();
    }
}