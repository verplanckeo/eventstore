using System.Threading;
using System.Threading.Tasks;
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
            var domainUser = Core.Domains.User.User.CreateNewUser(request.UserName, request.FirstName, request.LastName);
            var id = await _repository.SaveUserAsync(domainUser, cancellationToken);
            
            var scope = _mediatorFactory.CreateScope();
            await scope.SendAsync(UpdateReadUserCommand.CreateCommand(id.ToString(), request.FirstName, request.LastName, domainUser.Version), cancellationToken);

            return RegisterUserMediatorCommandResponse.CreateResponse(id.ToString());
        }
    }
}