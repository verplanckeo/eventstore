namespace EventStore.Api.Features.Ticket.Register
{
    /// <summary>
    /// Response returned after registering a new ticket.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Id of the newly registered ticket.
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="ticketId"><see cref="TicketId"/></param>
        private Response(string ticketId)
        {
            TicketId = ticketId;
        }

        /// <summary>
        /// Create new instance of Response.
        /// </summary>
        /// <param name="ticketId"><see cref="TicketId"/></param>
        /// <returns></returns>
        public static Response Create(string ticketId) => new Response(ticketId);
    }
}