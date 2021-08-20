using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.Ticket;
using EventStore.Core.Domains.Ticket.Option;
using MediatR;

namespace EventStore.Application.Features.Ticket.Option
{
    public class GetTicketTypesMediatorQueryHandler : IRequestHandler<GetTicketTypesMediatorQuery, GetTicketTypesMediatorQueryResult>
    {
        public GetTicketTypesMediatorQueryHandler()
        {
            
        }

        public Task<GetTicketTypesMediatorQueryResult> Handle(GetTicketTypesMediatorQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetTicketTypesMediatorQueryResult(GetAll()));
        }

        public IEnumerable<string> GetAll()
        {
            yield return TicketType.Bug.ToString();
            yield return TicketType.Defect.ToString();
            yield return TicketType.ProductBacklogItem.ToString();
        }
    }
}