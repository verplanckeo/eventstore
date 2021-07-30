using EventStore.Application.Features.User.Authenticate;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class AuthenticateUserMediatorCommandBuilder : GenericBuilder<AuthenticateUserMediatorCommand>
    {
        private AuthenticateUserMediatorCommand _model => AuthenticateUserMediatorCommand.CreateCommand("overplan", "securepassword");
        
        public AuthenticateUserMediatorCommandBuilder()
        {
            SetDefaults(model =>
            {
                model.Password = _model.Password;
                model.UserName = model.UserName;
            });
        }
    }
}