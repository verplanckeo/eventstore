using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.User.Authenticate;
using EventStore.Application.Features.User.LoadAllUsers;
using EventStore.Application.Features.User.Register;
using EventStore.Application.Mediator;
using EventStore.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.User
{
    /// <summary>
    /// Api controller for basic user management (register, list, change ...)
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediatorFactory _mediatorFactory;

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="mediatorFactory"></param>
        public UsersController(IMediatorFactory mediatorFactory)
        {
            _mediatorFactory = mediatorFactory;
        }

        /// <summary>
        /// Register new user on the EventStore Api
        /// </summary>
        /// <param name="request">Request to register user</param>
        /// <param name="cancellationToken">When cancellation is requested</param>
        /// <returns></returns>
        [HttpPost("register")]
        [SwaggerRequestExample(typeof(Register.Request), typeof(Register.RequestExample))]
        [ProducesResponseType(typeof(Register.Response), (int)HttpStatusCode.OK)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(Register.ResponseExample))]
        public async Task<ObjectResult> Register(Register.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(
                    RegisterUserMediatorCommand.CreateCommand(request.UserName, request.FirstName, request.LastName, request.Password), 
                    cancellationToken);

                var response = Features.User.Register.Response.Create(mediatorResponse.Id, request.UserName, request.FirstName, request.LastName);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        //TODO: Use a real IDP (Azure B2C, Facebook, Google ...) instead of e-mail password. This implementation is a placeholder
        /// <summary>
        /// Authenticate user on the EventStore Api
        /// </summary>
        /// <param name="request">Parameters to authenticate user.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [SwaggerRequestExample(typeof(Authenticate.Request), typeof(Authenticate.RequestExample))]
        [ProducesResponseType(typeof(Authenticate.Response), (int) HttpStatusCode.OK)]
        [SwaggerResponseExample((int) HttpStatusCode.OK, typeof(Authenticate.ResponseExample))]
        public async Task<ObjectResult> Authenticate(Authenticate.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(AuthenticateUserMediatorCommand.CreateCommand(request.UserName, request.Password), cancellationToken);

                return new OkObjectResult(mediatorResponse);
            }
            catch (KeyNotFoundException knfe)
            {
                //We do not want to give info if a user is present on our platform or not. However, we do want to know as IT if someone attempts to find out if a user is on this platform
                //TODO: Add tracing here - User account was not found
                //_telemetryClient.TraceEvent($"{nameof(Authentication)}FailedEvent", new { Code = ErrorMessages.ErrorUserNotFound, Exception = knfe, Message = knfe.Message });
                Console.WriteLine(knfe);
                return new BadRequestObjectResult(new {ErrorCode = ErrorMessages.User.ErrorUserNotAuthenticated, ErrorMessage = "Could not authenticate the given user."});
            }
            catch (InvalidCredentialException ice)
            {
                //TODO: Add tracing here - Incorrect credentials (if this happens too often for 1 given account someone is bruteforcing their way in)
                //_telemetryClient.TraceEvent($"{nameof(Authentication)}FailedEvent", new { Code = ErrorMessages.ErrorUserInvalidPassword, Exception = ice, Message = ice.Message });
                Console.WriteLine(ice);
                return new BadRequestObjectResult(new { ErrorCode = ErrorMessages.User.ErrorUserNotAuthenticated, ErrorMessage = "Could not authenticate the given user." });
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here - Something else went wrong but we don't know what
                //_telemetryClient.TraceEvent($"{nameof(Authentication)}FailedEvent", new { Code = ErrorMessages.ErrorUserNotAuthenticated, Exception = ex, Message = ex.Message });
                Console.WriteLine(ex);
                return new BadRequestObjectResult(new { ErrorCode = ErrorMessages.User.ErrorUserNotAuthenticated, ErrorMessage = "Could not authenticate the given user." });
            }
        }

        /// <summary>
        /// Get all users active on the platform
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(LoadAllUsers.Response), (int)HttpStatusCode.OK)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(LoadAllUsers.ResponseExample))]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(LoadAllUsersMediatorCommand.CreateCommand(), cancellationToken);

                var response = LoadAllUsers.Response.Create(mediatorResponse.Users);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
