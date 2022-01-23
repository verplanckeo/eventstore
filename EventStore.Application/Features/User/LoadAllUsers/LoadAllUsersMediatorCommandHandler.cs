using EventStore.Application.Repositories.User;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventStore.Application.Features.User.LoadAllUsers
{
    public class LoadAllUsersMediatorCommandHandler : IRequestHandler<LoadAllUsersMediatorCommand, LoadAllUsersMediatorCommandResponse>
    {
        private readonly IReadUserRepository _repository;

        public LoadAllUsersMediatorCommandHandler(IReadUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoadAllUsersMediatorCommandResponse> Handle(LoadAllUsersMediatorCommand request, CancellationToken cancellationToken)
        {
            var users = await _repository.LoadUsersAsync(cancellationToken);

            return LoadAllUsersMediatorCommandResponse.CreateResponse(users);
        }
    }
}
