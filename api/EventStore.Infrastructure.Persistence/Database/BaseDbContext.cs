using EventStore.Infrastructure.Persistence.Configurations;
using EventStore.Infrastructure.Persistence.Entities;
using EventStore.Infrastructure.Persistence.Entities.Project;
using EventStore.Infrastructure.Persistence.Entities.Ticket;
using EventStore.Infrastructure.Persistence.Entities.TimeEntry;
using EventStore.Infrastructure.Persistence.Entities.User;
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
            modelBuilder.ApplyConfiguration(new EventStoreRecordConfiguration());
            modelBuilder.ApplyConfiguration(new ReadUserConfiguration());
            modelBuilder.ApplyConfiguration(new ReadTicketConfiguration());
            modelBuilder.ApplyConfiguration(new ReadProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReadTimeEntryConfiguration());
        }

        public DbSet<EventStoreRecord> EventStoreRecords { get; set; }

        public DbSet<ReadUser> ReadUsers { get; set; }

        public DbSet<ReadTicket> ReadTickets { get; set; }
        
        public DbSet<ReadProject> ReadProjects { get; set; }
        
        public DbSet<ReadTimeEntry> ReadTimeEntries { get; set; }
    }
}