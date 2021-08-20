using System.Collections.Generic;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public class NewTicketState : TicketStateMachineBase
    {
        public override TicketState State => TicketState.New;
        public override IEnumerable<TicketState> ValidTransitions { get; }

        public NewTicketState()
        {
            ValidTransitions = new[] {TicketState.InProgress, TicketState.Removed};
        }
    }
}