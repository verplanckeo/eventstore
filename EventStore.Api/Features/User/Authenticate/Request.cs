namespace EventStore.Api.Features.User.Authenticate
{
    /// <summary>
    /// Request model to authenticate a user.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// User name to login (i.e.: overplan)
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// User's password (i.e.: SuperSecureLongPassword)
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="userName"><see cref="UserName"/></param>
        /// <param name="password"><see cref="Password"/></param>
        public Request(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Create new instance of <see cref="Request"/>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Request CreateRequest(string userName, string password) => new(userName, password);
    }
}