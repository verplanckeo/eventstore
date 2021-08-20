using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.DomainEvents
{
    public class TicketRegisteredDomainEvent : DomainEvent
    {
        public string Id { get; set; }
        public TicketType TicketType { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public TicketRegisteredDomainEvent(string id, string title, string description, TicketType ticketType, TicketPriority ticketPriority)
        {
            Id = id;
            Title = title;
            Description = description;
            TicketType = ticketType;
            TicketPriority = ticketPriority;
        }
    }
}