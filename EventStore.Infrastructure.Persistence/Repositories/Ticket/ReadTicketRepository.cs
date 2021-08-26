using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Ticket;
using EventStore.Application.Repositories.Ticket;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Entities.Ticket;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Repositories.Ticket
{
    public class ReadTicketRepository : IReadTicketRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ReadTicketRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<string> SaveOrUpdateTicketAsync(ReadTicketModel readTicket, CancellationToken cancellationToken)
        {
            var existingRecord = await LoadTicketByAggregateRootIdAsync(readTicket.AggregateRootId, cancellationToken);
            if (existingRecord != null)
            {
                existingRecord.ChangeTicketModel(readTicket);
                await _databaseContext.SaveChangesAsync(cancellationToken);
                return existingRecord.AggregateRootId;
            }

            try
            {
                var record = new ReadTicket
                {
                    Title = readTicket.Title,
                    TicketPriority = readTicket.TicketPriority,
                    TicketState = readTicket.TicketState,
                    TicketType = readTicket.TicketType,
                    UserName = readTicket.UserName,
                    UserId = readTicket.UserId.Id.ToString(),
                    AggregateRootId = readTicket.AggregateRootId,
                    Version = readTicket.Version + 1
                };

                await _databaseContext.ReadTickets.AddAsync(record, cancellationToken);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return record.AggregateRootId;
            }
            catch
            {
                //TODO - Add errorhandling
                return null;
            }
        }

        public async Task<IEnumerable<ReadTicketModel>> LoadAllTicketsAsync(CancellationToken cancellationToken)
        {
            return (await _databaseContext.ReadTickets.ToListAsync(cancellationToken))
                .Select(t => ReadTicketModel.CreateModel(t.AggregateRootId, t.UserId, t.UserName, t.Title, t.TicketState, t.TicketPriority, t.TicketType, t.Version));
        }

        public async Task<IEnumerable<ReadTicketModel>> LoadAllTicketsForUser(string userName, CancellationToken cancellationToken)
        {
            return (await _databaseContext.ReadTickets.Where(t => t.UserName == userName).ToListAsync(cancellationToken))
                .Select(t => ReadTicketModel.CreateModel(t.AggregateRootId, t.UserId, t.UserName, t.Title, t.TicketState, t.TicketPriority, t.TicketType, t.Version));
        }

        private async Task<ReadTicketModel> LoadTicketByAggregateRootIdAsync(string aggregateRootId, CancellationToken cancellationToken)
        {
            var entity = await _databaseContext.ReadTickets.FindAsync(new[] { aggregateRootId }, cancellationToken);

            if (entity == null) return null;

            return ReadTicketModel.CreateModel(entity.AggregateRootId, entity.UserId, entity.UserName, entity.Title, entity.TicketState, entity.TicketPriority, entity.TicketType, entity.Version);
        }
    }
}