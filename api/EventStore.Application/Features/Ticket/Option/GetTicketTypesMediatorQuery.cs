using MediatR;

namespace EventStore.Application.Features.Ticket.Option
{
    public class GetTicketTypesMediatorQuery : IRequest<GetTicketTypesMediatorQueryResult>
    {
        public static GetTicketTypesMediatorQuery CreateQuery() => new GetTicketTypesMediatorQuery();
    }
}