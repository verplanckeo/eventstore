namespace EventStore.Api.Features.User.Register
{
    /// <summary>
    /// Response model when user successfully registered.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Identifier of the user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User name to login (i.e.: overplan)
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Access token to authorize the user to access resources of the EventStore Api
        /// </summary>
        public string Token { get; }

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
        /// <param name="id"><see cref="Id"/></param>
        /// <param name="userName"><see cref="UserName"/></param>
        /// <param name="token"><see cref="Token"/></param>
        /// <param name="firstName"><see cref="FirstName"/></param>
        /// <param name="lastName"><see cref="LastName"/></param>
        public Response(string id, string token, string userName, string firstName, string lastName)
        {
            Id = id;
            Token = token;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Create a new instance of <see cref="Response"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static Response Create(string id, string token, string userName, string firstName, string lastName) => new Response(id, token, userName, firstName, lastName);
    }
}