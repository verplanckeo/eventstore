using EventStore.Api.Features.Models;

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
    }
}