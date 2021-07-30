using EventStore.Infrastructure.Persistence.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore.Infrastructure.Persistence.Configurations
{
    public class ReadUserConfiguration : IEntityTypeConfiguration<ReadUser>
    {
        public void Configure(EntityTypeBuilder<ReadUser> builder)
        {
            builder.ToTable(Constants.Database.TableReadUser, Constants.Database.SchemaRead);

            builder.HasKey(key => new { key.AggregateRootId });

            builder.HasKey(key => new { key.UserName });
        }
    }
}