namespace EventStore.Application.Features.Project.LoadSingleProject;

public class LoadSingleProjectMediatorQueryResult
{
    public ReadProjectModel Project { get; set; }
    
    public static LoadSingleProjectMediatorQueryResult CreateResult(ReadProjectModel project)
    {
        return new LoadSingleProjectMediatorQueryResult { Project = project };
    }
}