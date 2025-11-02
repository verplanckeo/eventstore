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

namespace EventStore.Application.Features.TimeEntry.Register;

public class RegisterTimeEntryMediatorCommandHandler : IRequestHandler<RegisterTimeEntryMediatorCommand, RegisterTimeEntryMediatorCommandResponse>
{
    private readonly ITimeEntryRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMediator _mediator;

    public RegisterTimeEntryMediatorCommandHandler(ITimeEntryRepository repository, IUserRepository userRepository, IProjectRepository projectRepository, IMediator mediator)
    {
        _repository = repository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _mediator = mediator;
    }

    public async Task<RegisterTimeEntryMediatorCommandResponse> Handle(RegisterTimeEntryMediatorCommand request, CancellationToken cancellationToken)
    {
        var timeEntryId = new TimeEntryId();
        
        var user = await _userRepository.LoadUserAsync(new UserId(request.UserId), cancellationToken);
        var project = await _projectRepository.LoadProjectAsync(new ProjectId(request.ProjectId), cancellationToken);
            
        var timeEntry = Core.Domains.TimeEntry.TimeEntry.RegisterTimeEntry(
            timeEntryId,
            request.From,
            request.Until,
            request.UserId,
            request.ProjectId,
            request.ActivityType,
            request.Comment);

        await _repository.SaveAsync(timeEntry, cancellationToken);

        // Update read model
        var updateReadCommand = UpdateReadTimeEntryCommand.CreateCommand(
            timeEntryId.ToString(),
            timeEntry.From,
            timeEntry.Until,
            timeEntry.TotalSeconds,
            user.Id.ToString(),
            user.UserName,
            project.Id.ToString(),
            project.Code,
            timeEntry.ActivityType,
            timeEntry.Comment,
            timeEntry.IsRemoved,
            timeEntry.Version);

        await _mediator.Send(updateReadCommand, cancellationToken);

        return RegisterTimeEntryMediatorCommandResponse.CreateResponse(timeEntryId.ToString());
    }
}