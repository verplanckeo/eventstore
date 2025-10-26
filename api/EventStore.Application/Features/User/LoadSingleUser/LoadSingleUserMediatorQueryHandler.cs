using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.User;
using EventStore.Application.Services;
using MediatR;

namespace EventStore.Application.Features.User.LoadSingleUser;

public class LoadSingleUserMediatorQueryHandler(IReadUserRepository repository, IServiceContext context)
    : IRequestHandler<LoadSingleUserMediatorQuery, LoadSingleUserMediatorQueryResult>
{
    public async Task<LoadSingleUserMediatorQueryResult> Handle(LoadSingleUserMediatorQuery request, CancellationToken cancellationToken)
    {
        var userId = context.UserId;
        if (!string.IsNullOrEmpty((request.AggregateRootId))) userId = request.AggregateRootId;
        
        var user = await repository.LoadUserByAggregateRootIdAsync(userId, cancellationToken);

        return LoadSingleUserMediatorQueryResult.CreateResult(user);
    }
}