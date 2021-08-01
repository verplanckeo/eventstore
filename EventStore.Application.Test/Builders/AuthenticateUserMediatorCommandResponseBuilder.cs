using EventStore.Application.Features.User.Authenticate;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class AuthenticateUserMediatorCommandResponseBuilder : GenericBuilder<AuthenticateUserMediatorCommandResponse>
    {
        public AuthenticateUserMediatorCommandResponseBuilder()
        {
            SetDefaults(() => AuthenticateUserMediatorCommandResponse.CreateResponse("70A73A25-C837-4D38-AE78-3F6F28B8FF54", "unittest-jwt-token"));
        }
    }
}