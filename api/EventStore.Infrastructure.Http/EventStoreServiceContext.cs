using System.Security.Claims;
using EventStore.Application.Services;
using EventStore.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EventStore.Infrastructure.Http
{
    public class EventStoreServiceContext : IServiceContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventStoreServiceContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal Principal => _httpContextAccessor?.HttpContext?.User;

        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

        public string UserId
        {
            get
            {
                if (!IsAuthenticated) return null;

                // Try multiple known claim types in order of preference
                // 1) JWT registered "sub"
                var sub = Principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (!string.IsNullOrEmpty(sub)) return sub;

                // 2) Standard name identifier (http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier)
                var nameId = Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(nameId)) return nameId;

                // 3) Project's configured identifier constant (if used)
                var customId = Principal.FindFirst(Security.Claims.Identifier)?.Value;
                if (!string.IsNullOrEmpty(customId)) return customId;

                // No id claim found
                return null;
            }
        }

        public string UserName
        {
            get
            {
                if (!IsAuthenticated) return null;

                // Prefer the project's configured name claim, then standard identity name
                var customName = Principal.FindFirst(Security.Claims.Name)?.Value;
                if (!string.IsNullOrEmpty(customName)) return customName;

                var identityName = Principal.Identity?.Name;
                if (!string.IsNullOrEmpty(identityName)) return identityName;

                // fallback to any 'name' claim
                var nameClaim = Principal.FindFirst("name")?.Value;
                if (!string.IsNullOrEmpty(nameClaim)) return nameClaim;

                return null;
            }
        }
    }
}