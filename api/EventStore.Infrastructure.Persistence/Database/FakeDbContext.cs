using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Database
{
    public sealed class FakeDbContext : BaseDbContext
    {
        public FakeDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var randomUniqueInMemoryDatabaseInstanceName = "FakeDbInstance";

            optionsBuilder.UseInMemoryDatabase(randomUniqueInMemoryDatabaseInstanceName);
        }

        public override void Dispose()
        {
            Database.EnsureDeleted();
        }
    }
}