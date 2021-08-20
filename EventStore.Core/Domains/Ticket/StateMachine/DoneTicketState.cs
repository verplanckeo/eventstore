using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class DoneTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.Done;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public DoneTicketState()
        {
            ValidTransitions = new[] {TicketState.Reopen};
        }
    }
}