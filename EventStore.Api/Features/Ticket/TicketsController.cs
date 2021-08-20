using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Api.Features.Models;
using EventStore.Application.Features.Ticket.Option;
using EventStore.Application.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Api.Features.Ticket
{
    /// <summary>
    /// Api controller for ticket management (add, edit, list ...)
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediatorFactory _mediatorFactory;

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="mediatorFactory"></param>
        public TicketsController(IMediatorFactory mediatorFactory)
        {
            _mediatorFactory = mediatorFactory;
        }

        /// <summary>
        /// Get list of ticket options (Priorities, States or Types).
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("options/{type}")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int) HttpStatusCode.OK)]
        public async Task<ObjectResult> GetTicketPriorities(TicketOptionType type, CancellationToken cancellationToken)
        {
            var scope = _mediatorFactory.CreateScope();

            switch (type)
            {
                case TicketOptionType.TicketPriority:
                    return new ObjectResult(await scope.SendAsync(GetTicketPrioritiesMediatorQuery.CreateQuery(), cancellationToken));
                case TicketOptionType.TicketState:
                    return new ObjectResult(await scope.SendAsync(GetTicketStatesMediatorQuery.CreateQuery(), cancellationToken));
                case TicketOptionType.TicketType:
                    return new ObjectResult(await scope.SendAsync(GetTicketTypesMediatorQuery.CreateQuery(), cancellationToken));
            }

            return new BadRequestObjectResult("Please use a valid ticket option.");
        }
    }
}
