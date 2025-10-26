using System;
using System.Collections.Generic;
using System.Linq;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Core.Domains.Ticket.StateMachine
{
    public abstract class TicketStateMachineBase
    {
        /// <summary>
        /// State of ticket.
        /// </summary>
        public abstract TicketState State { get; }

        /// <summary>
        /// Valid transitions the current state can make.
        /// </summary>
        public abstract IEnumerable<TicketState> ValidTransitions { get; }

        public virtual void ChangeState(Ticket ticket, TicketStateMachineBase ticketState)
        {
            if (ticketState == null)
                throw new NullReferenceException("TicketState is null.");
            if (!ValidTransitions.Contains(ticketState.State))
                throw new InvalidOperationException($"Can not transition tickets current state from '{State}' to {ticketState.State}.");

            ticket.TransitionToState(ticketState);
        }

        public static TicketStateMachineBase GetTicketState(TicketState state)
        {
            return state switch
            {
                TicketState.New => new NewTicketState(),
                TicketState.InProgress => new InProgressTicketState(),
                TicketState.Resolved => new ResolvedTicketState(),
                TicketState.Done => new DoneTicketState(),
                TicketState.Closed => new ClosedTicketState(),
                TicketState.Removed => new RemovedTicketState(),
                TicketState.Reopen => new ReopenTicketState(),
                _ => new NewTicketState()
            };
        }
    }
}