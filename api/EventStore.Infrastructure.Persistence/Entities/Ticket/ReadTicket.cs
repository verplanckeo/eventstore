using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Infrastructure.Persistence.Entities.Ticket
{
    /// <summary>
    /// Basic representation 
    /// </summary>
    public class ReadTicket
    {
        public string AggregateRootId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public TicketState TicketState { get; set; }

        public TicketType TicketType { get; set; }

        public TicketPriority TicketPriority { get; set; }

        public int Version { get; set; }

    }
}