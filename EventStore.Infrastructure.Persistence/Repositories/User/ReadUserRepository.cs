﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User;
using EventStore.Application.Repositories.User;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Entities.User;
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
        
        public async Task<string> SaveOrUpdateUserAsync(ReadUserModel readUser, CancellationToken cancellationToken)
        {
            var existingRecord = await LoadUserByAggregateRootIdAsync(readUser.AggregateRootId, cancellationToken);
            if (existingRecord != null)
            {
                existingRecord.ChangeUserModel(readUser);
                await _databaseContext.SaveChangesAsync(cancellationToken);
                return readUser.AggregateRootId;
            }

            try
            {
                var record = new ReadUser
                {
                    AggregateRootId = readUser.AggregateRootId,
                    FirstName = readUser.FirstName,
                    LastName = readUser.LastName,
                    UserName = readUser.UserName,
                    Version = readUser.Version + 1
                };

                await _databaseContext.ReadUsers.AddAsync(record, cancellationToken);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return readUser.AggregateRootId;
            }
            catch
            {
                //TODO: add error handling
                return null;
            }
        }

        public async Task<IEnumerable<ReadUserModel>> LoadUsersAsync(CancellationToken cancellationToken)
        {
            return (await _databaseContext.ReadUsers.ToListAsync(cancellationToken)).Select(r => ReadUserModel.CreateNewReadUser(r.AggregateRootId, r.FirstName, r.LastName, r.UserName, r.Version));
        }

        private async Task<ReadUserModel> LoadUserByAggregateRootIdAsync(string aggregateRootId, CancellationToken cancellationToken)
        {
            var entity = await _databaseContext.ReadUsers.FindAsync(new[]{aggregateRootId}, cancellationToken);
            
            if (entity == null) return null;

            return ReadUserModel.CreateNewReadUser(entity.AggregateRootId, entity.FirstName, entity.LastName, entity.UserName, entity.Version);
        }

        public async Task<ReadUserModel> LoadUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            var entity = await _databaseContext.ReadUsers.SingleAsync(u => u.UserName == userName, cancellationToken);

            if (entity == null) return null;

            return ReadUserModel.CreateNewReadUser(entity.AggregateRootId, entity.FirstName, entity.LastName, entity.UserName, entity.Version);
        }
    }
}