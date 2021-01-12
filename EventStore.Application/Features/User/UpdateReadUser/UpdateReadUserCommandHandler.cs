using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.User;
using MediatR;

namespace EventStore.Application.Features.User.UpdateReadUser
{
    public class UpdateReadUserCommandHandler : IRequestHandler<UpdateReadUserCommand>
    {
        private readonly IReadUserRepository _repository;

        public UpdateReadUserCommandHandler(IReadUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateReadUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.SaveOrUpdateUserAsync(ReadUserModel.CreateNewReadUser(request.AggregateRootId, request.FirstName, request.LastName, request.Version), cancellationToken);

            if (string.IsNullOrEmpty(result))
            {
                //could not save / update read model
            }
            return Unit.Value;
        }
    }
}