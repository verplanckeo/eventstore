namespace EventStore.Api.Features.User.Register
{
    /// <summary>
    /// Request model to register a new user.
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
        /// Person's first name (i.e.: Olivier)
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Person's last name (i.e.: Verplancke)
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="userName"><see cref="UserName"/></param>
        /// <param name="password"><see cref="Password"/></param>
        /// <param name="firstName"><see cref="FirstName"/></param>
        /// <param name="lastName"><see cref="LastName"/></param>
        public Request(string userName, string password, string firstName, string lastName)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Create a new instance of <see cref="Request"/>
        /// </summary>
        /// <param name="userName"><see cref="UserName"/></param>
        /// <param name="password"><see cref="Password"/></param>
        /// <param name="firstName"><see cref="FirstName"/></param>
        /// <param name="lastName"><see cref="LastName"/></param>
        /// <returns></returns>
        public static Request Create(string userName, string password, string firstName, string lastName) => new Request(userName, password, firstName, lastName);
    }
}
