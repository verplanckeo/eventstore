
using EventStore.Application.Entities.User;
using EventStore.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventStore.Infrastructure.Persistence.Database
{
    public class BaseDbContext : DbContext, IDatabaseContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfiguration(modelBuilder);
        }

        private void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            //add configurations here
        }

        public DbSet<ReadUser> ReadUsers { get; set; }
    }
}