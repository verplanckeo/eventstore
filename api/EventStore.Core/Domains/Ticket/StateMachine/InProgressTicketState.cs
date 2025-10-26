using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class InProgressTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.InProgress;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public InProgressTicketState()
        {
            ValidTransitions = new[] {TicketState.New, TicketState.Resolved, TicketState.Removed};
        }
    }
}