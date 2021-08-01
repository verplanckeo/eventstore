using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EventStore.Application.Features.User.Password
{
    public class GetHashedPasswordMediatorQueryHandler : IRequestHandler<GetHashedPasswordMediatorQuery, GetHashedPasswordMediatorQueryResult>
    {
        public GetHashedPasswordMediatorQueryHandler()
        { }

        public Task<GetHashedPasswordMediatorQueryResult> Handle(GetHashedPasswordMediatorQuery request, CancellationToken cancellationToken)
        {
            //TODO: This has been copy pasted in the AuthenticatePassword handler so we can move this into a service to share the logic
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: request.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return Task.FromResult(GetHashedPasswordMediatorQueryResult.CreateResult(hashed, Convert.ToBase64String(salt)));
        }
    }
}