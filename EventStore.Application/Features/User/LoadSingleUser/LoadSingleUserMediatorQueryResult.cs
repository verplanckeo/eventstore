namespace EventStore.Application.Features.User.LoadSingleUser;

public class LoadSingleUserMediatorQueryResult
{
    public ReadUserModel User { get; set; }
    
    public static LoadSingleUserMediatorQueryResult CreateResult(ReadUserModel user)
    {
        return new LoadSingleUserMediatorQueryResult { User = user };
    }
}