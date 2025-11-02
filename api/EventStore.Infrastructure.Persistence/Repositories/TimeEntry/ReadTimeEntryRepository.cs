using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.TimeEntry;
using EventStore.Application.Repositories.TimeEntry;
using EventStore.Core.Domains.TimeEntry;
using EventStore.Core.Domains.User;
using EventStore.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Repositories.TimeEntry;

public class ReadTimeEntryRepository : IReadTimeEntryRepository
    {
        private readonly IDatabaseContext _context;

        public ReadTimeEntryRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> SaveOrUpdateTimeEntryAsync(ReadTimeEntryModel timeEntry, CancellationToken cancellationToken)
        {
            var existingTimeEntry = await _context.ReadTimeEntries
                .FirstOrDefaultAsync(te => te.AggregateRootId == timeEntry.AggregateRootId, cancellationToken);

            if (existingTimeEntry == null)
            {
                // Create new
                var newTimeEntry = new Entities.TimeEntry.ReadTimeEntry
                {
                    AggregateRootId = timeEntry.AggregateRootId,
                    From = timeEntry.From,
                    Until = timeEntry.Until,
                    UserId = timeEntry.User.Id,
                    UserName = timeEntry.User.UserName,
                    ProjectId = timeEntry.Project.Id,
                    ProjectCode = timeEntry.Project.Code,
                    ActivityType = timeEntry.ActivityType,
                    Comment = timeEntry.Comment,
                    IsRemoved = timeEntry.IsRemoved,
                    Version = timeEntry.Version
                };

                await _context.ReadTimeEntries.AddAsync(newTimeEntry, cancellationToken);
            }
            else
            {
                // Update existing
                if (existingTimeEntry.Version < timeEntry.Version)
                {
                    existingTimeEntry.From = timeEntry.From;
                    existingTimeEntry.Until = timeEntry.Until;
                    existingTimeEntry.UserId = timeEntry.User.Id;
                    existingTimeEntry.UserName = timeEntry.User.UserName;
                    existingTimeEntry.ProjectId = timeEntry.Project.Id;
                    existingTimeEntry.ProjectCode = timeEntry.Project.Code;
                    existingTimeEntry.ActivityType = timeEntry.ActivityType;
                    existingTimeEntry.Comment = timeEntry.Comment;
                    existingTimeEntry.IsRemoved = timeEntry.IsRemoved;
                    existingTimeEntry.Version = timeEntry.Version;

                    _context.ReadTimeEntries.Update(existingTimeEntry);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return timeEntry.AggregateRootId;
        }
        
        public async Task<string> MarkTimeEntryAsRemovedAsync(string aggregateRootId, int version, CancellationToken cancellationToken)
        {
            var timeEntry = await _context.ReadTimeEntries
                .FirstOrDefaultAsync(te => te.AggregateRootId == aggregateRootId, cancellationToken);

            if (timeEntry != null && timeEntry.Version < version)
            {
                timeEntry.IsRemoved = true;
                timeEntry.Version = version;

                _context.ReadTimeEntries.Update(timeEntry);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return aggregateRootId;
        }

        public async Task<List<ReadTimeEntryModel>> LoadAllActiveTimeEntriesAsync(CancellationToken cancellationToken)
        {
            var timeEntries = await _context.ReadTimeEntries
                .Where(te => !te.IsRemoved)
                .OrderByDescending(te => te.From)
                .ToListAsync(cancellationToken);

            return timeEntries.Select(te => ReadTimeEntryModel.CreateNewReadTimeEntry(
                te.AggregateRootId,
                te.From,
                te.Until,
                te.UserId,
                te.UserName,
                te.ProjectId,
                te.ProjectCode,
                te.ActivityType,
                te.Comment,
                te.IsRemoved,
                te.Version)).ToList();
        }

        public async Task<List<ReadTimeEntryModel>> LoadAllActiveTimeEntriesForUserAsync(UserId userId, CancellationToken cancellationToken)
        {
            var timeEntries = await _context.ReadTimeEntries
                .Where(te => !te.IsRemoved && te.UserId == userId.ToString())
                .OrderByDescending(te => te.From)
                .ToListAsync(cancellationToken);

            return timeEntries.Select(te => ReadTimeEntryModel.CreateNewReadTimeEntry(
                te.AggregateRootId,
                te.From,
                te.Until,
                te.UserId,
                te.UserName,
                te.ProjectId,
                te.ProjectCode,
                te.ActivityType,
                te.Comment,
                te.IsRemoved,
                te.Version)).ToList();
        }

        public async Task<ReadTimeEntryModel> LoadTimeEntryByIdAsync(string aggregateRootId, CancellationToken cancellationToken)
        {
            var timeEntry = await _context.ReadTimeEntries
                .FirstOrDefaultAsync(te => te.AggregateRootId == aggregateRootId, cancellationToken);

            if (timeEntry == null)
                return null;

            return ReadTimeEntryModel.CreateNewReadTimeEntry(
                timeEntry.AggregateRootId,
                timeEntry.From,
                timeEntry.Until,
                timeEntry.UserId,
                timeEntry.UserName,
                timeEntry.ProjectId,
                timeEntry.ProjectCode,
                timeEntry.ActivityType,
                timeEntry.Comment,
                timeEntry.IsRemoved,
                timeEntry.Version);
        }

        public async Task<ReadTimeEntryModel> LoadTimeEntryByIdAsync(TimeEntryId aggregateRootId, CancellationToken cancellationToken)
        {
            return await LoadTimeEntryByIdAsync(aggregateRootId.ToString(), cancellationToken);
        }
    }