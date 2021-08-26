using EventStore.Infrastructure.Persistence.Entities.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore.Infrastructure.Persistence.Configurations
{
    public class ReadTicketConfiguration : IEntityTypeConfiguration<ReadTicket>
    {
        public void Configure(EntityTypeBuilder<ReadTicket> builder)
        {
            builder.ToTable(Constants.Database.TableReadTicket, Constants.Database.SchemaRead);

            builder.HasKey(key => new {key.AggregateRootId});

            builder.HasIndex(model => model.UserName, "IX_TicketUserName")
                .IsUnique()
                .IncludeProperties(model => new {model.UserId, model.Title, model.TicketPriority, model.TicketState, model.TicketType, model.Version});
        }
    }
}