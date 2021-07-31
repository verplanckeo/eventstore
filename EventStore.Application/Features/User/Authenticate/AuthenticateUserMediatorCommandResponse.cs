namespace EventStore.Application.Features.User.Authenticate
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticateUserMediatorCommandResponse
    {
        /// <summary>
        /// Identifier of the user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Access Token
        /// </summary>
        public string Token { get; set; } //todo - make all properties get only, for now they are set as well for unit tests.

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        public AuthenticateUserMediatorCommandResponse(string id, string token)
        {
            Id = id;
            Token = token;
        }

        /// <summary>
        /// Create new instance of <see cref="AuthenticateUserMediatorCommandResponse"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static AuthenticateUserMediatorCommandResponse CreateResponse(string id, string token)
        {
            return new AuthenticateUserMediatorCommandResponse(id, token);
        }
    }
}