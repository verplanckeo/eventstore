using System.Collections.Generic;

namespace EventStore.Application.Features.Project.LoadAllProjects;

public class LoadAllProjectsMediatorQueryResult
{
    public IEnumerable<ReadProjectModel> Projects { get; set; }

    public static LoadAllProjectsMediatorQueryResult CreateResult(IEnumerable<ReadProjectModel> projects)
    {
        return new LoadAllProjectsMediatorQueryResult { Projects = projects };
    }
}