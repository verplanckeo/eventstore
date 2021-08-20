using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.User.Authenticate
{
    /// <summary>
    /// Example of authenticating a user.
    /// </summary>
    public class RequestExample : IExamplesProvider<Request>
    {
        /// <summary>
        /// Return an example request for authenticating a user.
        /// </summary>
        /// <returns></returns>
        public Request GetExamples()
        {
            return Request.CreateRequest("overplan", "supersecurepassword");
        }
    }
}