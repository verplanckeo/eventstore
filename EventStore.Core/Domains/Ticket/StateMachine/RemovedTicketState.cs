using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class RemovedTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.Removed;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public RemovedTicketState()
        {
            ValidTransitions = new TicketState[0];
        }
    }
}