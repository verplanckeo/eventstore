namespace EventStore.Api.Features.Project.Register;

/// <summary>
/// Response model when project successfully registered.
/// </summary>
public class Response
{
    /// <summary>
    /// Identifier of the project
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// CTor
    /// </summary>
    /// <param name="id"><see cref="Id"/></param>
    public Response(string id)
    {
        Id = id;
    }

    /// <summary>
    /// Create a new instance of <see cref="Response"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Response Create(string id) => new Response(id);
}