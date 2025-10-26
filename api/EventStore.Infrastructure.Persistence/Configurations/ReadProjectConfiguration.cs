using EventStore.Infrastructure.Persistence.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore.Infrastructure.Persistence.Configurations;

public class ReadProjectConfiguration : IEntityTypeConfiguration<ReadProject>
{
    public void Configure(EntityTypeBuilder<ReadProject> builder)
    {
        builder.ToTable(Constants.Database.TableReadProject, Constants.Database.SchemaRead);

        builder.HasKey(key => new { key.AggregateRootId });

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(25);

        builder.HasIndex(model => new { model.Code }, "IX_ProjectCode")
            .IsUnique()
            .IncludeProperties(model => new { model.Name, model.Billable, model.IsRemoved });
    }
}