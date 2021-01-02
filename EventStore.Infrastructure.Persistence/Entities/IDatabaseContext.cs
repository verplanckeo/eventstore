using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Entities
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Commit changes to database.
        /// </summary>
        /// <param name="token">Cancellation token in case process is interrupted.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken token = default);

        /// <summary>
        /// Get overview of users based on it's read model
        /// </summary>
        DbSet<ReadUser> ReadUsers { get; set; }


    }
}