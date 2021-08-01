using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EventStore.Application.Features.User.Password
{
    public class ValidateHashedPasswordMediatorQueryHandler : IRequestHandler<ValidateHashedPasswordMediatorQuery, ValidateHashedPasswordMediatorQueryResult>
    {
        public Task<ValidateHashedPasswordMediatorQueryResult> Handle(ValidateHashedPasswordMediatorQuery request, CancellationToken cancellationToken)
        {
            //TODO: This is copy paste from the GetHashed handler so we can move this into a service
            var salt = Convert.FromBase64String(request.Salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: request.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var isValid = hashed == request.HashedPassword;
            return Task.FromResult(ValidateHashedPasswordMediatorQueryResult.CreateResult(isValid));
        }
    }
}