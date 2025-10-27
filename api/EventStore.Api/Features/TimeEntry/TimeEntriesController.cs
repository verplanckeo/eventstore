using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.TimeEntry.LoadAllEntries;
using EventStore.Application.Features.TimeEntry.LoadSingleTimeEntry;
using EventStore.Application.Features.TimeEntry.Register;
using EventStore.Application.Features.TimeEntry.Remove;
using EventStore.Application.Features.TimeEntry.Update;
using EventStore.Application.Mediator;
using EventStore.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Api.Features.TimeEntry;

/// <summary>
/// 
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TimeEntriesController : ControllerBase
{
    private readonly IMediatorFactory _mediator;
    private readonly IServiceContext _context;

    /// <summary>
    /// CTor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="context"></param>
    public TimeEntriesController(IMediatorFactory mediator, IServiceContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    /// <summary>
    /// Register a new time entry
    /// </summary>
    /// <param name="request">Time entry registration details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Newly created time entry ID</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Register.Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterTimeEntry(
        [FromBody] Register.Request request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterTimeEntryMediatorCommand
        {
            From = request.From,
            Until = request.Until,
            UserId = request.UserId,
            ProjectId = request.ProjectId,
            ActivityType = request.ActivityType,
            Comment = request.Comment
        };
        
        var scope = _mediator.CreateScope();
        var result = await scope.SendAsync(command, cancellationToken);

        var response = new Register.Response
        {
            TimeEntryId = result.TimeEntryId
        };

        return CreatedAtAction(nameof(GetTimeEntryById), new { id = result.TimeEntryId }, response);
    }

    /// <summary>
    /// Update an existing time entry
    /// </summary>
    /// <param name="id">Time entry ID</param>
    /// <param name="request">Updated time entry details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated time entry ID</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Update.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTimeEntry(
        string id,
        [FromBody] Update.Request request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTimeEntryMediatorCommand
        {
            TimeEntryId = id,
            From = request.From,
            Until = request.Until,
            UserId = request.UserId,
            ProjectId = request.ProjectId,
            ActivityType = request.ActivityType,
            Comment = request.Comment
        };

        var scope = _mediator.CreateScope();
        var result = await scope.SendAsync(command, cancellationToken);

        var response = new Update.Response
        {
            TimeEntryId = result.TimeEntryId
        };

        return Ok(response);
    }

    /// <summary>
    /// Delete a time entry
    /// </summary>
    /// <param name="id">Time entry ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveTimeEntry(
        string id,
        CancellationToken cancellationToken)
    {
        var command = new RemoveTimeEntryMediatorCommand
        {
            TimeEntryId = id
        };

        var scope = _mediator.CreateScope();
        await scope.SendAsync(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Get all active time entries
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of time entries</returns>
    [HttpGet]
    [ProducesResponseType(typeof(LoadAllTimeEntries.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTimeEntries(CancellationToken cancellationToken)
    {
        var query = new LoadAllTimeEntriesMediatorQuery();
        var scope = _mediator.CreateScope();
        var result = await scope.SendAsync(query, cancellationToken);

        var response = new LoadAllTimeEntries.Response
        {
            TimeEntries = result.TimeEntries.ToList()
        };

        return Ok(response);
    }

    /// <summary>
    /// Get all active time entries
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of time entries</returns>
    [HttpGet("me")]
    [ProducesResponseType(typeof(LoadAllTimeEntries.Response), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTimeEntriesForAuthenticatedUser(CancellationToken cancellationToken)
    {
        var authenticatedUser = _context.UserId;
        var query = LoadAllTimeEntriesMediatorQuery.Create(authenticatedUser);
        var scope = _mediator.CreateScope();
        var result = await scope.SendAsync(query, cancellationToken);

        var response = new LoadAllTimeEntries.Response
        {
            TimeEntries = result.TimeEntries.ToList()
        };

        return Ok(response);
    }

    /// <summary>
    /// Get a specific time entry by ID
    /// </summary>
    /// <param name="id">Time entry ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Time entry details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LoadSingleTimeEntry.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTimeEntryById(
        string id,
        CancellationToken cancellationToken)
    {
        var query = new LoadSingleTimeEntryMediatorQuery
        {
            TimeEntryId = id
        };

        var scope = _mediator.CreateScope();
        var result = await scope.SendAsync(query, cancellationToken);

        if (result.TimeEntry == null)
            return NotFound();

        var response = new LoadSingleTimeEntry.Response
        {
            TimeEntry = result.TimeEntry
        };

        return Ok(response);
    }
}