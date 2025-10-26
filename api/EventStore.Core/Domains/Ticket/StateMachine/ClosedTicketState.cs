using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class ClosedTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.Closed;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public ClosedTicketState()
        {
            ValidTransitions = new[] {TicketState.Reopen};
        }
    }
}
