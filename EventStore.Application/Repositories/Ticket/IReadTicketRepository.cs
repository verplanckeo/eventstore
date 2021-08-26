using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Ticket;

namespace EventStore.Application.Repositories.Ticket
{
    /// <summary>
    /// Repository to save or get the read representation of tickets.
    /// </summary>
    public interface IReadTicketRepository
    {
        /// <summary>
        /// Save or update ReadTicket model.
        /// </summary>
        /// <param name="readTicket"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> SaveOrUpdateTicketAsync(ReadTicketModel readTicket, CancellationToken cancellationToken);

        /// <summary>
        /// Load all tickets in our system.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ReadTicketModel>> LoadAllTicketsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Load all tickets in our system linked to the given user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ReadTicketModel>> LoadAllTicketsForUser(string userName, CancellationToken cancellationToken);
    }
}