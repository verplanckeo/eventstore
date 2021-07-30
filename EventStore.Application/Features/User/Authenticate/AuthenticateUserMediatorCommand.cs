using MediatR;

namespace EventStore.Application.Features.User.Authenticate
{
    /// <summary>
    /// Command used to authenticate user
    /// </summary>
    public class AuthenticateUserMediatorCommand : IRequest<AuthenticateUserMediatorCommandResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        //TODO: Rework default constructor - for now it's use only used for unit tests
        public AuthenticateUserMediatorCommand() { }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        private AuthenticateUserMediatorCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        
        /// <summary>
        /// Create new instance of <see cref="AuthenticateUserMediatorCommand"/>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static AuthenticateUserMediatorCommand CreateCommand(string userName, string password) => new AuthenticateUserMediatorCommand(userName, password);
    }
}