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
            switch (state)
            {
                case TicketState.New:
                    return new NewTicketState();
                case TicketState.InProgress:
                    return new InProgressTicketState();
                case TicketState.Resolved:
                    return new ResolvedTicketState();
                case TicketState.Done:
                    return new DoneTicketState();
                case TicketState.Closed:
                    return new ClosedTicketState();
                case TicketState.Removed:
                    return new RemovedTicketState();
                case TicketState.Reopen:
                    return new ReopenTicketState();
                default:
                    return new NewTicketState();
            }
        }
    }
}