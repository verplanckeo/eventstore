using System.Collections.Generic;
using EventStore.Application.Features.Project;

namespace EventStore.Api.Features.Project.LoadAllProjects;

/// <summary>
/// Response containing all projects in our system.
/// </summary>
public class Response
{
    /// <summary>
    /// List of registered projects.
    /// </summary>
    public IEnumerable<ReadProjectModel> Projects { get; set; }

    /// <summary>
    /// CTor
    /// </summary>
    private Response()
    {
            
    }

    /// <summary>
    /// Create new instance of <see cref="Response"/>
    /// </summary>
    /// <returns></returns>
    public static Response Create(IEnumerable<ReadProjectModel> projects)
    {
        return new Response { Projects = projects };
    }
}