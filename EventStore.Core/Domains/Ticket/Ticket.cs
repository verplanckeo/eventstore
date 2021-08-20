using System.Collections.Generic;
using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.Ticket.DomainEvents;
using EventStore.Core.Domains.Ticket.Option;
using EventStore.Core.Domains.Ticket.StateMachine;

namespace EventStore.Core.Domains.Ticket
{
    public class Ticket : EventSourcedAggregateRoot<TicketId>
    {
        public override TicketId Id { get; protected set; }
        public TicketType TicketType { get; private set; }
        public TicketPriority TicketPriority { get; private set; }
        public TicketStateMachineBase TicketStateMachine { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        //TODO: Rework default constructor - it is only used for unit tests
        /// <summary>
        /// DO NOT USE THIS CTOR!
        /// </summary>
        public Ticket()
        { }

        /// <summary>
        /// When an aggregate has been fetched from db, we call this CTor which will apply all events and increase the Version by 1.
        /// </summary>
        /// <param name="events"></param>
        public Ticket(IEnumerable<IDomainEvent> events): base(events){ }

        public static Ticket CreateNewTicket(string title, string description, TicketType ticketType, TicketPriority ticketPriority)
        {
            var ticket = new Ticket();
            ticket.Apply(new TicketRegisteredDomainEvent(new TicketId().ToString(), title, description, ticketType, ticketPriority));

            return ticket;
        }
        //When changing the state of our ticket, this is what the external classes are calling
        public void ChangeTicketState(TicketState newState)
        {
            TicketStateMachine.ChangeState(this, TicketStateMachineBase.GetTicketState(newState));
        }

        //This is the 'callback' method called from within our ticket state machine.
        internal void TransitionToState(TicketStateMachineBase newState)
        {
            Apply(new TicketStateChangedDomainEvent(newState));
        }

        //To know how this method is called, check ...\EventStore.Core\DddSeedwork\EventSourcedAggregateRoot.cs
        //Using dynamic we call the "On(event)" method
        public void On(TicketRegisteredDomainEvent evt)
        {
            Id = new TicketId(evt.Id);
            Title = evt.Title;
            Description = evt.Description;
            TicketType = evt.TicketType;
            TicketPriority = evt.TicketPriority;
        }

        public void On(TicketStateChangedDomainEvent evt)
        {
            TicketStateMachine = TicketStateMachineBase.GetTicketState(evt.State);
        }
    }
}