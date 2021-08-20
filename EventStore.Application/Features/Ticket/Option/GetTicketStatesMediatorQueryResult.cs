using System.Collections.Generic;

namespace EventStore.Application.Features.Ticket.Option
{
    /// <summary>
    /// Result returned when fetching available ticket states
    /// </summary>
    public class GetTicketStatesMediatorQueryResult
    {
        /// <summary>
        /// List of all available states a ticket can have.
        /// </summary>
        public IEnumerable<string> States { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        public GetTicketStatesMediatorQueryResult()
        {
            
        }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="states"><see cref="States"/></param>
        public GetTicketStatesMediatorQueryResult(IEnumerable<string> states)
        {
            States = states;
        }

        /// <summary>
        /// Create new instance of <see cref="GetTicketStatesMediatorQueryResult"/>
        /// </summary>
        /// <param name="states"><see cref="States"/></param>
        /// <returns></returns>
        public static GetTicketStatesMediatorQueryResult CreateResult(IEnumerable<string> states) => new GetTicketStatesMediatorQueryResult(states);
    }
}