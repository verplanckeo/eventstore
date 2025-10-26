using EventStore.Core.Domains.Ticket.Option;
using MediatR;

namespace EventStore.Application.Features.Ticket.UpdateReadTicket
{
    public class UpdateReadTicketCommand : IRequest
    {
        public string AggregateRootId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public TicketState TicketState { get; set; }

        public TicketType TicketType { get; set; }

        public TicketPriority TicketPriority { get; set; }

        public int Version { get; }

        public UpdateReadTicketCommand(string aggregateRootId, string userId, string userName, string title, TicketState state, TicketPriority priority, TicketType type, int version)
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

        public static UpdateReadTicketCommand CreateCommand(string aggregateRootId, string userId, string userName, string title, TicketState state, TicketPriority priority, TicketType type, int version)
        {
            return new UpdateReadTicketCommand(aggregateRootId, userId, userName, title, state, priority, type, version);
        }
    }
}