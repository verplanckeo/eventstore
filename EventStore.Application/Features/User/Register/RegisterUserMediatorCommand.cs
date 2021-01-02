using MediatR;

namespace EventStore.Application.Features.User.Register
{
    public class RegisterUserMediatorCommand : IRequest<RegisterUserMediatorCommandResponse>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private RegisterUserMediatorCommand(string userName, string firstName, string lastName)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        public static RegisterUserMediatorCommand CreateCommand(string userName, string firstName, string lastName)
        {
            return new RegisterUserMediatorCommand(userName, firstName, lastName);
        }
    }
}