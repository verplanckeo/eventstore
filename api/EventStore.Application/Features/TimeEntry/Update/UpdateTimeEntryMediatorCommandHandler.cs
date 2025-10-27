using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Features.TimeEntry.UpdateReadTimeEntry;
using EventStore.Application.Repositories.Project;
using EventStore.Application.Repositories.TimeEntry;
using EventStore.Application.Repositories.User;
using EventStore.Core.Domains.Project;
using EventStore.Core.Domains.TimeEntry;
using EventStore.Core.Domains.User;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.Update;

public class UpdateTimeEntryMediatorCommandHandler : IRequestHandler<UpdateTimeEntryMediatorCommand, UpdateTimeEntryMediatorCommandResponse>
{
    private readonly ITimeEntryRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMediator _mediator;

    public UpdateTimeEntryMediatorCommandHandler(ITimeEntryRepository repository, IUserRepository userRepository, IProjectRepository projectRepository, IMediator mediator)
    {
        _repository = repository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _mediator = mediator;
    }

    public async Task<UpdateTimeEntryMediatorCommandResponse> Handle(UpdateTimeEntryMediatorCommand request, CancellationToken cancellationToken)
    {
        var timeEntry = await _repository.LoadAsync(request.TimeEntryId, cancellationToken);

        if (timeEntry == null)
            throw new System.Exception($"TimeEntry with id {request.TimeEntryId} not found.");
        
        var user = await _userRepository.LoadUserAsync(new UserId(request.UserId), cancellationToken);
        var project = await _projectRepository.LoadProjectAsync(new ProjectId(request.ProjectId), cancellationToken);
        
        timeEntry.ChangeTimeEntry(
            request.From,
            request.Until,
            request.UserId,
            request.ProjectId,
            request.ActivityType,
            request.Comment);

        await _repository.SaveAsync(timeEntry, cancellationToken);

        // Update read model
        var updateReadCommand = UpdateReadTimeEntryCommand.CreateCommand(
            timeEntry.ToString(),
            request.From,
            request.Until,
            request.UserId,
            user.UserName,
            request.ProjectId,
            project.Code,
            request.ActivityType,
            request.Comment,
            false,
            timeEntry.Version);

        await _mediator.Send(updateReadCommand, cancellationToken);

        return UpdateTimeEntryMediatorCommandResponse.CreateResponse(timeEntry.ToString());
    }
}