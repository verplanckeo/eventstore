using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.UpdateReadTimeEntry;

public class UpdateReadTimeEntryCommandHandler(IReadTimeEntryRepository repository) : IRequestHandler<UpdateReadTimeEntryCommand>
{
    public async Task Handle(UpdateReadTimeEntryCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.SaveOrUpdateTimeEntryAsync(
            ReadTimeEntryModel.CreateNewReadTimeEntry(request.AggregateRootId, request.From, request.Until, request.UserId, request.UserName, request.ProjectId, request.ProjectCode, request.ActivityType, request.Comment, request.IsRemoved, request.Version),
            cancellationToken);

        if (string.IsNullOrEmpty(result))
        {
            //could not save / update read model
        }
    }
}