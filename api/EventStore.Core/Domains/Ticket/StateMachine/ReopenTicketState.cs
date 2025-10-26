using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class ReopenTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.Reopen;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public ReopenTicketState()
        {
            ValidTransitions = new[] {TicketState.InProgress, TicketState.Resolved, TicketState.Removed};
        }
    }
}