using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.Ticket.Option;
using EventStore.Core.Domains.Ticket.StateMachine;

namespace EventStore.Core.Domains.Ticket.DomainEvents
{
    public class TicketStateChangedDomainEvent : DomainEvent
    {
        public TicketState State { get; set; }

        public TicketStateChangedDomainEvent(TicketStateMachineBase stateMachine)
        {
            State = stateMachine.State;
        }
    }
}