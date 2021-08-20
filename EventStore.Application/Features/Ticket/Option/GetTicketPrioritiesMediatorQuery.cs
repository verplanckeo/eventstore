using MediatR;

namespace EventStore.Application.Features.Ticket.Option
{
    public class GetTicketPrioritiesMediatorQuery : IRequest<GetTicketPrioritiesMediatorQueryResult>
    {
        public static GetTicketPrioritiesMediatorQuery CreateQuery() => new GetTicketPrioritiesMediatorQuery();
    }
}