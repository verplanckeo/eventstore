using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.Project.LoadAllProjects;
using EventStore.Application.Features.Project.LoadSingleProject;
using EventStore.Application.Features.Project.Register;
using EventStore.Application.Features.Project.Remove;
using EventStore.Application.Features.Project.Update;
using EventStore.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EventStore.Api.Features.Project;

/// <summary>
    /// Api controller for project management (add, edit, list, delete)
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediatorFactory _mediatorFactory;

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="mediatorFactory"></param>
        public ProjectsController(IMediatorFactory mediatorFactory)
        {
            _mediatorFactory = mediatorFactory;
        }

        /// <summary>
        /// Register a new project
        /// </summary>
        /// <param name="request">Request to register project</param>
        /// <param name="cancellationToken">When cancellation is requested</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Register.Response), (int)HttpStatusCode.OK)]
        public async Task<ObjectResult> RegisterProject(Register.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(
                    RegisterProjectMediatorCommand.CreateCommand(request.Name, request.Code, request.Billable), 
                    cancellationToken);

                var response = Register.Response.Create(mediatorResponse.Id);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here
                Console.WriteLine(ex);
                return new BadRequestObjectResult(new { ErrorCode = "ERR_PROJECT_NOT_REGISTERED", ErrorMessage = "Could not register project." });
            }
        }

        /// <summary>
        /// Update an existing project
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <param name="request">Request to update project</param>
        /// <param name="cancellationToken">When cancellation is requested</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Update.Response), (int)HttpStatusCode.OK)]
        public async Task<ObjectResult> UpdateProject(string id, Update.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(
                    UpdateProjectMediatorCommand.CreateCommand(id, request.Name, request.Code, request.Billable), 
                    cancellationToken);

                var response = Update.Response.Create(mediatorResponse.Id);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here
                Console.WriteLine(ex);
                return new BadRequestObjectResult(new { ErrorCode = "ERR_PROJECT_NOT_UPDATED", ErrorMessage = "Could not update project." });
            }
        }

        /// <summary>
        /// Delete/Remove a project
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <param name="cancellationToken">When cancellation is requested</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteProject(string id, CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                await scope.SendAsync(RemoveProjectMediatorCommand.CreateCommand(id), cancellationToken);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here
                Console.WriteLine(ex);
                return new BadRequestObjectResult(new { ErrorCode = "ERR_PROJECT_NOT_DELETED", ErrorMessage = "Could not delete project." });
            }
        }

        /// <summary>
        /// Get all projects active on the platform
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(LoadAllProjects.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(LoadAllProjectsMediatorQuery.CreateQuery(), cancellationToken);

                var response = LoadAllProjects.Response.Create(mediatorResponse.Projects);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here
                Console.WriteLine(ex);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Get a single project from our platform.
        /// </summary>
        /// <param name="id">AggregateRootId of the project we wish to retrieve.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LoadSingleProject.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSingleProject(string id, CancellationToken cancellationToken)
        {
            try
            {
                var scope = _mediatorFactory.CreateScope();
                var mediatorResponse = await scope.SendAsync(LoadSingleProjectMediatorQuery.CreateQuery(id), cancellationToken);

                var response = LoadSingleProject.Response.Create(mediatorResponse.Project);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                //TODO: Add tracing here
                Console.WriteLine(ex);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }