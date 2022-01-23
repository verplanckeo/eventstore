using EventStore.Application.Features.User;
using System.Collections.Generic;

namespace EventStore.Api.Features.User.LoadAllUsers
{
    /// <summary>
    /// Response containing all users in our system.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// List of registered users.
        /// </summary>
        public IEnumerable<ReadUserModel> Users { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        private Response()
        {
            
        }

        /// <summary>
        /// Create new instance of <see cref="Response"/>
        /// </summary>
        /// <returns></returns>
        public static Response Create(IEnumerable<ReadUserModel> users)
        {
            return new Response { Users = users };
        }
    }
}