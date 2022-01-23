using MediatR;

namespace EventStore.Application.Features.User.LoadAllUsers
{
    /// <summary>
    /// Command to load all registered users.
    /// </summary>
    public class LoadAllUsersMediatorCommand : IRequest<LoadAllUsersMediatorCommandResponse>
    {
        //empty command as we only require the trigger for the business logic.

        /// <summary>
        /// Create new instance of <see cref="LoadAllUsersMediatorCommand"/>
        /// </summary>
        /// <returns></returns>
        public static LoadAllUsersMediatorCommand CreateCommand() => new LoadAllUsersMediatorCommand();
    }
}
