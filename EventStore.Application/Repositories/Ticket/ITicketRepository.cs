using System.Threading;
using System.Threading.Tasks;
using EventStore.Core.Domains.Ticket;

namespace EventStore.Application.Repositories.Ticket
{
    public interface ITicketRepository
    {
        Task<TicketId> SaveTicketAsync(Core.Domains.Ticket.Ticket ticket, CancellationToken cancellationToken);

        Task<Core.Domains.Ticket.Ticket> LoadTicketAsync(string id, CancellationToken cancellationToken);
    }
}