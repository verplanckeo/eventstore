using MediatR;

namespace EventStore.Application.Features.User.Register
{
    public class RegisterUserMediatorCommand : IRequest<RegisterUserMediatorCommandResponse>
    {
        public string UserName { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Password { get; private set; }

        //TODO: Rework default constructor - for now it's use only used for unit tests
        public RegisterUserMediatorCommand()
        {
            
        }

        private RegisterUserMediatorCommand(string userName, string firstName, string lastName, string password)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public static RegisterUserMediatorCommand CreateCommand(string userName, string firstName, string lastName, string password)
        {
            return new RegisterUserMediatorCommand(userName, firstName, lastName, password);
        }
    }
}