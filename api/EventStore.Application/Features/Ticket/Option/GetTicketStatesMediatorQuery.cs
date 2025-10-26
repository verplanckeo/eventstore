using MediatR;

namespace EventStore.Application.Features.Ticket.Option
{
    public class GetTicketStatesMediatorQuery : IRequest<GetTicketStatesMediatorQueryResult>
    {
        public static GetTicketStatesMediatorQuery CreateQuery() => new GetTicketStatesMediatorQuery();
    }
}