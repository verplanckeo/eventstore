using System.Collections;
using System.Collections.Generic;

namespace EventStore.Application.Features.Ticket.Option
{
    /// <summary>
    /// Result returned when fetching all available ticket types.
    /// </summary>
    public class GetTicketTypesMediatorQueryResult
    {
        /// <summary>
        /// List of all available ticket types.
        /// </summary>
        public IEnumerable<string> Types { get; set; }

        /// <summary>
        /// CTor
        /// </summary>
        public GetTicketTypesMediatorQueryResult()
        {
            
        }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="types"><see cref="Types"/></param>
        public GetTicketTypesMediatorQueryResult(IEnumerable<string> types)
        {
            Types = types;
        }

        /// <summary>
        /// Create new instance of <see cref="GetTicketTypesMediatorQueryResult"/>.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public GetTicketTypesMediatorQueryResult CreateResult(IEnumerable<string> types) => new GetTicketTypesMediatorQueryResult(types);
    }
}