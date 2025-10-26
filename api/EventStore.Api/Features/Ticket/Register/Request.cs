using EventStore.Api.Features.Ticket.Models;

namespace EventStore.Api.Features.Ticket.Register
{
    /// <summary>
    /// Request 
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Type of ticket to be registered.
        /// </summary>
        public TicketType TicketType { get; set; }

        /// <summary>
        /// Priority of the ticket.
        /// </summary>
        public TicketPriority TicketPriority { get; set; }

        /// <summary>
        /// Title of the ticket.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the ticket.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="title"><see cref="Title"/></param>
        /// <param name="description"><see cref="Description"/></param>
        /// <param name="ticketType"><see cref="TicketType"/></param>
        /// <param name="ticketPriority"><see cref="TicketPriority"/></param>
        public Request(string title, string description, TicketType ticketType, TicketPriority ticketPriority)
        {
            Title = title;
            Description = description;
            TicketType = ticketType;
            TicketPriority = ticketPriority;
        }

        /// <summary>
        /// Create an instance of request to register a new ticket.
        /// </summary>
        /// <param name="title">Title of the ticket.</param>
        /// <param name="description">Description of the ticket.</param>
        /// <param name="ticketType">Type of ticket (i.e. <example>Defect</example>).</param>
        /// <param name="ticketPriority">Priority of ticket (i.e. <example>High</example>).</param>
        /// <returns></returns>
        public static Request CreateRequest(string title, string description, TicketType ticketType, TicketPriority ticketPriority)
        {
            return new Request(title, description, ticketType, ticketPriority);
        }
    }
}