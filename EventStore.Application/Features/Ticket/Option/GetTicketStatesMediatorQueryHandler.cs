using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.Ticket;
using EventStore.Core.Domains.Ticket.Option;
using MediatR;

namespace EventStore.Application.Features.Ticket.Option
{
    public class GetTicketStatesMediatorQueryHandler : IRequestHandler<GetTicketStatesMediatorQuery, GetTicketStatesMediatorQueryResult>
    {
        public GetTicketStatesMediatorQueryHandler()
        {
            
        }

        public Task<GetTicketStatesMediatorQueryResult> Handle(GetTicketStatesMediatorQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(GetTicketStatesMediatorQueryResult.CreateResult(GetAll()));
        }

        private IEnumerable<string> GetAll()
        {
            yield return TicketState.New.ToString();
            yield return TicketState.InProgress.ToString();
            yield return TicketState.Resolved.ToString();
            yield return TicketState.Done.ToString();
            yield return TicketState.Closed.ToString();
        }
    }
}