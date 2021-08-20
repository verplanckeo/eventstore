﻿namespace EventStore.Api.Features.Models
{
    /// <summary>
    /// State of ticket (i.e. Closed).
    /// </summary>
    public enum TicketState
    {
        /// <summary>
        /// State when a new ticket is registered.
        /// </summary>
        New = 0,
        /// <summary>
        /// State once someone picked up the ticket.
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// State if ticket has been solved but not yet verified.
        /// </summary>
        Resolved = 2,
        /// <summary>
        /// State when ticket has been solved and verified.
        /// </summary>
        Done = 3,
        /// <summary>
        /// State when it has been decided the ticket won't be handled.
        /// </summary>
        Closed = 4
    }
}