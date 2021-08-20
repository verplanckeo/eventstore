using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class ResolvedTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.Resolved;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public ResolvedTicketState()
        {
            ValidTransitions = new[] {TicketState.InProgress, TicketState.Done, TicketState.Closed, TicketState.Removed};
        }
    }
}