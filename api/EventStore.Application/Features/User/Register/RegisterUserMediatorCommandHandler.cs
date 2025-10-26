using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using EventStore.Application.Features.User.UpdateReadUser;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.User;
using MediatR;

namespace EventStore.Application.Features.User.Register
{
    public class RegisterUserMediatorCommandHandler : IRequestHandler<RegisterUserMediatorCommand, RegisterUserMediatorCommandResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IMediatorFactory _mediatorFactory;

        public RegisterUserMediatorCommandHandler(IUserRepository repository, IMediatorFactory mediatorFactory)
        {
            _repository = repository;
            _mediatorFactory = mediatorFactory;
        }

        public async Task<RegisterUserMediatorCommandResponse> Handle(RegisterUserMediatorCommand request, CancellationToken cancellationToken)
        {
            var scope = _mediatorFactory.CreateScope();

            var domainUser = Core.Domains.User.User.CreateNewUser(request.UserName, request.FirstName, request.LastName);
            var hashedPasswordResult = await scope.SendAsync(GetHashedPasswordMediatorQuery.CreateQuery(request.Password), cancellationToken);
            domainUser.ChangePassword(hashedPasswordResult.HashedPassword, hashedPasswordResult.Salt);

            var id = await _repository.SaveUserAsync(domainUser, cancellationToken);

            await scope.SendAsync(UpdateReadUserCommand.CreateCommand(id.ToString(), request.FirstName, request.LastName, request.UserName, domainUser.Version), cancellationToken);

            return RegisterUserMediatorCommandResponse.CreateResponse(id.ToString());
        }
    }
}