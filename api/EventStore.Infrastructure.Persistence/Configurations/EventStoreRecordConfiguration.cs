using EventStore.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore.Infrastructure.Persistence.Configurations
{
    public class EventStoreRecordConfiguration : IEntityTypeConfiguration<EventStoreRecord>
    {
        public void Configure(EntityTypeBuilder<EventStoreRecord> builder)
        {
            builder.ToTable(Constants.Database.TableEventStore, Constants.Database.SchemaWrite);

            builder.HasKey(key => new {key.AggregateRootId, key.Version}); // optimistic concurrency check is done based on these 2 fields

            builder.Property(p => p.Sequence).ValueGeneratedOnAdd(); // auto increment
        }
    }
}