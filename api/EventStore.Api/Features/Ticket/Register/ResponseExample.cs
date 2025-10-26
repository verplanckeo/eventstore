using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.Ticket.Register
{
    /// <summary>
    /// Example of register new ticket response.
    /// </summary>
    public class ResponseExample : IExamplesProvider<Response>
    {
        /// <summary>
        /// Generate an example of the response after registering a ticket.
        /// </summary>
        /// <returns></returns>
        public Response GetExamples()
        {
            return Response.Create("3FCD5BCB-B984-4B1E-84C4-1913C97A75BD");
        }
    }
}