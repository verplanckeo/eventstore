using System;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.Ticket
{
    public class TicketId : EntityId
    {
        private Guid _ticketId;

        public TicketId()
        {
            _ticketId = Guid.NewGuid();
        }

        public TicketId(string id)
        {
            if (!Guid.TryParse(id, out var ticketId)) throw new FormatException("Id of TicketId entity should be a guid (i.e.: D5E717DF-EDDE-433B-947D-0CE8EE20E4A0).");
            
            _ticketId = ticketId;
        }

        public override string ToString() => _ticketId.ToString();
    }
}