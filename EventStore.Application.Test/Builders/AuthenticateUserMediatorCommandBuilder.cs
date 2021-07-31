using EventStore.Application.Features.User.Authenticate;
using EventStore.Shared.Test;

namespace EventStore.Application.Test.Builders
{
    public class AuthenticateUserMediatorCommandBuilder : GenericBuilder<AuthenticateUserMediatorCommand>
    {
        private static readonly AuthenticateUserMediatorCommand Model = AuthenticateUserMediatorCommand.CreateCommand("overplan", "securepassword");
        
        public AuthenticateUserMediatorCommandBuilder()
        {
            SetDefaults(model =>
            {
                model.Password = Model.Password;
                model.UserName = Model.UserName;
            });
        }
    }
}