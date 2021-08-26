using EventStore.Core.Domains.Ticket.Option;
using MediatR;

namespace EventStore.Application.Features.Ticket.Register
{
    public class RegisterTicketMediatorCommand : IRequest<RegisterTicketMediatorCommandResponse>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TicketType TicketType { get; set; }

        public TicketPriority TicketPriority { get; set; }

        //TODO: Rework default constructor - for now it's use only used for unit tests
        public RegisterTicketMediatorCommand()
        { }

        private RegisterTicketMediatorCommand(string userId, string userName, string title, string description, TicketType ticketType, TicketPriority ticketPriority)
        {
            UserId = userId;
            UserName = userName;
            Title = title;
            Description = description;
            TicketType = ticketType;
            TicketPriority = ticketPriority;
        }

        public static RegisterTicketMediatorCommand CreateCommand(string userId, string userName, string title, string description, TicketType ticketType, TicketPriority ticketPriority)
        {
            return new RegisterTicketMediatorCommand(userId, userName, title, description, ticketType, ticketPriority);
        }
    }
}