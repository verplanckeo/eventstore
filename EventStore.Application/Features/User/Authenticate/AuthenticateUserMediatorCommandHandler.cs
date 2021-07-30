using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.User;
using MediatR;

namespace EventStore.Application.Features.User.Authenticate
{
    public class AuthenticateUserMediatorCommandHandler : IRequestHandler<AuthenticateUserMediatorCommand, AuthenticateUserMediatorCommandResponse>
    {
        private readonly IReadUserRepository _readUserRepository;

        public AuthenticateUserMediatorCommandHandler(IReadUserRepository readUserRepository)
        {
            _readUserRepository = readUserRepository;
        }

        public Task<AuthenticateUserMediatorCommandResponse> Handle(AuthenticateUserMediatorCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}