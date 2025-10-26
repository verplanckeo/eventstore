using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.Project;
using MediatR;

namespace EventStore.Application.Features.Project.UpdateReadProject;

public class UpdateReadProjectCommandHandler : IRequestHandler<UpdateReadProjectCommand>
{
    private readonly IReadProjectRepository _repository;

    public UpdateReadProjectCommandHandler(IReadProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateReadProjectCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.SaveOrUpdateProjectAsync(
            ReadProjectModel.CreateNewReadProject(
                request.AggregateRootId, 
                request.Name, 
                request.Code, 
                request.Billable,
                request.IsRemoved,
                request.Version), 
            cancellationToken);

        if (string.IsNullOrEmpty(result))
        {
            //could not save / update read model
        }
    }
}