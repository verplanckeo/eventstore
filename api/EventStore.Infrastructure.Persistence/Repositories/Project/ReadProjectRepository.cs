using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Project;
using EventStore.Application.Repositories.Project;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Entities.Project;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Repositories.Project;

public class ReadProjectRepository : IReadProjectRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public ReadProjectRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public async Task<string> SaveOrUpdateProjectAsync(ReadProjectModel readProject, CancellationToken cancellationToken)
        {
            var existingRecord = await LoadProjectByAggregateRootIdAsync(readProject.AggregateRootId, cancellationToken);
            if (existingRecord != null)
            {
                existingRecord.ChangeProjectModel(readProject);
                await _databaseContext.SaveChangesAsync(cancellationToken);
                return readProject.AggregateRootId;
            }

            try
            {
                var record = new ReadProject
                {
                    AggregateRootId = readProject.AggregateRootId,
                    Name = readProject.Name,
                    Code = readProject.Code,
                    Billable = readProject.Billable,
                    IsRemoved = readProject.IsRemoved,
                    Version = readProject.Version + 1
                };

                await _databaseContext.ReadProjects.AddAsync(record, cancellationToken);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return readProject.AggregateRootId;
            }
            catch
            {
                //TODO: add error handling
                return null;
            }
        }

        public async Task<IEnumerable<ReadProjectModel>> LoadProjectsAsync(CancellationToken cancellationToken)
        {
            return (await _databaseContext.ReadProjects
                .Where(p => !p.IsRemoved)
                .ToListAsync(cancellationToken))
                .Select(r => ReadProjectModel.CreateNewReadProject(
                    r.AggregateRootId, 
                    r.Name, 
                    r.Code, 
                    r.Billable, 
                    r.IsRemoved, 
                    r.Version));
        }

        public async Task<ReadProjectModel> LoadProjectByAggregateRootIdAsync(string aggregateRootId, CancellationToken cancellationToken)
        {
            var entity = await _databaseContext.ReadProjects.FindAsync(new[]{aggregateRootId}, cancellationToken);
            
            if (entity == null) return null;

            return ReadProjectModel.CreateNewReadProject(
                entity.AggregateRootId, 
                entity.Name, 
                entity.Code, 
                entity.Billable, 
                entity.IsRemoved, 
                entity.Version);
        }

        public async Task<ReadProjectModel> LoadProjectByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var entity = await _databaseContext.ReadProjects
                .SingleOrDefaultAsync(p => p.Code == code, cancellationToken);

            if (entity == null) return null;

            return ReadProjectModel.CreateNewReadProject(
                entity.AggregateRootId, 
                entity.Name, 
                entity.Code, 
                entity.Billable, 
                entity.IsRemoved, 
                entity.Version);
        }
    }