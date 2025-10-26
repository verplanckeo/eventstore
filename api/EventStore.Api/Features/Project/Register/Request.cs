namespace EventStore.Api.Features.Project.Register;

/// <summary>
/// Request model to register a new project.
/// </summary>
public class Request
{
    /// <summary>
    /// Project name (max 100 characters)
    /// </summary>
    public string Name { get; }
            
    /// <summary>
    /// Project code (max 25 characters)
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Is the project billable
    /// </summary>
    public bool Billable { get; }

    /// <summary>
    /// CTor
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="code"><see cref="Code"/></param>
    /// <param name="billable"><see cref="Billable"/></param>
    public Request(string name, string code, bool billable)
    {
        Name = name;
        Code = code;
        Billable = billable;
    }

    /// <summary>
    /// Create a new instance of <see cref="Request"/>
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="code"><see cref="Code"/></param>
    /// <param name="billable"><see cref="Billable"/></param>
    /// <returns></returns>
    public static Request Create(string name, string code, bool billable) => new Request(name, code, billable);
}