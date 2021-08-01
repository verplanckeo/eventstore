using EventStore.Application.Features.User.Authenticate;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class AuthenticateUserMediatorCommandBuilder : GenericBuilder<AuthenticateUserMediatorCommand>
    {
        public AuthenticateUserMediatorCommandBuilder()
        {
            SetDefaults(() => AuthenticateUserMediatorCommand.CreateCommand("overplan", "securepassword"));
        }
    }
}