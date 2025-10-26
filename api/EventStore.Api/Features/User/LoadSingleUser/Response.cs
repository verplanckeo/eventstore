using EventStore.Application.Features.User;

namespace EventStore.Api.Features.User.LoadSingleUser;

/// <summary>
/// Response containing single user.
/// </summary>
public class Response
{
    /// <summary>
    /// List of registered users.
    /// </summary>
    public ReadUserModel User { get; set; }

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
    public static Response Create(ReadUserModel user)
    {
        return new Response { User = user };
    }
}