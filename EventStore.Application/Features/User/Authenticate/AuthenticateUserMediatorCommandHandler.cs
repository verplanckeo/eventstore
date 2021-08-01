using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.User;
using EventStore.Application.Services;
using MediatR;

namespace EventStore.Application.Features.User.Authenticate
{
    public class AuthenticateUserMediatorCommandHandler : IRequestHandler<AuthenticateUserMediatorCommand, AuthenticateUserMediatorCommandResponse>
    {
        private readonly IReadUserRepository _readUserRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMediatorFactory _mediatorFactory;

        private readonly ISecurityService _securityService;

        public AuthenticateUserMediatorCommandHandler(IReadUserRepository readUserRepository, IUserRepository userRepository, IMediatorFactory mediatorFactory, ISecurityService securityService)
        {
            _readUserRepository = readUserRepository;
            _userRepository = userRepository;
            _mediatorFactory = mediatorFactory;
            _securityService = securityService;
        }

        public async Task<AuthenticateUserMediatorCommandResponse> Handle(AuthenticateUserMediatorCommand request, CancellationToken cancellationToken)
        {
            var readUser = await _readUserRepository.LoadUserByUserNameAsync(request.UserName, cancellationToken);
            if (readUser == null) throw new KeyNotFoundException($"User with username {request.UserName} was not found.");

            var user = await _userRepository.LoadUserAsync(readUser.AggregateRootId, cancellationToken);

            //Check if the password entered by the user is the same as what is stored in our database
            var scope = _mediatorFactory.CreateScope();
            var passwordResult = await scope.SendAsync(ValidateHashedPasswordMediatorQuery.CreateQuery(
                request.Password, 
                user.Password.HashedPassword, 
                user.Password.Salt), 
                cancellationToken);

            if (!passwordResult.IsValid) throw new InvalidCredentialException($"User with username {request.UserName} entered an invalid password");
            var jwtToken = await _securityService.GenerateJsonWebToken(user, cancellationToken);

            return AuthenticateUserMediatorCommandResponse.CreateResponse(readUser.AggregateRootId, jwtToken);
        }
    }
}