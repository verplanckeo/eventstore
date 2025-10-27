using EventStore.Infrastructure.Persistence.Entities.TimeEntry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore.Infrastructure.Persistence.Configurations;

public class ReadTimeEntryConfiguration : IEntityTypeConfiguration<ReadTimeEntry>
{
    public void Configure(EntityTypeBuilder<ReadTimeEntry> builder)
    {
        builder.ToTable(Constants.Database.TableReadTimeEntry, Constants.Database.SchemaRead);

        builder.HasKey(e => e.AggregateRootId);

        builder.Property(e => e.AggregateRootId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(e => e.From)
            .IsRequired();

        builder.Property(e => e.Until)
            .IsRequired();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ProjectId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(e => e.ProjectCode)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(e => e.ActivityType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Comment)
            .HasMaxLength(100);

        builder.Property(e => e.IsRemoved)
            .IsRequired();

        builder.Property(e => e.Version)
            .IsRequired();

        // Create indexes for common queries
        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => e.ProjectId);
        builder.HasIndex(e => e.From);
        builder.HasIndex(e => new { e.IsRemoved, e.From });
    }
}