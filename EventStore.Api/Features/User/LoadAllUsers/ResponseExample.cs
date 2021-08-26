using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.User.LoadAllUsers
{
    /// <summary>
    /// Example of returned data when loading all users.
    /// </summary>
    public class ResponseExample : IExamplesProvider<Response>
    {
        /// <summary>
        /// Return an example after authentication request.
        /// </summary>
        /// <returns></returns>
        public Response GetExamples()
        {
            return Response.Create();
        }
    }
}