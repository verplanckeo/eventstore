namespace EventStore.Application.Services
{
    public interface IServiceContext
    {
        /// <summary>
        /// True when the current request has an authenticated principal.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Identifier of the authenticated user (as provided in JWT claims).
        /// Returns null when not authenticated or the claim is not present.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Username / display name of the authenticated user (as provided in JWT claims).
        /// Returns null when not authenticated or the claim is not present.
        /// </summary>
        string UserName { get; }
    }
}