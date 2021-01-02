using EventStore.Infrastructure.Persistence.Factories;

namespace EventStore.Infrastructure.Persistence.Database
{
    public class RealDbContext : BaseDbContext
    {
        private readonly ISqlConnectionManager _sqlConnectionFactory;
    }
}