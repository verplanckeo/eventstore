using EventStore.Application.Features.Project;

namespace EventStore.Api.Features.Project.LoadSingleProject;

/// <summary>
/// Response containing single project.
/// </summary>
public class Response
{
    /// <summary>
    /// Project data
    /// </summary>
    public ReadProjectModel Project { get; set; }

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
    public static Response Create(ReadProjectModel project)
    {
        return new Response { Project = project };
    }
}