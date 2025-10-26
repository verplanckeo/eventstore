using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.User.Register
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
            return Response.Create("27C7D9FF-AC4F-4C8F-AED4-EB188CED1C64", "overplan", "Olivier", "Verplancke");
        }
    }
}