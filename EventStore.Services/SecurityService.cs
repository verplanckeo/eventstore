using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Password;
using EventStore.Application.Services;
using EventStore.Core.Domains.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EventStore.Services
{
    public class SecurityService : ISecurityService
    {
        public Task<string> GenerateJsonWebToken(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<(string hashedPassword, string salt)> GenerateHashedPassword(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var base64Salt = Convert.ToBase64String(salt);
            var hashedPassword = await GenerateHashedPassword(password, base64Salt);

            return (hashedPassword, base64Salt);
        }

        public Task<string> GenerateHashedPassword(string password, string salt)
        { 
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return Task.FromResult(hashed);
        }
    }
}