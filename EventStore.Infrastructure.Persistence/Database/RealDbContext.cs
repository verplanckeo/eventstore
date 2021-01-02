using EventStore.Infrastructure.Persistence.Factories;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Database
{
    public class RealDbContext : BaseDbContext
    {
        private readonly ISqlConnectionManager _sqlConnectionFactory;

        public RealDbContext(ISqlConnectionManager connectionManager)
        {
            _sqlConnectionFactory = connectionManager;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var sqlConnection = _sqlConnectionFactory.Get();
            optionsBuilder.UseSqlServer(sqlConnection, dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.MigrationsAssembly("EventStore.Infrastructure.Persistence");
            });
        }
    }
}