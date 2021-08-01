using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Services;
using EventStore.Core.Domains.User;
using EventStore.Infrastructure.Seedwork;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EventStore.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly Jwt _jwt;

        public SecurityService(IOptions<Jwt> jwt)
        {
            _jwt = jwt.Value;
        }

        public Task<string> GenerateJsonWebToken(User user, CancellationToken cancellationToken)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, $"{user.UserName}")
            };

            var token = new JwtSecurityToken(
                _jwt.Issuer, 
                _jwt.Issuer, 
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ValidFor), 
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
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