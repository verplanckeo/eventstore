using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Services;
using MediatR;

namespace EventStore.Application.Features.User.Password
{
    public class GetHashedPasswordMediatorQueryHandler : IRequestHandler<GetHashedPasswordMediatorQuery, GetHashedPasswordMediatorQueryResult>
    {
        private readonly ISecurityService _securityService;

        public GetHashedPasswordMediatorQueryHandler(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<GetHashedPasswordMediatorQueryResult> Handle(GetHashedPasswordMediatorQuery request, CancellationToken cancellationToken)
        {
            var (hashedPassword, salt) = await _securityService.GenerateHashedPassword(request.Password);

            return GetHashedPasswordMediatorQueryResult.CreateResult(hashedPassword, salt);
        }
    }
}