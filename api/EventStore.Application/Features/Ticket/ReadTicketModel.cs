using EventStore.Core.Domains.Ticket;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Application.Features.Ticket
{
    public class ReadTicketModel
    {
        public string AggregateRootId { get; set; }

        public UserId UserId { get; private set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public TicketState TicketState { get; set; }

        public TicketType TicketType { get; set; }

        public TicketPriority TicketPriority { get; set; }

        public int Version { get; private set; }

        //TODO: Rework default constructor - for now it's only used for unit tests
        public ReadTicketModel()
        { }

        public ReadTicketModel(string aggregateRootId, UserId userId, string userName, string title, TicketState state, TicketPriority priority, TicketType type, int version)
        {
            AggregateRootId = aggregateRootId;
            UserId = userId;
            UserName = userName;
            Title = title;
            TicketState = state;
            TicketPriority = priority;
            TicketType = type;
            Version = version;
        }

        public void ChangeTicketModel(ReadTicketModel updated)
        {
            Title = updated.Title;
            TicketState = updated.TicketState;
            TicketPriority = updated.TicketPriority;
            TicketType = updated.TicketType;
            Version++;
        }

        public static ReadTicketModel CreateModel(string aggregateRootId, string userId, string userName, string title,
            TicketState state, TicketPriority priority, TicketType type, int version)
        {
            return new ReadTicketModel(aggregateRootId, new UserId(userId), userName, title, state, priority, type, version);
        }
    }
}