using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.User.Authenticate
{
    /// <summary>
    /// Example of authentication response
    /// </summary>
    public class ResponseExample : IExamplesProvider<Response>
    {
        /// <summary>
        /// Return an example after authentication request.
        /// </summary>
        /// <returns></returns>
        public Response GetExamples()
        {
            return Response.CreateResponse("id", "jwt-token");
        }
    }
}