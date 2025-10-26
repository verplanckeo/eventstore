using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Services;
using MediatR;

namespace EventStore.Application.Features.User.Password
{
    public class ValidateHashedPasswordMediatorQueryHandler : IRequestHandler<ValidateHashedPasswordMediatorQuery, ValidateHashedPasswordMediatorQueryResult>
    {
        private readonly ISecurityService _securityService;

        public ValidateHashedPasswordMediatorQueryHandler(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<ValidateHashedPasswordMediatorQueryResult> Handle(ValidateHashedPasswordMediatorQuery request, CancellationToken cancellationToken)
        {
            var hashed = await _securityService.GenerateHashedPassword(request.Password, request.Salt);

            var isValid = hashed == request.HashedPassword;
            return ValidateHashedPasswordMediatorQueryResult.CreateResult(isValid);
        }
    }
}