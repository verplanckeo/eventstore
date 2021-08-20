using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.Ticket;
using EventStore.Core.Domains.Ticket.Option;
using MediatR;

namespace EventStore.Application.Features.Ticket.Option
{
    public class GetTicketPrioritiesMediatorQueryHandler : IRequestHandler<GetTicketPrioritiesMediatorQuery, GetTicketPrioritiesMediatorQueryResult>
    {
        public GetTicketPrioritiesMediatorQueryHandler()
        {
            
        }

        public Task<GetTicketPrioritiesMediatorQueryResult> Handle(GetTicketPrioritiesMediatorQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(GetTicketPrioritiesMediatorQueryResult.CreateResult(GetAll()));
        }

        private IEnumerable<string> GetAll()
        {
            yield return TicketPriority.Low.ToString();
            yield return TicketPriority.Medium.ToString();
            yield return TicketPriority.High.ToString();
            yield return TicketPriority.Critical.ToString();
        }
    }
}