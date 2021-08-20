using System.Collections;
using System.Collections.Generic;

namespace EventStore.Application.Features.Ticket.Option
{
    /// <summary>
    /// Result returned when fetching available priorities a ticket can have.
    /// </summary>
    public class GetTicketPrioritiesMediatorQueryResult
    {
        /// <summary>
        /// List of all available priorities a ticket can have.
        /// </summary>
        public IEnumerable<string> Priorities { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        public GetTicketPrioritiesMediatorQueryResult()
        {
            
        }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="priorities"><see cref="Priorities"/></param>
        public GetTicketPrioritiesMediatorQueryResult(IEnumerable<string> priorities)
        {
            Priorities = priorities;
        }

        /// <summary>
        /// Create instance of <see cref="GetTicketPrioritiesMediatorQueryResult"/>
        /// </summary>
        /// <param name="priorities"><see cref="Priorities"/></param>
        /// <returns></returns>
        public static GetTicketPrioritiesMediatorQueryResult CreateResult(IEnumerable<string> priorities) => new GetTicketPrioritiesMediatorQueryResult(priorities);
    }
}