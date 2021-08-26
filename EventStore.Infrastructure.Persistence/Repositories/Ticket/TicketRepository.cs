using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories;
using EventStore.Application.Repositories.Ticket;
using EventStore.Core.Domains.Ticket;

namespace EventStore.Infrastructure.Persistence.Repositories.Ticket
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IEventStoreRepository _eventStore;

        public TicketRepository(IEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<TicketId> SaveTicketAsync(Core.Domains.Ticket.Ticket ticket, CancellationToken cancellationToken)
        {
            await _eventStore.SaveAsync(ticket.Id, ticket.Version, ticket.DomainEvents, "TicketRoot", cancellationToken);
            return ticket.Id;
        }

        public async Task<Core.Domains.Ticket.Ticket> LoadTicketAsync(string id, CancellationToken cancellationToken)
        {
            var ticketId = new TicketId(id);
            var events = await _eventStore.LoadAsync(ticketId, cancellationToken);
            return new Core.Domains.Ticket.Ticket(events);
        }
    }
}