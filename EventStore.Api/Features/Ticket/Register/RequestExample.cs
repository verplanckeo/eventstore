using EventStore.Api.Features.Ticket.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.Ticket.Register
{
    /// <summary>
    /// Request example for registering a new ticket.
    /// </summary>
    public class RequestExample : IExamplesProvider<Request>
    {
        /// <summary>
        /// Generate an example of a request to register a new ticket.
        /// </summary>
        /// <returns></returns>
        public Request GetExamples()
        {
            return Request.CreateRequest("Example title", "Example description", TicketType.Bug, TicketPriority.Critical);
        }
    }
}