﻿using EventStore.Infrastructure.Persistence.Entities.User;
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

            builder.HasIndex(model => new {model.UserName}, "IX_Username")
                .IsUnique()
                .IncludeProperties(model => new{ model.FirstName, model.LastName });
        }
    }
}