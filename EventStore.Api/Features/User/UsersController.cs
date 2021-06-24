using System;
using System.Threading.Tasks;
using EventStore.Api.Features.User.Register;
using EventStore.Application.Features.User.Register;
using EventStore.Application.Mediator;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ObjectResult> Register([FromBody] Request request)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(
                    RegisterUserMediatorCommand.CreateCommand(request.UserName, request.FirstName, request.LastName, request.Password), 
                    default);

                var response = Features.User.Register.Response.Create(mediatorResponse.Id, "api-token", request.UserName, request.FirstName, request.LastName);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
