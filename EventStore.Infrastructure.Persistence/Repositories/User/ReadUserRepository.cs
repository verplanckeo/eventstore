using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Entities.User;
using EventStore.Application.Repositories.User;
using EventStore.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Repositories.User
{
    public class ReadUserRepository : IReadUserRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ReadUserRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public async Task<string> SaveUserAsync(ReadUser readUser, CancellationToken cancellationToken)
        {
            var existingRecord = await LoadUserAsync(readUser.AggregateRootId, cancellationToken);
            if (existingRecord != null)
            {
                existingRecord.FirstName = readUser.FirstName;
                existingRecord.LastName = readUser.LastName;
                await _databaseContext.SaveChangesAsync(cancellationToken);
                return readUser.AggregateRootId;
            }

            try
            {
                var record = new ReadUser
                {
                    AggregateRootId = readUser.AggregateRootId,
                    FirstName = readUser.FirstName,
                    LastName = readUser.LastName
                };
                await _databaseContext.ReadUsers.AddAsync(record, cancellationToken);
                return readUser.AggregateRootId;
            }
            catch
            {
                //TODO: add error handling
                return null;
            }
        }

        public async Task<IEnumerable<ReadUser>> LoadUsersAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.ReadUsers.ToListAsync(cancellationToken);
        }

        private async Task<ReadUser> LoadUserAsync(string aggregateRootId, CancellationToken cancellationToken)
        {
            return await _databaseContext.ReadUsers.FindAsync(aggregateRootId);
        }
    }
}