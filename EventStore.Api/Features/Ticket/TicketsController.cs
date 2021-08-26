using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Api.Features.Ticket.Models;
using EventStore.Api.Seedwork;
using EventStore.Application.Features.Ticket.Option;
using EventStore.Application.Features.Ticket.Register;
using EventStore.Application.Mediator;
using EventStore.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.Ticket
{
    /// <summary>
    /// Api controller for ticket management (add, edit, list ...)
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IMediatorFactory _mediatorFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="mediatorFactory"></param>
        /// <param name="httpContextAccessor"></param>
        public TicketsController(IMediatorFactory mediatorFactory, IHttpContextAccessor httpContextAccessor)
        {
            _mediatorFactory = mediatorFactory;
            _httpContextAccessor = httpContextAccessor;
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

            return type switch
            {
                TicketOptionType.TicketPriority => new ObjectResult(
                    await scope.SendAsync(GetTicketPrioritiesMediatorQuery.CreateQuery(), cancellationToken)),
                TicketOptionType.TicketState => new ObjectResult(
                    await scope.SendAsync(GetTicketStatesMediatorQuery.CreateQuery(), cancellationToken)),
                TicketOptionType.TicketType => new ObjectResult(
                    await scope.SendAsync(GetTicketTypesMediatorQuery.CreateQuery(), cancellationToken)),
                _ => new BadRequestObjectResult("Please use a valid ticket option.")
            };
        }

        /// <summary>
        /// Register a new ticket.
        /// </summary>
        /// <param name="request">Parameters to register a new ticket with.</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        [HttpPost("")]
        [SwaggerRequestExample(typeof(Register.Request), typeof(Register.RequestExample))]
        [ProducesResponseType(typeof(Register.Response), (int) HttpStatusCode.OK)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(Register.ResponseExample))]
        public async Task<ObjectResult> RegisterTicket(Register.Request request, CancellationToken cancellationToken)
        {
            var scope = _mediatorFactory.CreateScope();

            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.GetClaimsValue(Security.Claims.Identifier);
                var userName = _httpContextAccessor.HttpContext?.User.GetClaimsValue(Security.Claims.Name);
                var mediatorResponse = await scope.SendAsync(RegisterTicketMediatorCommand.CreateCommand(
                    userId, 
                    userName,
                    request.Title, 
                    request.Description,
                    request.TicketType.ToDomainTicketType(), 
                    request.TicketPriority.ToDomainTicketPriority()), 
                    cancellationToken);

                return new OkObjectResult(mediatorResponse);
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here 
                //_telemetryClient.TraceEvent($"{nameof(RegisterTicket)}FailedEvent", new { Code = ErrorMessages.Ticket.TicketNotRegistered, Exception = ex, Message = ex.Message });
                Console.WriteLine(ex);
                return new BadRequestObjectResult(new {ErrorCode = ErrorMessages.Ticket.TicketNotRegistered, ErrorMessage = "Could not register ticket."});
            }
        }
    }
}
